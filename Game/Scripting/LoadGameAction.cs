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
                _levelDataService.SetLevelNumber(levelNumber);

                if (player.hasDied && (!(_audioService.IsPlayingSound(playerDied))))
                {
                    ReloadCast(scene);
                }
                else if (player.hasWon && (!(_audioService.IsPlayingSound(playerWin))))
                {
                    levelNumber += 1;
                    playerLives += 2;
                    ReloadCast(scene);
                }

            }


            catch (Exception exception)
            {
                callback.OnError("Couldn't load level.", exception);
            }

        }


        // Loads the cast initially.
        public void LoadCastInitially(Scene scene)
        {
            _levelDataService.SetLevelNumber(levelNumber);
            BuildCast buildCast = new BuildCast();

            buildCast.CreateNewLabel(scene, playerLives, _levelDataService);
            buildCast.CreateRegularActors(scene, playerLives, _levelDataService);
            buildCast.CreateNewEnemies(scene, _levelDataService);
            buildCast.CreateNewPlatforms(scene, _levelDataService);
            buildCast.CreateNewDoors(scene, _levelDataService);
            buildCast.CreateNewFlagpole(scene, _levelDataService);
        }
        

        // Clears and reloads the cast
        private void ReloadCast(Scene scene)
        {
            _levelDataService.SetLevelNumber(levelNumber);
            BuildCast buildCast = new BuildCast();
            scene.ClearCast();
            playerLives -= 1;
            
            buildCast.CreateNewLabel(scene, playerLives, _levelDataService);
            buildCast.CreateRegularActors(scene, playerLives, _levelDataService);
            buildCast.CreateNewEnemies(scene, _levelDataService);
            buildCast.CreateNewPlatforms(scene, _levelDataService);
            buildCast.CreateNewFlagpole(scene, _levelDataService);
            if (levelNumber == 1)
            {
                buildCast.CreateNewDoors(scene, _levelDataService);
            }
        }

    }
}