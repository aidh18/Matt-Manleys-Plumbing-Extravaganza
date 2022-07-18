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

        bool isGrounded = false;

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
                Actor player = scene.GetFirstActor("actors");
                Actor flagpole = scene.GetFirstActor("flagpole");
                List<Actor> platforms = scene.GetAllActors("platforms");
                List<Image> enemies = scene.GetAllActors<Image>("enemies");
                string playerWin = _settingsService.GetString("playerWin");
                string enemyDied = _settingsService.GetString("enemyDied");
                
                // detect a collision between the platforms and the player.
                if (player.Overlaps(flagpole))
                {
                    _audioService.PlaySound(playerWin);
                }

                foreach (Actor platform in platforms)
                {
                    if (player.Overlaps(platform))
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Player: Right {player.GetRight()} | Left {player.GetLeft()}");
                        Console.WriteLine($"Platform: Left {platform.GetLeft()} | Right {platform.GetRight()}");
                        Console.WriteLine();
                        int collisionDirection = player.DetectCollisionDirection(platform);
                        // resolve by moving the actor to the top
                        if (collisionDirection == 1) // Hits left side of platform
                        {
                            Console.WriteLine("Hit left");
                            float x = platform.GetLeft() - player.GetWidth();
                            float y = player.GetTop();
                            player.MoveTo(x, y);
                            isGrounded = true;
                        }
                        else if (collisionDirection == 2) // Hits right side of platform
                        {
                            Console.WriteLine("Hit right");
                            float x = platform.GetRight();
                            float y = player.GetTop();
                            player.MoveTo(x, y);
                            isGrounded = true;
                        }
                        else if (collisionDirection == 3) // Lands on top of platform
                        {
                            Console.WriteLine("Hit top");
                            float x = player.GetLeft();
                            float y = platform.GetTop() - player.GetHeight();
                            player.MoveTo(x, y);
                            isGrounded = true;
                        }
                        else if (collisionDirection == 4) // Hits bottom of platform
                        {
                            Console.WriteLine("Hit bottom");
                            float x = player.GetLeft();
                            float y = platform.GetBottom();
                            player.MoveTo(x, y);
                            isGrounded = true;
                        }
                        else
                        {
                            isGrounded = false;
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
                        }
                    }
                }
                foreach (Image enemy in enemies)
                {
                    if (enemy.Overlaps(player))
                    {
                        _audioService.PlaySound(enemyDied);
                        float vx = enemy.GetVelocity().X * 0;
                        enemy.Steer(vx, 0);
                        if (!(enemy.hasDied))
                        {
                            enemy.Display(@"Assets\Images\GoombaDead.png");
                            enemy.hasDied = true;
                        }
                        // enemies.Remove(enemy);
                    }
                }
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't check if actors collide.", exception);
            }
        }
    }
}