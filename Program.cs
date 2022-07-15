using System;
using System.IO;
using Matt_Manleys_Plumbing_Extravaganza.Game.Casting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Directing;
using Matt_Manleys_Plumbing_Extravaganza.Game.Scripting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Services;

namespace Matt_Manleys_Plumbing_Extravaganza
{
    internal class Program
    {
        static readonly string platformsFile = @"Assets\LevelData\platforms.txt";
        
        static void Main(string[] args)
        {
            Scene scene = new Scene();

            // Instantiate a service factory for other objects to use.
            IServiceFactory serviceFactory = new RaylibServiceFactory();
            
            // Instantiate the actors that are used
            Label label = new Label();
            label.Display("I am the greatest person in the world.");
            label.MoveTo(25, 25);
            
            Hero hero = new Hero();
            hero.SizeTo(32, 32);
            hero.MoveTo(32, 96); // world coordinates
            hero.Tint(Color.Red());

            Actor screen = new Actor();
            screen.SizeTo(480, 480);
            screen.MoveTo(0, 0); // screen (or raylib window) coordinates 

            Actor world = new Actor();
            world.SizeTo(6752, 480);
            world.MoveTo(0, 0);

            // Image background = new Image();
            // background.SizeTo(6752, 480);
            // background.MoveTo(0, 0);
            // background.Display(@"Assets\Images\Background.png");

            // Draw the locations of the platforms from the text file and instantiate them
            string[] lines = File.ReadAllLines(platformsFile);  
            foreach(string line in lines)
            {
                String[] platformsData = line.Split(", ", 5, StringSplitOptions.RemoveEmptyEntries);
                Actor platform = new Actor();
                platform.SizeTo(float.Parse(platformsData[0]), float.Parse(platformsData[1]));
                platform.MoveTo(float.Parse(platformsData[2]), float.Parse(platformsData[3])); // world coordinates
                platform.Tint(Color.Transparent());
                scene.AddActor("platforms", platform);
                // if (platformsData[4] == "Floor")
                // {
                //     // Do nothing for now
                //     // Eventually, we will assign the image of the floor to this
                // }
                // else if (platformsData[4] == "Brick")
                // {
                //     // Do nothing for now
                //     // Eventually, we will assign the image of a brick to this
                // }
            }

            Camera camera = new Camera(hero, screen, world);

            // Instantiate the actions that use the actors.
            SteerActorAction steerActorAction = new SteerActorAction(serviceFactory);
            MoveActorAction moveActorAction = new MoveActorAction(serviceFactory);
            DrawActorAction drawActorAction = new DrawActorAction(serviceFactory);
            CollideActorsAction collideActorsAction = new CollideActorsAction(serviceFactory);
            DrawImagesAction drawImagesAction = new DrawImagesAction(serviceFactory);

            // Instantiate a new scene, add the actors and actions.
            scene.AddActor("actors", hero);
            scene.AddActor("labels", label);
            scene.AddActor("screen", screen);
            scene.AddActor("camera", camera);

            scene.AddAction(Phase.Input, steerActorAction);
            scene.AddAction(Phase.Update, moveActorAction);
            scene.AddAction(Phase.Update, collideActorsAction);
            scene.AddAction(Phase.Output, drawActorAction);

            Director director = new Director(serviceFactory);
            director.Direct(scene);
        }
    }
}