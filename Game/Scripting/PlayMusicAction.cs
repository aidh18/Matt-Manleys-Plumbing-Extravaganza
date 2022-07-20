using System;
using Matt_Manleys_Plumbing_Extravaganza.Game.Casting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Services;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Scripting
{

    /// <summary>
    /// Plays background music.
    /// </summary>
    public class PlayMusicAction : Matt_Manleys_Plumbing_Extravaganza.Game.Scripting.Action
    {

        private IAudioService _audioService;
        private ISettingsService _settingsService;


        public PlayMusicAction(IServiceFactory serviceFactory)
        {
            _audioService = serviceFactory.GetAudioService();
            _settingsService = serviceFactory.GetSettingsService();
        }


        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {

            try
            {

                Hero player = (Hero) scene.GetFirstActor("actors");
                string backgroundMusic = _settingsService.GetString("backgroundMusic");
                string playerWin = _settingsService.GetString("playerWin");


                // Start playing music if it isn't already and the player is still alive.
                if ((!(player.hasDied)) && (!(player.hasWon)))
                {
                    if (!_audioService.IsPlayingMusic(backgroundMusic))
                    {
                        _audioService.PlayMusic(backgroundMusic);
                    }
                    
                    // Update the audio buffer to keep playing it.
                    _audioService.UpdateMusic(backgroundMusic);
                }
                else
                {
                    _audioService.PauseMusic(backgroundMusic);
                }

            }


            catch (Exception exception)
            {
                callback.OnError("Couldn't play music.", exception);
            }

        }
    }
}