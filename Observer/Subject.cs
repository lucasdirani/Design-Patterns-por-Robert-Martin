using System.Collections.Generic;

namespace PatternsUncleBob.Observer
{
    public class Subject<T> 
        where T : IObserver
    {
        private readonly IList<T> itsObservers = new List<T>();

        public void NotifyObservers()
        {
            foreach (IObserver observer in itsObservers)
            {
                observer.Update();
            }
        }

        public void RegisterObserver(T observer)
        {
            itsObservers.Add(observer);
        }
    }
}