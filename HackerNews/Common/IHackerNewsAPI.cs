using HackerNews.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Common
{
    public interface IHackerNewsAPI
    {
        Task<IEnumerable<int>> GetNewStoriesAsync();
        Task<StoryDto> GetStoryByIdAsync(int id);
    }
}
