using System;
using Matt_Manleys_Plumbing_Extravaganza.Game.Casting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Scripting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Services;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Scripting
{
    /// <summary>
    /// Steers an actor in a direction corresponding to keyboard input. Note, this does not update 
    /// the actor's position, just steers it in a certain direction. See MoveActorAction to see how
    /// the actor's position is actually updated.
    /// </summary>
    public class SteerActorAction : Matt_Manleys_Plumbing_Extravaganza.Game.Scripting.Action
    {
        private IKeyboardService _keyboardService;

        public SteerActorAction(IServiceFactory serviceFactory)
        {
            _keyboardService = serviceFactory.GetKeyboardService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                Hero hero = (Hero) scene.GetFirstActor("actors");
                // declare direction variables
                float directionX = hero.GetVelocity().X;
                float directionY = hero.GetVelocity().Y;

                // determine vertical or y-axis direction
                if (_keyboardService.IsKeyDown(KeyboardKey.W))
                {
                    directionY = -5;
                }
                else if (_keyboardService.IsKeyDown(KeyboardKey.S))
                {
                    directionY = 5;
                }

                // determine horizontal or x-axis direction
                if (_keyboardService.IsKeyDown(KeyboardKey.A))
                {
                    directionX = -5;
                }
                else if (_keyboardService.IsKeyDown(KeyboardKey.D))
                {
                    directionX = 5;
                }
                else
                {
                    directionX = 0;
                }
                
                // JUMP
                if (hero.isJumping() == false)
                {
                    if (_keyboardService.IsKeyPressed(KeyboardKey.Space))
                    {
                        directionY = -20;
                        hero.StartJump();
                    }
                }

                if (hero.isJumping() == true)
                {
                    directionY += 1;
                    if (directionY == 5)
                    {
                        hero.StopJump();
                    }
                }

                // steer the actor in the desired direction
                hero.Steer(directionX, directionY);
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't steer actor.", exception);
            }
        }
    }
}