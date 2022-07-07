using System;
using Matt_Manleys_Plumbing_Extravaganza.Game.Casting;
// using Matt_Manleys_Plumbing_Extravaganza.Game.Scripting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Services;

namespace Matt_Manleys_Plumbing_Extravaganza.Game.Directing
{

    public class Director
    {

        private VideoService videoService;

        public Director(VideoService videoService)
        {
            this.videoService = videoService;
        }


        public void StartGame()
        {
            while (videoService.IsWindowOpen())
            {
                GetInputs();
                DoUpdates();
                DoOutputs();
            }
        }

        private void GetInputs()
        {

        }

        private void DoUpdates()
        {
            
        }


        private void DoOutputs()
        {

        }
    }
}