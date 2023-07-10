using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Interface;
using UnityEngine;

namespace Domain
{
    public class Interaction
    {
        public string Name { get; }
        public string BroadcasterName { get; }
        public List<Resolution> Resolutions { get; }
        private int MaxDuration { get; }
        public ITransform Transform { get; }
        private Action<Character> OnInteractionStart { get; }
        private Action OnInteractionFinish { get; }

        private Dictionary<Character, InteractionAction> InteractionActions { get; set; }

        public Interaction(
            string name, 
            string broadcasterName, 
            List<Resolution> resolutions, 
            int maxDuration, 
            ITransform transform,
            Action<Character> onInteractionStart,
            Action onInteractionFinish)
        {
            Name = name;
            BroadcasterName = broadcasterName;
            Resolutions = resolutions;
            MaxDuration = maxDuration;
            Transform = transform;
            OnInteractionStart = onInteractionStart;
            OnInteractionFinish = onInteractionFinish;

            InteractionActions = new Dictionary<Character, InteractionAction>();
        }

        public void OnTick()
        {
            for (int i = InteractionActions.Count - 1; i >= 0; i--)
            {
                InteractionActions.ElementAt(i).Value.OnTick();
            }
        }

        public void Interact(Character character)
        {
            character.StartAction(this, () =>
            {
                var interactionAction = new InteractionAction(MaxDuration);

                interactionAction.SetCallback(() =>
                {
                    InteractionActions.Remove(character);
                    character.FinishAction(this);
                    OnInteractionFinish();
                });

                InteractionActions.Add(character, interactionAction);
                Debug.Log($"{character.Name} using {BroadcasterName} to {Name}");
                OnInteractionStart(character);
            });
        }

        public void ForceFinish(Character character)
        {
            InteractionActions[character].ForceFinish();
        }
    }
}