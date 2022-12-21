namespace PatternsUncleBob.Observer
{
    public class MockTimeSink : IObserver
    {
        private int itsHours;
        private int itsMinutes;
        private int itsSeconds;
        private readonly ITimeSource itsSource;

        public MockTimeSink(ITimeSource timeSource)
        {
            itsSource = timeSource;
        }

        public int GetHours()
        {
            return itsHours;
        }

        public int GetMinutes()
        {
            return itsMinutes;
        }

        public int GetSeconds()
        {
            return itsSeconds;
        }

        public void Update()
        {
            itsHours = itsSource.GetHours();
            itsMinutes = itsSource.GetMinutes();
            itsSeconds = itsSource.GetSeconds();
        }
    }
}