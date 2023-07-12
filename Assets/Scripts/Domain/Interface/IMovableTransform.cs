namespace Domain.Interface
{
    public interface IMovableTransform : ITransform
    {
        Movement Movement { get; }
        void MoveTo(Movement movement);
    }
}