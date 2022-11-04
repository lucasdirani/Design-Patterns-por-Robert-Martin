namespace PatternsUncleBob.TemplateMethod.After
{
    public class DoubleBubbleSorter : BubbleSorter
    {
        private double[] array = null;

        public int Sort(double[] theArray)
        {
            array = theArray;
            length = array.Length;
            return DoSort();
        }

        protected override bool OutOfOrder(int index)
        {
            return array[index] > array[index + 1];
        }

        protected override void Swap(int index)
        {
            double temp = array[index];
            array[index] = array[index + 1];
            array[index + 1] = temp;
        }
    }
}