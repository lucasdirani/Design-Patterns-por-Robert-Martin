namespace PatternsUncleBob.Singleton.Sample
{
    public record User
    {
        public string UserName { get; init; }
    }

    public interface IUserDatabase
    {
        User ReadUser(string userName);
        void WriteUser(User user);
    }
}