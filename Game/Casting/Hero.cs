namespace Matt_Manleys_Plumbing_Extravaganza.Game.Casting
{
    public class Hero : Actor
    {
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
    }
}