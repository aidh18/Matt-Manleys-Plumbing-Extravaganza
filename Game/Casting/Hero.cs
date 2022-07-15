namespace Matt_Manleys_Plumbing_Extravaganza.Game.Casting
{
    public class Hero : Image
    {
        public void ShowWalkLeft()
        {
            string[] filePaths = new string[3];
            filePaths[0] = @"Assets\Images\Mario2.png";
            filePaths[1] = @"Assets\Images\Mario3.png";
            filePaths[2] = @"Assets\Images\Mario4.png";

            this.Animate(filePaths, 0.2f, 60);
        }
        public void ShowWalkRight()
        {
            string[] filePaths = new string[3];
            filePaths[0] = @"Assets\Images\Mario2.png";
            filePaths[1] = @"Assets\Images\Mario3.png";
            filePaths[2] = @"Assets\Images\Mario4.png";

            this.Animate(filePaths, 0.2f, 60);
        }
        public void ShowIdle()
        {
            this.Display(@"Assets\Images\Mario1.png");
        }
        public void ShowJumpLeft()
        {
            string[] filePaths = new string[3];
            filePaths[0] = @"Assets\Images\Mario2.png";
            filePaths[1] = @"Assets\Images\Mario3.png";
            filePaths[2] = @"Assets\Images\Mario4.png";

            this.Animate(filePaths, 0.2f, 60);
        }
        public void ShowJumpRight()
        {
            string[] filePaths = new string[3];
            filePaths[0] = @"Assets\Images\Mario2.png";
            filePaths[1] = @"Assets\Images\Mario3.png";
            filePaths[2] = @"Assets\Images\Mario4.png";

            this.Animate(filePaths, 0.2f, 60);
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