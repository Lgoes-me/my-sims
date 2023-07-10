namespace Domain.Interface
{
    public interface IMovableTransform : ITransform
    {
        void MoveTo(MovementData movementData);
    }
}