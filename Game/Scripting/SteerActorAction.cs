using System;
using Matt_Manleys_Plumbing_Extravaganza.Game.Casting;
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
                // Declare direction variables.
                float directionX = hero.GetVelocity().X;
                float directionY = hero.GetVelocity().Y;
                float positionX = hero.GetLeft();
                string playerJump = _settingsService.GetString("playerJump");
                string playerGoThroughDoor = _settingsService.GetString("playerGoThroughDoor");


                if ((!(hero.isDead)) && (!(hero.hasWon)))
                {
                    // Teleport player when they use a door.
                    if (_keyboardService.IsKeyPressed(KeyboardKey.S))
                    {
                        if (hero.inFrontOfDoor)
                        {
                            if (positionX < 4000)
                            {
                                hero.MoveTo(6528, 384);
                                _audioService.PlaySound(playerGoThroughDoor);
                            }
                            else
                            {
                                hero.MoveTo(2784, 128);
                                _audioService.PlaySound(playerGoThroughDoor);
                            }
                        }
                    }


                    // Determine horizontal or x-axis direction.
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
                        if (previousXDirection > 0 && directionY == 0)
                            {
                                hero.ShowIdleRight();
                            }
                        else if (previousXDirection < 0 && directionY == 0)
                            {
                                hero.ShowIdleLeft();
                            }
                    }
                    

                    // Cause player to jump.
                    if (!(hero.IsJumping()))
                    {
                        if (_keyboardService.IsKeyPressed(KeyboardKey.W))
                        {
                            _audioService.PlaySound(playerJump);
                            directionY = -20;
                            hero.StartJump();
                            if (previousXDirection > 0 && (!(directionY == 0)))
                            {
                                hero.ShowJumpRight();
                            }
                            else if (previousXDirection < 0 && (!(directionY == 0)))
                            {
                                hero.ShowJumpLeft();
                            }
                            
                        }
                    }


                    if (hero.IsJumping())
                    {
                        directionY += 1;
                        if (directionY == 0)
                        {
                            hero.StopJump();
                        }
                    }

                }
                else if (hero.isDead)
                {
                    directionX = 0;
                    directionY = 0;
                }
                else if (hero.hasWon)
                {
                    if (hero.GetTop() < 384)
                    {
                        directionX = 0;
                        directionY = 2;
                    }
                    else
                    {
                        directionX = 0;
                        directionY = 0;
                    }
                }


                // Steer the actor in the desired direction.
                hero.Steer(directionX, directionY);

            }


            catch (Exception exception)
            {
                callback.OnError("Couldn't steer actor.", exception);
            }

        }
    }
}