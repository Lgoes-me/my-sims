namespace Domain.Interface
{
    public interface IMovableTransform : ITransform
    {
        void MoveTo(Movement movement);
    }
}