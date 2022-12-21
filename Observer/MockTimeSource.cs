namespace PatternsUncleBob.Observer
{
    public class MockTimeSource : Subject<MockTimeSink>, ITimeSource
    {
        private int itsHours;
        private int itsMinutes;
        private int itsSeconds;

        public void SetTime(int hours, int mins, int secs)
        {
            itsHours = hours;
            itsMinutes = mins;
            itsSeconds = secs;
            NotifyObservers();
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
    }
}