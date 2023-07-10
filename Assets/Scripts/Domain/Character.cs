using System;
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
        public IMovableTransform Transform { get; private set; }
        public Interaction CurrentInteraction { get; private set; }
        public Action OnCurrentInteractionChanged { get; set; }

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
        }

        private void SearchNextBroadcaster()
        {
            var nextInteraction = BroadcasterManager.FindInteraction(this);
            
            if(nextInteraction == CurrentInteraction)
                return;
            
            nextInteraction.Interact(this);
        }

        public void StartAction(Interaction interaction, Action callback)
        {
            if (CurrentInteraction != null)
                CurrentInteraction.ForceFinish(this);

            var movementData = new MovementData(interaction.Transform, () =>
            {
                foreach (var resolution in interaction.Resolutions)
                {
                    if (Motives.TryGetValue(resolution.Need, out var motive))
                    {
                        motive.InitResolve(resolution);
                    }
                }

                CurrentInteraction = interaction;
                OnCurrentInteractionChanged?.Invoke();

                callback();
            });
            
            Transform.MoveTo(movementData);
        }

        public void FinishAction(Interaction interaction)
        {
            foreach (var resolution in interaction.Resolutions)
            {
                if (Motives.TryGetValue(resolution.Need, out var motive))
                {
                    motive.FinishResolve(resolution);
                }
            }

            CurrentInteraction = null;
            OnCurrentInteractionChanged?.Invoke();
            
            if(!IsPaused)
                SearchNextBroadcaster();
        }

        public void Pause()
        {
            IsPaused = true;
            CurrentInteraction?.ForceFinish(this);
        }

        public void Resume()
        {
            IsPaused = false;
            SearchNextBroadcaster();
        }
    }
}