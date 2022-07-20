using System.IO;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Services
{
    public class LevelDataService
    {
        private int levelNumber;

        public LevelDataService() { }


        public string[] GetEnemiesData()
        {
            string fileLocation = $@"Assets\LevelData\Level{levelNumber}\enemies_locations.txt";
            string[] fileText = File.ReadAllLines(fileLocation);
            return fileText;
        }


        public string[] GetFlagpoleData()
        {
            string fileLocation = $@"Assets\LevelData\Level{levelNumber}\flagpole_location.txt";
            string[] fileText = File.ReadAllLines(fileLocation);
            return fileText;
        }


        public string[] GetHeroData()
        {
            string fileLocation = $@"Assets\LevelData\Level{levelNumber}\hero_location.txt";
            string[] fileText = File.ReadAllLines(fileLocation);
            return fileText;
        }
        
        
        public void SetLevelNumber(int levelNum)
        {
            this.levelNumber = levelNum;
        }


        public string[] GetPlatformsData()
        {
            string fileLocation = $@"Assets\LevelData\Level{levelNumber}\platforms_locations.txt";
            string[] fileText = File.ReadAllLines(fileLocation);
            return fileText;
        }


        public string[] GetWorldData()
        {
            string fileLocation = $@"Assets\LevelData\Level{levelNumber}\world_size.txt";
            string[] fileText = File.ReadAllLines(fileLocation);
            return fileText;
        }

        public string GetWorldImage()
        {
            string fileLocation = $@"Assets\Images\Level{levelNumber}\Background.png";
            return fileLocation;
        }

    }
}