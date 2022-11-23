namespace PatternsUncleBob.Singleton.Sample
{
    public class UserDatabaseSource : IUserDatabase
    {
        private static readonly IUserDatabase theInstance = new UserDatabaseSource();

        public static IUserDatabase Instance
        {
            get
            {
                return theInstance;
            }
        }

        public User ReadUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public void WriteUser(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}