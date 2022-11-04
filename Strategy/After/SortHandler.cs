namespace PatternsUncleBob.Strategy.After
{
    public interface ISortHandler
    {
        void Swap(int index);
        bool OutOfOrder(int index);
        int Length();
        void SetArray(object array);
    }
}