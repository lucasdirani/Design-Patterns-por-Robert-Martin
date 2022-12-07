using System;

namespace PatternsUncleBob.NullObject
{
    public abstract class Employee
    {
        public abstract bool IsTimeToPay(DateTime time);
        public abstract void Pay();

        public static readonly Employee NULL = new NullEmployee();

        private class NullEmployee : Employee
        {
            public override bool IsTimeToPay(DateTime time)
            {
                return false;
            }

            public override void Pay()
            {
            }
        }
    }
}