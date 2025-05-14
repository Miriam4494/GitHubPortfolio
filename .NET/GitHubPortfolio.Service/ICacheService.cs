//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GitHubPortfolio.Service
//{
//    public interface ICacheService
//    {
//        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> factory, TimeSpan expiration);
//        void Remove(string key);
//    }

//}
using System;
using System.Threading.Tasks;

namespace GitHubPortfolio.Service
{
    public interface ICacheService
    {
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> factory, TimeSpan expiration);
        void Remove(string key);
    }
}
