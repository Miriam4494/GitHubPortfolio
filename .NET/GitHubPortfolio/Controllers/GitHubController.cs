
using GitHubPortfolio.Service;
using GitHubPortfolio.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GitHubPortfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GitHubController : ControllerBase
    {
        private readonly GitHubService _gitHubService;
        private readonly ICacheService _cache;

        public GitHubController(GitHubService service, ICacheService cache)
        {
            _gitHubService = service;
            _cache = cache;
        }

        [HttpGet("portfolio")]
        public async Task<IActionResult> GetPortfolio()
        {
            var cacheKey = "portfolioData";
            var cached = await _cache.GetOrAddAsync(cacheKey, async () =>
            {
                var repos = await _gitHubService.GetMyRepositoriesAsync();
                return new CachedPortfolio
                {
                    RetrievedAt = DateTimeOffset.UtcNow,
                    Repositories = repos
                };
            }, TimeSpan.FromMinutes(60));

            var lastActivity = await _gitHubService.GetLastUserActivityAsync();
            if (lastActivity.HasValue && lastActivity > cached.RetrievedAt)
            {
                _cache.Remove(cacheKey);
                var freshRepos = await _gitHubService.GetMyRepositoriesAsync();
                cached = new CachedPortfolio
                {
                    RetrievedAt = DateTimeOffset.UtcNow,
                    Repositories = freshRepos
                };
            }

            return Ok(cached.Repositories);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRepos([FromQuery] string? name, [FromQuery] string? language, [FromQuery] string? user)
        {
            var result = await _gitHubService.SearchRepositories(name, language, user);
            return Ok(result);
        }
    }
}

