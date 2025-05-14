//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GitHubPortfolio.Service.Models
//{
//    internal class CachedPortfolio
//    {
//    }
//}
using Octokit;
using System;
using System.Collections.Generic;

namespace GitHubPortfolio.Service.Models
{
    public class CachedPortfolio
    {
        public DateTimeOffset RetrievedAt { get; set; }
        public IReadOnlyList<Repository> Repositories { get; set; }
    }
}
