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

                // determine horizontal or x-axis direction
                if (_keyboardService.IsKeyDown(KeyboardKey.A))
                {
                    directionX = -5;
                    hero.ShowWalkLeft();
                }
                else if (_keyboardService.IsKeyDown(KeyboardKey.D))
                {
                    directionX = 5;
                    hero.ShowWalkRight();
                }
                else
                {
                    directionX = 0;
                    hero.ShowIdle();
                }
                
                // JUMP
                if (hero.isJumping() == false)
                {
                    if (_keyboardService.IsKeyPressed(KeyboardKey.W))
                    {
                        directionY = -20;
                        hero.StartJump();
                        if (directionX >= 0)
                        {
                            hero.ShowJumpRight();
                        }
                        else
                        {
                            hero.ShowJumpLeft();
                        }
                        
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