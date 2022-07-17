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

        public CollideActorsAction(IServiceFactory serviceFactory)
        {
            _keyboardService = serviceFactory.GetKeyboardService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                // get the actors from the cast
                Actor player = scene.GetFirstActor("actors");
                List<Actor> platforms = scene.GetAllActors("platforms");
                List<Image> enemies = scene.GetAllActors<Image>("enemies");
                
                // detect a collision between the platforms and the player.
                foreach (Actor platform in platforms)
                {
                    if (player.Overlaps(platform))
                    {
                        // resolve by moving the actor to the top
                        float x = player.GetLeft();
                        float y = platform.GetTop() - player.GetHeight();
                        player.MoveTo(x, y);
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
                        float vx = enemy.GetVelocity().X * 0;
                        enemy.Steer(vx, 0);
                        // enemy.Display(@"Assets\Images\GoombaDead.png");
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