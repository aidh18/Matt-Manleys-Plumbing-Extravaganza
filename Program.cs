using Matt_Manleys_Plumbing_Extravaganza.Game.Directing;
using Matt_Manleys_Plumbing_Extravaganza.Game.Scripting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Services;


namespace Matt_Manleys_Plumbing_Extravaganza
{

    internal class Program
    {

        static void Main(string[] args)
        {

            Scene scene = new Scene();
            

            // Instantiate a service factory for other objects to use.
            IServiceFactory serviceFactory = new RaylibServiceFactory();
            

            // Instantiate the actions that use the actors.
            SteerActorAction steerActorAction = new SteerActorAction(serviceFactory);
            MoveActorAction moveActorAction = new MoveActorAction(serviceFactory);
            CollideActorsAction collideActorsAction = new CollideActorsAction(serviceFactory);
            LoadGameAction loadGameAction = new LoadGameAction(serviceFactory);
            DrawActorAction drawActorAction = new DrawActorAction(serviceFactory);
            PlayMusicAction playMusicAction = new PlayMusicAction(serviceFactory);


            // Instantiate the actors.
            loadGameAction.LoadCastInitially(scene);


            // Add the actions to the scene
            scene.AddAction(Phase.Input, steerActorAction);
            scene.AddAction(Phase.Update, moveActorAction);
            scene.AddAction(Phase.Update, collideActorsAction);
            scene.AddAction(Phase.Output, drawActorAction);
            scene.AddAction(Phase.Output, playMusicAction);
            scene.AddAction(Phase.Output, loadGameAction);


            // "ACTION!"
            Director director = new Director(serviceFactory);
            director.Direct(scene);

        }
    }
}