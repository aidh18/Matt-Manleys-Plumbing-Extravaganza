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
            hero.Display(@"Assets\Images\Mario1.png");

            Actor screen = new Actor();
            screen.SizeTo(480, 480);
            screen.MoveTo(0, 0); // screen (or raylib window) coordinates 

            Image world = new Image();
            world.SizeTo(6752, 480);
            world.MoveTo(0, 0);
            world.Display(@"Assets\Images\Background.png");

            Image enemy = new Image();
            enemy.SizeTo(32, 32);
            enemy.MoveTo(1280, 384);
            string[] filePaths = new string[3];
            filePaths[0] = @"Assets\Images\Enemy1.png";
            filePaths[1] = @"Assets\Images\Enemy2.png";
            enemy.Animate(filePaths, 0.2f, 60);
            enemy.Steer(3, 0);

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
            }

            Camera camera = new Camera(hero, screen, world);

            // Instantiate the actions that use the actors.
            SteerActorAction steerActorAction = new SteerActorAction(serviceFactory);
            MoveActorAction moveActorAction = new MoveActorAction(serviceFactory);
            DrawActorAction drawActorAction = new DrawActorAction(serviceFactory);
            CollideActorsAction collideActorsAction = new CollideActorsAction(serviceFactory);
            PlayMusicAction playMusicAction = new PlayMusicAction(serviceFactory);

            // Instantiate a new scene, add the actors and actions.
            scene.AddActor("actors", hero);
            scene.AddActor("labels", label);
            scene.AddActor("screen", screen);
            scene.AddActor("assets", world);
            scene.AddActor("camera", camera);
            scene.AddActor("enemies", enemy);

            scene.AddAction(Phase.Input, steerActorAction);
            scene.AddAction(Phase.Update, moveActorAction);
            scene.AddAction(Phase.Update, collideActorsAction);
            scene.AddAction(Phase.Output, drawActorAction);
            scene.AddAction(Phase.Output, playMusicAction);

            Director director = new Director(serviceFactory);
            director.Direct(scene);
        }
    }
}