namespace Matt_Manleys_Plumbing_Extravaganza.Game.Services
{
    public class RaylibServiceFactory : IServiceFactory
    {
        private static IAudioService AudioService;
        private static IKeyboardService KeyboardService;
        private static IMouseService MouseService;
        private static ISettingsService SettingsService;
        private static IVideoService VideoService;
        private static LevelDataService LevelDataService;

        public RaylibServiceFactory()
        {
            SettingsService = new JsonSettingsService();
            AudioService = new RaylibAudioService(SettingsService);
            KeyboardService = new RaylibKeyboardService();
            MouseService = new RaylibMouseService();
            VideoService = new RaylibVideoService(SettingsService);
            LevelDataService= new LevelDataService();
        }

        public RaylibServiceFactory(string filepath)
        {
            SettingsService = new JsonSettingsService(filepath);
            AudioService = new RaylibAudioService(SettingsService);
            KeyboardService = new RaylibKeyboardService();
            MouseService = new RaylibMouseService();
            VideoService = new RaylibVideoService(SettingsService);
            LevelDataService= new LevelDataService();
        }

        public IAudioService GetAudioService()
        {
            return AudioService;
        }

        public IKeyboardService GetKeyboardService()
        {
            return KeyboardService;
        }

        public IMouseService GetMouseService()
        {
            return MouseService;
        }

        public ISettingsService GetSettingsService()
        {
            return SettingsService;
        }

        public IVideoService GetVideoService()
        {
            return VideoService;
        }

        public LevelDataService GetLevelDataService()
        {
            return LevelDataService;
        }

    }
}