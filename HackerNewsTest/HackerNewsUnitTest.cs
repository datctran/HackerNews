using HackerNews.Dal;
using HackerNews.Dto;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace HackerNewsTest
{
    public class HackerNewsUnitTest
    {
        private readonly HackerNewsAPI _hackerNewsAPI;
        private readonly HttpClient _HttpClient;
        public HackerNewsUnitTest()
        {
            _HttpClient = new HttpClient();
           
            _hackerNewsAPI = new HackerNewsAPI(_HttpClient, new MemoryCache(new MemoryCacheOptions()));
        }

        [Fact]
        public async Task GetLastStoryTestAsync()
        {
            int storyId = await _hackerNewsAPI.GetLastStoryAsync();

            Assert.True(storyId > 0, "Invalid Id");
        }

        [Fact]
        public async Task GetNewStoriesTest()
        {
            IEnumerable<int> storyIds = await _hackerNewsAPI.GetNewStoriesAsync();

            Assert.True(storyIds != null, "Un-instatiated collection returned");

            Assert.True(storyIds.Any(), "Empty resultset");

            foreach (int id in storyIds)
            {
                Assert.True(id > 0, "Invalid Id");
            }
        }

        [Fact]
        public async Task GetStoryByIdTest()
        {
            IEnumerable<int> storyIds = await _hackerNewsAPI.GetNewStoriesAsync();

            Random random = new Random();
            int storyPosition = random.Next(0, storyIds.Count());

            StoryDto story = await _hackerNewsAPI.GetStoryByIdAsync(storyIds.ToArray()[storyPosition]);

            Assert.True(story != null, "Story not found");

            Assert.True(!string.IsNullOrWhiteSpace(story.Url), "Url not found");

            Assert.True(!string.IsNullOrWhiteSpace(story.By), "Author not found");

            Assert.True(!string.IsNullOrWhiteSpace(story.Title), "Title not found");
        }

    }
}
