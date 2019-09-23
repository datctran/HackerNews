using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Common;
using HackerNews.Dal;
using HackerNews.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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