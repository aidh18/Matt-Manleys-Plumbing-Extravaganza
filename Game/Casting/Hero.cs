namespace Matt_Manleys_Plumbing_Extravaganza.Game.Casting
{
    
    /// <summary>
    /// A visible actor that the player steers.
    /// </summary>
    public class Hero : Image
    {

        public bool isDead = false;
        public bool hasWon = false;
        public bool inFrontOfDoor = false;
        private bool jumping = false;


        public void Dies()
        {
            isDead = true;
            this.Display(@"Assets\Images\MarioDeath.png");
        }


        public bool IsJumping()
        {
            
            return jumping;
        }


        public void ShowIdleLeft()
        {
            this.Display(@"Assets\Images\MarioLeftIdle.png");
        }


        public void ShowIdleRight()
        {
            this.Display(@"Assets\Images\MarioRightIdle.png");
        }


        public void ShowJumpLeft()
        {
            this.Display(@"Assets\Images\MarioJumpLeft.png");
        }


        public void ShowJumpRight()
        {
            this.Display(@"Assets\Images\MarioJumpRight.png");
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


        public void StartJump()
        {
            jumping = true;
        }


        public void StopJump()
        {
            jumping = false;
        }


        public void Wins()
        {
            hasWon = true;
            if (this.GetTop() < 384)
            {
                this.Display(@"Assets\Images\MarioClimb.png");
            }
        }

    }
}