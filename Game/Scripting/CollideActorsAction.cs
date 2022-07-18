using System;
using System.Collections.Generic;
using Matt_Manleys_Plumbing_Extravaganza.Game.Casting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Scripting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Services;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Scripting
{
    /// <summary>
    /// Detects and resolves collisions between actors.
    /// </summary>
    public class CollideActorsAction : Matt_Manleys_Plumbing_Extravaganza.Game.Scripting.Action
    {
        private IKeyboardService _keyboardService;
        private IAudioService _audioService;
        private ISettingsService _settingsService;
        private bool initialTouch = true;

        public CollideActorsAction(IServiceFactory serviceFactory)
        {
            _keyboardService = serviceFactory.GetKeyboardService();
            _audioService = serviceFactory.GetAudioService();
            _settingsService = serviceFactory.GetSettingsService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                // get the actors from the cast
                Hero player = (Hero) scene.GetFirstActor("actors");
                Actor flagpole = scene.GetFirstActor("flagpole");
                List<Actor> platforms = scene.GetAllActors("platforms");
                List<Image> enemies = scene.GetAllActors<Image>("enemies");
                string playerWin = _settingsService.GetString("playerWin");
                string enemyDied = _settingsService.GetString("enemyDied");
                string playerDied = _settingsService.GetString("playerDied");
                string backgroundMusic = _settingsService.GetString("backgroundMusic");
                

                

                // detect a collision between the platforms and the player.
                

                foreach (Actor platform in platforms)
                {
                    if (player.Overlaps(platform))
                    {
                        int collisionDirection = player.DetectCollisionDirection(platform);

                        // resolve by moving the actor to the correct side
                        if (collisionDirection == 1) // Hits bottom of platform
                        {
                            float x = player.GetLeft();
                            float y = platform.GetBottom();
                            player.MoveTo(x, y);
                        }
                        else if (collisionDirection == 2) // Hits left side of platform
                        {
                            float x = platform.GetLeft() - player.GetWidth();
                            float y = player.GetTop();
                            player.MoveTo(x, y);
                        }
                        else if (collisionDirection == 3) // Hits right side of platform
                        {
                            float x = platform.GetRight();
                            float y = player.GetTop();
                            player.MoveTo(x, y);
                        }
                        else if (collisionDirection == 4) // Lands on top of platform
                        {
                            float x = player.GetLeft();
                            float y = platform.GetTop() - player.GetHeight();
                            player.MoveTo(x, y);
                            
                        }
                    }

                }
                foreach (Image enemy in enemies)
                {
                    foreach (Actor platform in platforms)
                    {
                        if (enemy.Overlaps(platform))
                        {
                            float vx = enemy.GetVelocity().X * -1;
                            enemy.Steer(vx, 0);
                            if (enemy.movingRight)
                            {
                                enemy.Display(@"Assets\Images\Goomba2.png");
                                enemy.movingRight = false;
                            }
                            else
                            {
                                enemy.Display(@"Assets\Images\Goomba1.png");
                                enemy.movingRight = true;
                            }
                        }
                    }
                }
                foreach (Image enemy in enemies)
                {
                    int collisionDirection = player.DetectCollisionDirection(enemy);
                    if (!(player.isDead))
                    {
                        if (enemy.Overlaps(player) && collisionDirection == 4)
                        {
                            float vx = enemy.GetVelocity().X * 0;
                            enemy.Steer(vx, 0);
                            if (!(enemy.hasDied))
                            {
                                enemy.Display(@"Assets\Images\GoombaDead.png");
                                enemy.hasDied = true;
                                _audioService.PlaySound(enemyDied);
                            }
                        }
                        else if (enemy.Overlaps(player) && (!(enemy.hasDied)))
                        {   
                            player.Dies();
                            _audioService.PauseMusic(backgroundMusic);
                            _audioService.PlaySound(playerDied);
                        }
                    }
                }

                if ((initialTouch == true) && player.Overlaps(flagpole))
                {
                    player.Wins();
                    _audioService.StopMusic(backgroundMusic);
                    _audioService.PlaySound(playerWin);
                    Console.WriteLine("Flag collision");
                    initialTouch = false;
                }

                
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't check if actors collide.", exception);
            }
        }
    }
}