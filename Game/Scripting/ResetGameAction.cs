using System;
using System.IO;
using System.Collections.Generic;
using Matt_Manleys_Plumbing_Extravaganza.Game.Casting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Scripting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Services;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Scripting
{
    /// <summary>
    /// Draws the actors on the screen.
    /// </summary>
    public class ResetGameAction : Matt_Manleys_Plumbing_Extravaganza.Game.Scripting.Action
    {
        private ISettingsService _settingsService;
        private IAudioService _audioService;
        static readonly string platformsFile = @"Assets\LevelData\platforms.txt";
        static readonly string enemiesFile = @"Assets\LevelData\enemy_locations.txt";

        public ResetGameAction(IServiceFactory serviceFactory)
        {
            _settingsService = serviceFactory.GetSettingsService();
            _audioService = serviceFactory.GetAudioService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                Hero player = (Hero) scene.GetFirstActor("actors");
                string playerDied = _settingsService.GetString("playerDied");
                string playerWin = _settingsService.GetString("playerWin");

                if (player.isDead && (!(_audioService.IsPlayingSound(playerDied))))
                {
                    Console.WriteLine("IF ACTIVATED");
                    reloadCast(scene);
                }
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't reset game.", exception);
            }
        }
        
        private void reloadCast(Scene scene)
        {
            scene.ClearCast();
            
            // Instantiate the actors that are used
            Label label = new Label();
            label.Display("I am the greatest person in the world.");
            label.MoveTo(25, 25);
            
            Hero hero = new Hero();
            hero.SizeTo(32, 32);
            hero.MoveTo(96, 384); // world coordinates
            hero.Display(@"Assets\Images\Mario1.png");

            Actor screen = new Actor();
            screen.SizeTo(480, 480);
            screen.MoveTo(0, 0); // screen (or raylib window) coordinates 

            Image world = new Image();
            world.SizeTo(6752, 480);
            world.MoveTo(0, 0);
            world.Display(@"Assets\Images\Background.png");

            string[] enemyLines = File.ReadAllLines(enemiesFile);  
            foreach(string line in enemyLines)
            {
                String[] enemiesData = line.Split(", ", 2, StringSplitOptions.RemoveEmptyEntries);
                Image enemy = new Image();
                enemy.SizeTo(32, 32);
                enemy.MoveTo(float.Parse(enemiesData[0]), float.Parse(enemiesData[1]));
                enemy.Display(@"Assets\Images\Goomba1.png");
                enemy.Steer(3, 0);
                scene.AddActor("enemies", enemy);
            }

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

            Actor flagpole = new Actor();
            flagpole.SizeTo(16, 304);
            flagpole.MoveTo(6344, 80);
            flagpole.Tint(Color.Transparent());


            Camera camera = new Camera(hero, screen, world);

            scene.AddActor("actors", hero);
            scene.AddActor("labels", label);
            scene.AddActor("screen", screen);
            scene.AddActor("assets", world);
            scene.AddActor("camera", camera);
            scene.AddActor("flagpole", flagpole);
        }

    }
}