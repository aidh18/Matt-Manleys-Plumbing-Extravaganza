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
                Actor actor = scene.GetFirstActor("actors");
                Camera camera = (Camera) scene.GetFirstActor("camera");
                List<Actor> platforms = scene.GetAllActors("platforms");

                // draw the actors on the screen using the video service
                _videoService.ClearBuffer();
                _videoService.DrawGrid(32, Color.Gray(), camera);
                _videoService.Draw(label);
                _videoService.Draw(platforms, camera);
                _videoService.Draw(actor, camera);
                _videoService.FlushBuffer();
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't draw actors.", exception);
            }
        }
    }
}