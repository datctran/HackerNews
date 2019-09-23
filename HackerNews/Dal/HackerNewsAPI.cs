using HackerNews.Common;
using HackerNews.Dto;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNews.Dal
{
    public class HackerNewsAPI: IHackerNewsAPI
    {
        private readonly string _urlBase = "https://hacker-news.firebaseio.com/v0";
        private readonly HttpClient _client;
        private IMemoryCache _cache;
        private readonly string _cacheKeyStoryArray = "NewStories";
        private readonly string _cacheKeyStoryBase = "Story";

        public HackerNewsAPI(HttpClient client, IMemoryCache memoryCache)
        {
            _client = client;
            _cache = memoryCache;
        }

        public async Task<IEnumerable<int>> GetNewStoriesAsync()
        {
            IEnumerable<int> result;

            if (!_cache.TryGetValue<IEnumerable<int>>(_cacheKeyStoryArray, out result))
            {
                string url = $"{_urlBase}/newstories.json";
                HttpResponseMessage response = await _client.GetAsync(url);
                result = await response.Content.ReadAsAsync<IEnumerable<int>>();

                MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
                cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(10);
                cacheExpirationOptions.Priority = CacheItemPriority.Normal;
                _cache.Set<IEnumerable<int>>(_cacheKeyStoryArray, result, cacheExpirationOptions);
            }

            return result;
        }

        public async Task<StoryDto> GetStoryByIdAsync(int id)
        {
            StoryDto result;

            if (!_cache.TryGetValue<StoryDto>(_cacheKeyStoryBase + id.ToString(), out result))
            {
                string url = $"{_urlBase}/item/{id}.json";
                HttpResponseMessage response = await _client.GetAsync(url);
                result = await response.Content.ReadAsAsync<StoryDto>();

                MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
                cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(10);
                cacheExpirationOptions.Priority = CacheItemPriority.Normal;
                _cache.Set<StoryDto>(_cacheKeyStoryBase + id.ToString(), result, cacheExpirationOptions);
            }

            return result;
        }

        public async Task<int> GetLastStoryAsync()
        {
            string url = $"{_urlBase}/maxitem.json";
            HttpResponseMessage response = await _client.GetAsync(url);

            int result = await response.Content.ReadAsAsync<int>();

            return result;
        }
    }
}
