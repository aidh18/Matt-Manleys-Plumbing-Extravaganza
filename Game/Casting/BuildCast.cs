using System;
using System.IO;
using Matt_Manleys_Plumbing_Extravaganza.Game.Scripting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Services;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Casting
{

    /// <summary>
    /// Gathers the entire cast.
    /// </summary>
    public class BuildCast
    {
        
        public BuildCast () { }


        public void CreateNewDoors(Scene scene, LevelDataService levelDataService)
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


        public void CreateNewEnemies(Scene scene, LevelDataService levelDataService)
        {
            string[] enemyLines = levelDataService.GetEnemiesData();  
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
            

        public void CreateNewFlagpole(Scene scene, LevelDataService levelDataService)
        {
            string[] flagpoleLines = levelDataService.GetFlagpoleData(); 
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


        public void CreateNewLabel(Scene scene, int lives, LevelDataService levelDataService)
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


        public void CreateNewPlatforms(Scene scene, LevelDataService levelDataService)
        {
            // Draw the locations of the platforms from the text file and instantiate them
            string[] platformLines = levelDataService.GetPlatformsData();  
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


        public void CreateRegularActors(Scene scene, int lives, LevelDataService levelDataService)
        {
            string[] heroLines = levelDataService.GetHeroData();
            string[] worldLines = levelDataService.GetWorldData();
            string backgroundFile = levelDataService.GetWorldImage();

            foreach(string line in heroLines)
            {
                String[] heroData = line.Split(", ", 2, StringSplitOptions.RemoveEmptyEntries);
                Hero hero = new Hero();
                if (lives > 0)
                {
                    hero.SizeTo(32, 32);
                    hero.MoveTo(float.Parse(heroData[0]), float.Parse(heroData[1]));
                    hero.Display(@"Assets\Images\Mario1.png");
                }
                else
                {
                    hero.MoveTo(32, 240);
                    hero.LockVelocity();
                }
                scene.AddActor("actors", hero);
            }

            foreach(string line in worldLines)
            {
                String[] worldData = line.Split(", ", 2, StringSplitOptions.RemoveEmptyEntries);
                Image world = new Image();
                world.SizeTo(float.Parse(worldData[0]), float.Parse(worldData[1]));
                world.MoveTo(0, 0);
                world.Display(backgroundFile);
                scene.AddActor("world", world);
            }

            Actor screen = new Actor();
            screen.SizeTo(480, 480);
            screen.MoveTo(0, 0);
            
            Camera camera = new Camera(scene.GetFirstActor("actors"), screen, scene.GetFirstActor("world"));

            scene.AddActor("screen", screen);
            scene.AddActor("camera", camera);
        }

    }
}