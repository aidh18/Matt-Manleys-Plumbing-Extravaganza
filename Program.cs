using System;
using Matt_Manleys_Plumbing_Extravaganza.Game.Casting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Directing;
using Matt_Manleys_Plumbing_Extravaganza.Game.Scripting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Services;

namespace Matt_Manleys_Plumbing_Extravaganza
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            // Instantiate a service factory for other objects to use.
            IServiceFactory serviceFactory = new RaylibServiceFactory();
            
            // Instantiate the actors that are used in this example.
            Label label = new Label();
            label.Display("'w', 's', 'a', 'd' to move");
            label.MoveTo(25, 25);
            
            Actor player = new Actor();
            player.SizeTo(50, 50);
            player.MoveTo(100, 400); // world coordinates
            player.Tint(Color.Red());

            Actor screen = new Actor();
            screen.SizeTo(1100, 800);
            screen.MoveTo(0, -200); // 

            Actor world = new Actor();
            world.SizeTo(4000, 1000);
            world.MoveTo(0, 0);

            Actor platform = new Actor();
            world.SizeTo(100,50);
            world.MoveTo(0,0);
            platform.Tint(Color.Blue());

            Camera camera = new Camera(player, screen, world);

            // Instantiate the actions that use the actors.
            SteerActorAction steerActorAction = new SteerActorAction(serviceFactory);
            MoveActorAction moveActorAction = new MoveActorAction(serviceFactory);
            DrawActorAction drawActorAction = new DrawActorAction(serviceFactory);

            // Instantiate a new scene, add the actors and actions.
            Scene scene = new Scene();
            scene.AddActor("actors", player);
            scene.AddActor("labels", label);
            scene.AddActor("screen", screen);
            scene.AddActor("camera", camera);

            scene.AddAction(Phase.Input, steerActorAction);
            scene.AddAction(Phase.Update, moveActorAction);
            scene.AddAction(Phase.Output, drawActorAction);

            Director director = new Director(serviceFactory);
            director.Direct(scene);
        }
    }
}