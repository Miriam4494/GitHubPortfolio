

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
