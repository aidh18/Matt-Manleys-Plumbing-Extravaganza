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
    public class DrawActorAction : Matt_Manleys_Plumbing_Extravaganza.Game.Scripting.Action
    {
        private IVideoService _videoService;

        public DrawActorAction(IServiceFactory serviceFactory)
        {
            _videoService = serviceFactory.GetVideoService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                // get the actors from the cast
                Label label = (Label) scene.GetFirstActor("labels");
                Image actor = (Image) scene.GetFirstActor("actors");
                Camera camera = (Camera) scene.GetFirstActor("camera");
                List<Actor> platforms = scene.GetAllActors("platforms");
                List<Image> assets = scene.GetAllActors<Image>("assets");
                List<Image> enemies = scene.GetAllActors<Image>("enemies");

                // draw the actors on the screen using the video service
                _videoService.ClearBuffer();
                _videoService.Draw(assets, camera);
                _videoService.Draw(label);
                _videoService.Draw(platforms, camera);
                _videoService.Draw(actor, camera);
                _videoService.Draw(enemies, camera);
                _videoService.FlushBuffer();
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't draw actors.", exception);
            }
        }
    }
}