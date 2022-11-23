namespace PatternsUncleBob.Monostate.Sample
{
    public class Turnstile
    {
        private static bool isLocked = true;
        private static bool isAlarming = false;
        private static int itsCoins = 0;
        private static int itsRefunds = 0;
        protected static readonly Turnstile LOCKED = new Locked();
        protected static readonly Turnstile UNLOCKED = new Unlocked();
        protected static Turnstile itsState = LOCKED;

        public void Reset()
        {
            Lock(true);
            Alarm(false);
            itsCoins = 0;
            itsRefunds = 0;
            itsState = LOCKED;
        }

        public bool Locked()
        {
            return isLocked;
        }

        public bool Alarm()
        {
            return isAlarming;
        }

        public virtual void Coin()
        {
            itsState.Coin();
        }

        public virtual void Pass()
        {
            itsState.Pass();
        }

        protected void Lock(bool shouldLock)
        {
            isLocked = shouldLock;
        }

        protected void Alarm(bool shouldAlarm)
        {
            isAlarming = shouldAlarm;
        }

        public int Coins
        {
            get { return itsCoins; }
        }

        public int Refunds
        {
            get { return itsRefunds; }
        }

        public void Deposit()
        {
            itsCoins++;
        }

        public void Refund()
        {
            itsRefunds++;
        }
    }

    internal class Locked : Turnstile
    {
        public override void Coin()
        {
            itsState = UNLOCKED;
            Lock(false);
            Alarm(false);
            Deposit();
        }

        public override void Pass()
        {
            Alarm(true);
        }
    }

    internal class Unlocked : Turnstile
    {
        public override void Coin()
        {
            Refund();
        }

        public override void Pass()
        {
            Lock(true);
            itsState = LOCKED;
        }
    }
}