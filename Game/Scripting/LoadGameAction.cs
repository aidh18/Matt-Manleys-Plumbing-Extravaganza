using System;
using Matt_Manleys_Plumbing_Extravaganza.Game.Casting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Services;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Scripting
{

    /// <summary>
    /// Load game.
    /// </summary>
    public class LoadGameAction : Matt_Manleys_Plumbing_Extravaganza.Game.Scripting.Action
    {

        private ISettingsService _settingsService;
        private IAudioService _audioService;
        private LevelDataService _levelDataService;
        private int levelNumber = 1;
        private int playerLives = 3;


        public LoadGameAction(IServiceFactory serviceFactory)
        {
            _settingsService = serviceFactory.GetSettingsService();
            _audioService = serviceFactory.GetAudioService();
            _levelDataService = serviceFactory.GetLevelDataService();
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
                    ReloadCast(scene);
                }
                else if (player.hasWon && (!(_audioService.IsPlayingSound(playerWin))))
                {
                    levelNumber += 1;
                    _levelDataService.SetLevelNumber(levelNumber);
                    playerLives += 2;
                    ReloadCast(scene);
                }

            }


            catch (Exception exception)
            {
                callback.OnError("Couldn't reset game.", exception);
            }

        }


        // Loads the cast initially.
        public void LoadCastInitially(Scene scene)
        {
            BuildCast buildCast = new BuildCast();

            buildCast.CreateNewLabel(scene, playerLives);
            buildCast.CreateRegularActors(scene, playerLives);
            buildCast.CreateNewEnemies(scene);
            buildCast.CreateNewPlatforms(scene);
            buildCast.CreateNewDoors(scene);
            buildCast.CreateNewFlagpole(scene);
        }
        

        // Clears and reloads the cast
        private void ReloadCast(Scene scene)
        {
            BuildCast buildCast = new BuildCast();
            scene.ClearCast();
            playerLives -= 1;
            
            buildCast.CreateNewLabel(scene, playerLives);
            buildCast.CreateRegularActors(scene, playerLives);
            buildCast.CreateNewEnemies(scene);
            buildCast.CreateNewPlatforms(scene);
            buildCast.CreateNewDoors(scene);
            buildCast.CreateNewFlagpole(scene);
        }

    }
}