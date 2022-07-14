using System;
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
                Actor world = camera.GetWorld();

                // move the actor and restrict it to the screen boundaries
<<<<<<< HEAD
                if (hero.isJumping())
                {
                    hero.Move();
                }
                else
                {
                    hero.Move(5);
                }
                // keep actor inside world.
                hero.ClampTo(world);
=======
                actor.Move(9.8f); // use a constant pull of 5 in the downward direction, I.E. gravity
                actor.ClampTo(world); // keep actor inside world.
>>>>>>> 04908053c0b57145acb249ba0493438032e74de9
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't move actor.", exception);
            }
        }
    }
}