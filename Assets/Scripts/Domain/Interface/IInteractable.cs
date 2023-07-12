namespace Domain.Interface
{
    public interface IInteractable
    {
        void OnInteractionStart(Character character);
        void OnInteractionFinish();
    }
}