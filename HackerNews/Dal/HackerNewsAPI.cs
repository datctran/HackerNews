using HackerNews.Common;
using HackerNews.Dto;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNews.Dal
{
    public class HackerNewsAPI: IHackerNewsAPI
    {
        private readonly string _urlBase = "https://hacker-news.firebaseio.com/v0";
        private readonly HttpClient _client;

        public HackerNewsAPI(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<int>> GetNewStoriesAsync()
        {
            string url = $"{_urlBase}/newstories.json";
            HttpResponseMessage response = await _client.GetAsync(url);

            IEnumerable<int> result = await response.Content.ReadAsAsync<IEnumerable<int>>();

            return result;
        }

        public async Task<StoryDto> GetStoryByIdAsync(int id)
        {
            string url =  $"{_urlBase}/item/{id}.json";
            HttpResponseMessage response = await _client.GetAsync(url);

            StoryDto result = await response.Content.ReadAsAsync<StoryDto>();                     

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
