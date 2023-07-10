namespace Manager
{
    public class GameManager
    {
        public TimeManager TimeManager { get; }
        public BroadcasterManager BroadcasterManager { get; }

        public GameManager()
        {
            TimeManager = new TimeManager();
            BroadcasterManager = new BroadcasterManager();
        }
    }
}