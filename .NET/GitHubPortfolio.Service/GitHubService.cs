

//using GitHubPortfolio.Service;
//using Microsoft.Extensions.Options;
//using Octokit;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//public class GitHubService
//{
//    private readonly GitHubClient _client;
//    private readonly GitHubOptions _options;

//    public GitHubService(IOptions<GitHubOptions> options)
//    {
//        _options = options.Value;
//        _client = new GitHubClient(new ProductHeaderValue("GitHubPortfolioApp"))
//        {
//            Credentials = new Credentials(_options.Token)
//        };
//    }

//    public async Task<IReadOnlyList<Repository>> GetMyRepositoriesAsync()
//    {
//        return await _client.Repository.GetAllForUser(_options.Username);
//    }

//    public async Task<Repository> GetRepository(string owner, string name)
//    {
//        return await _client.Repository.Get(owner, name);
//    }

//    public async Task<IReadOnlyList<Repository>> SearchRepositories(string? repoName, string? language, string? user)
//    {
//        var request = new SearchRepositoriesRequest(repoName ?? "");

//        // המרה בטוחה מ-string ל-enum Language
//        if (!string.IsNullOrWhiteSpace(language) && Enum.TryParse<Language>(language, true, out var parsedLanguage))
//        {
//            request.Language = parsedLanguage;
//        }

//        if (!string.IsNullOrWhiteSpace(user))
//        {
//            request.User = user;
//        }

//        var result = await _client.Search.SearchRepo(request);
//        return result.Items;
//    }
//}

using Microsoft.Extensions.Options;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubPortfolio.Service
{
    public class GitHubService
    {
        private readonly GitHubClient _client;
        private readonly GitHubOptions _options;

        public GitHubService(IOptions<GitHubOptions> options)
        {
            _options = options.Value;
            _client = new GitHubClient(new ProductHeaderValue("GitHubPortfolioApp"))
            {
                Credentials = new Credentials(_options.Token)
            };
        }

        public async Task<IReadOnlyList<Repository>> GetMyRepositoriesAsync()
        {
            return await _client.Repository.GetAllForUser(_options.Username);
        }

        public async Task<IReadOnlyList<Repository>> SearchRepositories(string? repoName, string? language, string? user)
        {
            var request = new SearchRepositoriesRequest(repoName ?? "");

            if (!string.IsNullOrWhiteSpace(language) && Enum.TryParse<Language>(language, true, out var parsedLanguage))
            {
                request.Language = parsedLanguage;
            }

            if (!string.IsNullOrWhiteSpace(user))
            {
                request.User = user;
            }

            var result = await _client.Search.SearchRepo(request);
            return result.Items;
        }

        public async Task<DateTimeOffset?> GetLastUserActivityAsync()
        {
            var userEvents = await _client.Activity.Events.GetAllUserReceived(_options.Username);

            var latest = userEvents.FirstOrDefault();
            return latest?.CreatedAt;
        }

    }
}

