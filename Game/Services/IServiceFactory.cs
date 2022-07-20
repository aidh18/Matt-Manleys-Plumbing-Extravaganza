namespace Matt_Manleys_Plumbing_Extravaganza.Game.Services
{
    public interface IServiceFactory
    {
        IAudioService GetAudioService();
        IKeyboardService GetKeyboardService();
        IMouseService GetMouseService();
        ISettingsService GetSettingsService();
        IVideoService GetVideoService();
        LevelDataService GetLevelDataService();
    }
}