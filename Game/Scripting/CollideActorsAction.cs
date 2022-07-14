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
                
                // detect a collision between the platforms and the player.
                foreach (Actor platform in platforms)
                {
                    if (player.Overlaps(platform))
                    {
                        // resolve by changing the actor's color to something else
                        player.Tint(Color.Green());
                        float x = player.GetLeft();
                        float y = platform.GetTop() - player.GetHeight();
                        player.MoveTo(x, y);
                    }
                    else
                    {
                        // otherwise, just make it the original color
                        player.Tint(Color.Red());
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