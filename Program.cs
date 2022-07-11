using System;
using Matt_Manleys_Plumbing_Extravaganza.Game.Directing;
using Matt_Manleys_Plumbing_Extravaganza.Game.Services;
using Matt_Manleys_Plumbing_Extravaganza.Game.Scripting;

namespace Matt_Manleys_Plumbing_Extravaganza
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            IServiceFactory serviceFactory = new RaylibServiceFactory();
            Scene scene = new Scene();

            Director director = new Director(serviceFactory);
            director.Direct(scene);
        }
    }
}