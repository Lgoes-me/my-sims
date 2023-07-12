using System.Collections.Generic;
using System.Linq;
using Domain.Interface;
using Manager;

namespace Domain
{
    public class Character : ITimeManagerListener
    {
        public string Name { get; }
        public Dictionary<string, Motive> Motives { get; }
        public Broadcaster Broadcaster { get; }
        private BroadcasterManager BroadcasterManager { get; }
        public IMovableTransform Transform { get; }
        public Interaction CurrentInteraction { get; private set; }
        private bool IsPaused { get; set; }

        public Character(
            string name,
            List<Motive> motives,
            Broadcaster broadcaster,
            BroadcasterManager broadcasterManager,
            IMovableTransform transform)
        {
            Name = name;
            Motives = motives.ToDictionary(m => m.Need, m => m);
            Broadcaster = broadcaster;
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

            CurrentInteraction = advertisement.StartInteraction(FinishInteraction);
        }

        private void FinishInteraction()
        {
            foreach (var (_, motive) in Motives)
            {
                motive.FinishResolution();
            }

            CurrentInteraction = null;

            if (!IsPaused)
                SearchNextBroadcaster();
        }

        public void Pause()
        {
            IsPaused = true;
            CurrentInteraction?.ForceFinish();
        }

        public void Resume()
        {
            IsPaused = false;
            SearchNextBroadcaster();
        }
    }
}