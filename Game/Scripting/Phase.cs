using System;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Scripting
{
    /// <summary>
    /// Defines game phases.
    /// </summary>
    public class Phase
    {
        public const int Input = 0;
        public const int Update = 1;
        public const int Output = 2;
        
        private Phase() {}
    }
}