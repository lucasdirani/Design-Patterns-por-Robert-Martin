namespace PatternsUncleBob.Observer
{
    public interface ITimeSource
    {
        int GetHours();
        int GetMinutes();
        int GetSeconds();
    }
}