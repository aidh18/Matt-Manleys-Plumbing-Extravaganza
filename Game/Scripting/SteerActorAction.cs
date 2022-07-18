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
        private IAudioService _audioService;
        private ISettingsService _settingsService;
        public float previousXDirection = 5;

        public SteerActorAction(IServiceFactory serviceFactory)
        {
            _keyboardService = serviceFactory.GetKeyboardService();
            _audioService = serviceFactory.GetAudioService();
            _settingsService = serviceFactory.GetSettingsService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                Hero hero = (Hero) scene.GetFirstActor("actors");
                // declare direction variables
                float directionX = hero.GetVelocity().X;
                float directionY = hero.GetVelocity().Y;
                string playerJump = _settingsService.GetString("playerJump");

                // determine horizontal or x-axis direction
                if (_keyboardService.IsKeyDown(KeyboardKey.A))
                {
                    directionX = -5;
                    hero.ShowWalkLeft();
                    previousXDirection = directionX;
                }
                else if (_keyboardService.IsKeyDown(KeyboardKey.D))
                {
                    directionX = 5;
                    hero.ShowWalkRight();
                    previousXDirection = directionX;
                }
                else
                {
                    directionX = 0;
                    if (previousXDirection > 0 & directionY == 0)
                        {
                            hero.ShowIdleRight();
                        }
                    else if (previousXDirection < 0 & directionY == 0)
                        {
                            hero.ShowIdleLeft();
                        }
                }
                
                // JUMP
                if (hero.isJumping() == false)
                {
                    if (_keyboardService.IsKeyPressed(KeyboardKey.W))
                    {
                        _audioService.PlaySound(playerJump);
                        directionY = -20;
                        hero.StartJump();
                        if (previousXDirection > 0 & (!(directionY == 0)))
                        {
                            hero.ShowJumpRight();
                        }
                        else if (previousXDirection < 0 & (!(directionY == 0)))
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