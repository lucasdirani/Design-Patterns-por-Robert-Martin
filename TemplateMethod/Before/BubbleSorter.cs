namespace PatternsUncleBob.TemplateMethod.Before
{
    public class BubbleSorter
    {
        static int operations = 0;

        public static int Sort(int[] array)
        {
            operations = 0;
            if (array.Length <= 1)
                return operations;
            for (int nextToLast = array.Length - 2; nextToLast >= 0; nextToLast--)
            {
                for (int index = 0; index <= nextToLast; index++)
                {
                    CompareAndSwap(array, index);
                }
            }
            return operations;
        }

        private static void CompareAndSwap(int[] array, int index)
        {
            if (array[index] > array[index+1])
            {
                Swap(array, index);
            }
            operations++;
        }

        private static void Swap(int[] array, int index)
        {
            int temp = array[index];
            array[index] = array[index + 1];
            array[index + 1] = temp;
        }
    }
}