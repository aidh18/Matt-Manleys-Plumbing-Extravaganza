using System;
using Matt_Manleys_Plumbing_Extravaganza.Game.Directing;

namespace Matt_Manleys_Plumbing_Extravaganza
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Director director = new Director();
            director.StartGame();
        }
    }
}