using System;
using System.Collections.Generic;
using Matt_Manleys_Plumbing_Extravaganza.Game.Casting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Scripting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Services;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Scripting
{
    /// <summary>
    /// Moves the actors and clamps them to the screen boundaries. The call to actor.Move() is what updates
    /// their position on the screen.
    /// </summary>
    public class MoveActorAction : Matt_Manleys_Plumbing_Extravaganza.Game.Scripting.Action
    {
        private IKeyboardService _keyboardService;

        public MoveActorAction(IServiceFactory serviceFactory)
        {
            _keyboardService = serviceFactory.GetKeyboardService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                // get the actors from the scene
                Hero hero = (Hero) scene.GetFirstActor("actors");
                Camera camera = (Camera) scene.GetFirstActor("camera");
                List<Image> enemies = scene.GetAllActors<Image>("enemies");
                Actor world = camera.GetWorld();

                // move the actor and restrict it to the screen boundaries
                if (hero.isJumping())
                {
                    hero.Move();
                }
                else
                {
                    hero.Move(8);
                }
                // keep actor inside world.
                hero.ClampTo(world);

                foreach (Image enemy in enemies)
                {
                    enemy.Move();
                }
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't move actor.", exception);
            }
        }
    }
}