namespace Matt_Manleys_Plumbing_Extravaganza.Game.Casting
{
    public class Hero : Image
    {

        public int DetectCollisionDirection(Hero player, Actor platform)
        {
            int collisionLocation;

            if (player.GetRight() > platform.GetLeft() && player.GetLeft() < platform.GetRight() - player.GetWidth() / 2 && player.GetBottom() > platform.GetTop())
            {
                collisionLocation = 1;
            }

            return collisionLocation;
        }
        public void ShowWalkLeft()
        {
            string[] filePaths = new string[4];
            filePaths[0] = @"Assets\Images\MarioLeft1.png";
            filePaths[1] = @"Assets\Images\MarioLeft2.png";
            filePaths[2] = @"Assets\Images\MarioLeft3.png";
            filePaths[3] = @"Assets\Images\MarioLeft2.png";

            this.Animate(filePaths, 0.2f, 60);
        }
        public void ShowWalkRight()
        {
            string[] filePaths = new string[4];
            filePaths[0] = @"Assets\Images\MarioRight1.png";
            filePaths[1] = @"Assets\Images\MarioRight2.png";
            filePaths[2] = @"Assets\Images\MarioRight3.png";
            filePaths[3] = @"Assets\Images\MarioRight2.png";

            this.Animate(filePaths, 0.2f, 60);
        }
        public void ShowIdleRight()
        {
            this.Display(@"Assets\Images\MarioRightIdle.png");
        }
        public void ShowIdleLeft()
        {
            this.Display(@"Assets\Images\MarioLeftIdle.png");
        }
        public void ShowJumpLeft()
        {
            this.Display(@"Assets\Images\MarioJumpLeft.png");
        }
        public void ShowJumpRight()
        {
            this.Display(@"Assets\Images\MarioJumpRight.png");
        }
        private bool jumping = false;

        public bool isJumping()
        {
            
            return jumping;
        }
        public void StartJump()
        {
            jumping = true;
        }
        public void StopJump()
        {
            jumping = false;
        }
        public void HasDied()
        {
            
        }
    }
}