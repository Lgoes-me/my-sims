namespace Domain
{
    public class Resolution
    {
        public string Need { get; }
        public float Rate { get; }

        public Resolution(string need, float rate)
        {
            Need = need;
            Rate = rate;
        }
    }
}