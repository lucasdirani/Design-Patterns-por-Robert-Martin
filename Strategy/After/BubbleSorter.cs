namespace PatternsUncleBob.Strategy.After
{
    public class BubbleSorter
    {
        private int operations = 0;
        private int length = 0;
        private readonly ISortHandler itsSortHandler = null;

        public BubbleSorter(ISortHandler sortHandler)
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
            for (int nextToLast = length - 2; nextToLast >= 0; nextToLast--)
            {
                for (int index = 0; index <= nextToLast; index++)
                {
                    if (itsSortHandler.OutOfOrder(index))
                    {
                        itsSortHandler.Swap(index);
                    }
                    operations++;
                }
            }
            return operations;
        }
    }
}