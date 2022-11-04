namespace PatternsUncleBob.Strategy.After
{
    public class QuickBubbleSorter
    {
        private int operations = 0;
        private int length = 0;
        private ISortHandler itsSortHandler = null;

        public QuickBubbleSorter(ISortHandler sortHandler)
        {
            itsSortHandler = sortHandler;
        }

        public int Sort(object array)
        {
            itsSortHandler.SetArray(array);
            length = itsSortHandler.Length();
            operations = 0;
            if (length <= 1)
                return operations;
            bool thisPassInOrder = false;
            for (int nextToLast = length - 2; nextToLast >= 0 && !thisPassInOrder; nextToLast--)
            {
                thisPassInOrder = true;
                for (int index = 0; index <= nextToLast; index++)
                {
                    if (itsSortHandler.OutOfOrder(index))
                    {
                        itsSortHandler.Swap(index);
                        thisPassInOrder = false;
                    }
                    operations++;
                }
            }
            return operations;
        }
    }
}