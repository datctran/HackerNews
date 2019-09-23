using HackerNews.Common;
using HackerNews.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IHackerNewsAPI _hackerNewsAPI;

        public NewsController(IHackerNewsAPI hackerNewsAPI)
        {
            _hackerNewsAPI = hackerNewsAPI;    
        }

        [HttpGet]
        public async Task<IEnumerable<int>> Get()
        {
            return await _hackerNewsAPI.GetNewStoriesAsync();
        }

        [HttpGet("{id}")]
        public async Task<StoryDto> Get(int id)
        {
            return await _hackerNewsAPI.GetStoryByIdAsync(id);
        }
    }
}