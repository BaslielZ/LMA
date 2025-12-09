using LifeManagementApp.Interfaces;
using LifeManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace LifeManagementApp.Services
{
    public class JokeService : IJokeService
    {
        private readonly HttpClient _httpClient;

        // HttpClient injected automatically by AddHttpClient
        public JokeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Joke>> GetJokesAsync()
        {
            var jokes = new List<Joke>();

            var url = "https://v2.jokeapi.dev/joke/Programming,Dark" +
                      "?amount=1";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            var root = json.RootElement;

            // CASE 1: amount=1 → SINGLE JOKE
            if (root.TryGetProperty("jokes", out JsonElement jokesArray) == false)
            {
                jokes.Add(ParseSingleJoke(root));
                return jokes;
            }

            // CASE 2: amount>1 → ARRAY OF JOKES
            foreach (var item in jokesArray.EnumerateArray())
            {
                jokes.Add(ParseSingleJoke(item));
            }

            return jokes;
        }

        private Joke ParseSingleJoke(JsonElement e)
        {
            if (e.GetProperty("type").GetString() == "single")
            {
                return new Joke
                {
                    Content = e.GetProperty("joke").GetString()
                };
            }
            else
            {
                return new Joke
                {
                    Content = $"{e.GetProperty("setup").GetString()} — {e.GetProperty("delivery").GetString()}"
                };
            }
        }

    }
}
