using System;
using System.Collections.Generic;
using Matt_Manleys_Plumbing_Extravaganza.Game.Casting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Scripting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Services;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Scripting
{
    /// <summary>
    /// Draws the actors on the screen.
    /// </summary>
    public class DrawImagesAction : Matt_Manleys_Plumbing_Extravaganza.Game.Scripting.Action
    {
        private IVideoService _videoService;

        public DrawImagesAction(IServiceFactory serviceFactory)
        {
            _videoService = serviceFactory.GetVideoService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                // get the actors from the cast
                List<Image> assets = scene.GetAllActors<Image>("assets");

                // draw the actors on the screen using the video service
                // _videoService.ClearBuffer();
                _videoService.Draw(assets);
                // _videoService.FlushBuffer();
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't draw actors.", exception);
            }
        }
    }
}