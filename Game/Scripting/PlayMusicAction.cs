using System;
using System.Runtime.Serialization.Formatters;
using Matt_Manleys_Plumbing_Extravaganza.Game.Casting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Scripting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Services;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Scripting
{
    /// <summary>
    /// Plays background music for the example. Note the path to the music file comes from the
    /// program settings (see settings.json).
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

                // start playing music if it isn't already and the player is still alive
                if ((!(player.isDead)) && (!(player.hasWon)))
                {
                    if (!_audioService.IsPlayingMusic(backgroundMusic))
                    {
                        _audioService.PlayMusic(backgroundMusic);
                    }
                    
                    // update the audio buffer to keep playing it
                    _audioService.UpdateMusic(backgroundMusic);
                }
                // else if (player.hasWon)
                // {
                //     _audioService.PauseMusic(backgroundMusic);
                //     _audioService.PlaySound(playerWin);

                // }
                // else if (player.isDead)
                // {
                //     _audioService.PauseMusic(backgroundMusic);
                // }
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