using System;
using System.Collections.Generic;
using Matt_Manleys_Plumbing_Extravaganza.Game.Casting;
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

                // Get the actors from the cast.
                Hero player = (Hero) scene.GetFirstActor("actors");
                Actor flagpole = scene.GetFirstActor("flagpole");
                List<Actor> platforms = scene.GetAllActors("platforms");
                List<Image> enemies = scene.GetAllActors<Image>("enemies");
                List<Image> doors = scene.GetAllActors<Image>("doors");
                string playerWin = _settingsService.GetString("playerWin");
                string enemyDied = _settingsService.GetString("enemyDied");
                string playerDied = _settingsService.GetString("playerDied");
                string backgroundMusic = _settingsService.GetString("backgroundMusic");
                

                // Detect a collision between the platforms and the player.
                foreach (Actor platform in platforms)
                {
                    if (player.Overlaps(platform))
                    {
                        int collisionDirection = player.DetectCollisionDirection(platform);

                        // Resolve collision by moving the actor to the correct side
                        // Player hits bottom of platform.
                        if (collisionDirection == 1)
                        {
                            float x = player.GetLeft();
                            float y = platform.GetBottom();
                            player.MoveTo(x, y);
                        }
                        // Player hits left side of platform.
                        else if (collisionDirection == 2)
                        {
                            float x = platform.GetLeft() - player.GetWidth();
                            float y = player.GetTop();
                            player.MoveTo(x, y);
                        }
                        // Player hits right side of platform.
                        else if (collisionDirection == 3)
                        {
                            float x = platform.GetRight();
                            float y = player.GetTop();
                            player.MoveTo(x, y);
                        }
                        // Player lands on top of platform.
                        else if (collisionDirection == 4)
                        {
                            float x = player.GetLeft();
                            float y = platform.GetTop() - player.GetHeight();
                            player.MoveTo(x, y); 
                        }
                    }

                }


                // Detect a collision between the platforms and the enemies.
                foreach (Image enemy in enemies)
                {
                    foreach (Actor platform in platforms)
                    {
                        // Resolve collision by bouncing the enemy in between platforms.
                        if (enemy.Overlaps(platform))
                        {
                            float vx = enemy.GetVelocity().X * -1;
                            enemy.Steer(vx, 0);
                            if (enemy.movingRight)
                            {
                                enemy.Display(@"Assets\Images\MattEnemy2.png");
                                enemy.movingRight = false;
                            }
                            else
                            {
                                enemy.Display(@"Assets\Images\MattEnemy1.png");
                                enemy.movingRight = true;
                            }
                        }
                    }
                }


                // Detect a collision between the player and the enemies.
                foreach (Image enemy in enemies)
                {
                    int collisionDirection = player.DetectCollisionDirection(enemy);
                    if (!(player.isDead))
                    {
                        // Resolve collision by either killing the enemy or killing the player.
                        if (enemy.Overlaps(player) && collisionDirection == 4)
                        {
                            float vx = enemy.GetVelocity().X * 0;
                            enemy.Steer(vx, 0);
                            if (!(enemy.hasDied))
                            {
                                enemy.Display(@"Assets\Images\MattGotBonked.png");
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


                // Detect a collision between the player and the doors.
                foreach (Actor door in doors)
                {
                    // Resolve collision by telling the program that the player can use the door.
                    if (player.Overlaps(door))
                    {
                        player.inFrontOfDoor = true;
                    }
                }


                // Detect a collision between the player and the flagpole.
                // Resolve collision by telling the program that the player has won.
                if ((initialTouch == true) && player.Overlaps(flagpole))
                {
                    player.Wins();
                    _audioService.StopMusic(backgroundMusic);
                    _audioService.PlaySound(playerWin);
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