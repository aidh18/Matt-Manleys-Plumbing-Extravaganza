using Matt_Manleys_Plumbing_Extravaganza.Game.Casting;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Services
{
    public interface PhysicsService
    {
        /// <summary>
        /// Whether or not two bodies have collided.
        /// </summary>
        /// <param name="subject">The first body.</param>
        /// <param name="agent">The second body.</param>
        /// <returns></returns>
        bool HasCollided(Body subject, Body agent);
    }
}