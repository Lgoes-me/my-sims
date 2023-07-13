using System.Collections.Generic;
using System.Linq;
using Domain.Interface;
using Manager;

namespace Domain
{
    public class Character : Broadcaster, ITimeManagerListener
    {
        public string Name { get; }
        public Dictionary<string, Motive> Motives { get; }
        private BroadcasterManager BroadcasterManager { get; }
        public IMovableTransform Transform { get; }
        public Interaction CurrentInteraction { get; private set; }

        public Character(
            string name,
            List<Motive> motives,
            List<Advertisement> advertisements,
            BroadcasterManager broadcasterManager,
            IMovableTransform transform) : base(advertisements)
        {
            Name = name;
            Motives = motives.ToDictionary(m => m.Need, m => m);
            BroadcasterManager = broadcasterManager;
            Transform = transform;
        }

        public void Init()
        {
            SearchNextBroadcaster();
        }

        public void Tick()
        {
            foreach (var (_, motive) in Motives)
            {
                motive.OnTick();
            }

            CurrentInteraction?.OnTick();
        }

        private void SearchNextBroadcaster()
        {
            var advertisement = BroadcasterManager.FindAdvertisement(this);

            if (advertisement == CurrentInteraction?.Advertisement)
                return;

            MoveToNextInteraction(advertisement);
        }

        private void MoveToNextInteraction(Advertisement advertisement)
        {
            if (CurrentInteraction != null)
                CurrentInteraction.ForceFinish();

            var movementData = new Movement(advertisement.Transform, () => StartInteraction(advertisement));

            Transform.MoveTo(movementData);
        }

        private void StartInteraction(Advertisement advertisement)
        {
            foreach (var resolution in advertisement.Resolutions)
            {
                if (Motives.TryGetValue(resolution.Need, out var motive))
                {
                    motive.InitResolution(resolution);
                }
            }

            CurrentInteraction = advertisement.StartInteraction(() =>
            {
                FinishInteraction();
                SearchNextBroadcaster();
            });
        }

        private void FinishInteraction()
        {
            foreach (var (_, motive) in Motives)
            {
                motive.FinishResolution();
            }
            
            CurrentInteraction = null;
        }

        public override void OnInteractionStart(Advertisement advertisement)
        {
            base.OnInteractionStart(advertisement);
            Pause();

            var response = BroadcasterManager.FindBestResponse(advertisement.Responses, this);
            MoveToNextInteraction(response);
        }
        
        private void Pause()
        {
            Transform.Movement?.Stop();
            CurrentInteraction?.ForceFinish();
        }
        
        public override void OnInteractionFinish()
        {
            base.OnInteractionFinish();
            Resume();
        }
        
        private void Resume()
        {
            SearchNextBroadcaster();
        }
    }
}