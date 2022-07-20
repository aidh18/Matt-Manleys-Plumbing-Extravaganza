using System;
using System.IO;
using Matt_Manleys_Plumbing_Extravaganza.Game.Scripting;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Casting
{

    /// <summary>
    /// Gathers the entire cast.
    /// </summary>
    public class BuildCast
    {
        
        string platformsFile = @"Assets\LevelData\Level1\platforms_locations.txt";
        string enemiesFile = @"Assets\LevelData\Level1\enemy_locations.txt";
        string flagpoleFile = @"Assets\LevelData\Level1\flagpole_location.txt";
        


        public BuildCast () { }


        public void CreateNewDoors(Scene scene)
        {
            Image door1 = new Image();
            door1.SizeTo(32, 64);
            door1.MoveTo(2784, 96);
            door1.Display(@"Assets\Images\Door.png");

            Image door2 = new Image();
            door2.SizeTo(32, 64);
            door2.MoveTo(6528, 352);
            door2.Display(@"Assets\Images\Door.png");

            scene.AddActor("doors", door1);
            scene.AddActor("doors", door2);
        }


        public void CreateNewEnemies(Scene scene)
        {
            string[] enemyLines = File.ReadAllLines(enemiesFile);  
            foreach(string line in enemyLines)
            {
                String[] enemiesData = line.Split(", ", 2, StringSplitOptions.RemoveEmptyEntries);
                Image enemy = new Image();
                enemy.SizeTo(32, 32);
                enemy.MoveTo(float.Parse(enemiesData[0]), float.Parse(enemiesData[1]));
                enemy.Display(@"Assets\Images\MattEnemy1.png");
                enemy.Steer(3, 0);
                scene.AddActor("enemies", enemy);
            }
        }
            

        public void CreateNewFlagpole(Scene scene)
        {
            string[] flagpoleLines = File.ReadAllLines(flagpoleFile); 
            foreach(string line in flagpoleLines)
            {
                String[] flagpoleData = line.Split(", ", 2, StringSplitOptions.RemoveEmptyEntries);
                Actor flagpole = new Actor();
                flagpole.SizeTo(16, 304);
                flagpole.MoveTo(float.Parse(flagpoleData[0]), float.Parse(flagpoleData[1]));
                flagpole.Tint(Color.Transparent());
                scene.AddActor("flagpole", flagpole);
            }

        }


        public void CreateNewLabel(Scene scene, int lives)
        {
            Label label = new Label();
            if (lives == 0)
            {
                label.Display("GAME OVER");
                label.MoveTo(25, 25);
                label._fontSize = 72f;
                scene.AddActor("labels", label);
            }
            else
            {
                label.Display($"Mattio Lives x {lives}");
                label.MoveTo(25, 25);
                scene.AddActor("labels", label);
            }
            
        }


        public void CreateNewPlatforms(Scene scene)
        {
            // Draw the locations of the platforms from the text file and instantiate them
            string[] platformLines = File.ReadAllLines(platformsFile);  
            foreach(string line in platformLines)
            {
                String[] platformsData = line.Split(", ", 5, StringSplitOptions.RemoveEmptyEntries);
                Actor platform = new Actor();
                platform.SizeTo(float.Parse(platformsData[0]), float.Parse(platformsData[1]));
                platform.MoveTo(float.Parse(platformsData[2]), float.Parse(platformsData[3])); // world coordinates
                platform.Tint(Color.Transparent());
                scene.AddActor("platforms", platform);
            }
        }


        public void CreateRegularActors(Scene scene, int lives)
        {
            
            Hero hero = new Hero();
            if (lives > 0)
            {
                hero.SizeTo(32, 32);
                hero.MoveTo(96, 384); // world coordinates
                hero.Display(@"Assets\Images\Mario1.png");
            }

            Actor screen = new Actor();
            screen.SizeTo(480, 480);
            screen.MoveTo(0, 0); // screen (or raylib window) coordinates 

            Image world = new Image();
            world.SizeTo(6752, 480);
            world.MoveTo(0, 0);
            world.Display(@"Assets\Images\Background.png");
            
            Camera camera = new Camera(hero, screen, world);

            scene.AddActor("actors", hero);
            scene.AddActor("camera", camera);
            scene.AddActor("assets", world);
            scene.AddActor("screen", screen);
        }

    }
}