using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace GitHubActivity
{
    internal class GitHubService
    {
        public static readonly HttpClient client = new HttpClient();

        public static async Task GitTask(string username)
        {
            string url = $"https://api.github.com/users/{username}/events";

            if (!client.DefaultRequestHeaders.Contains("User-Agent"))
            {
                client.DefaultRequestHeaders.Add("User-Agent", "C# ConsoleApp");
            }
      
            try
            {
                
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var rateLimit = response.Headers.GetValues("X-RateLimit-Remaining").FirstOrDefault();
                Console.WriteLine($"Rate Limit Remaining: {rateLimit}");
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(responseBody))
                {
                    var events = JsonConvert.DeserializeObject<List<GitHubEvent>>(responseBody);
                  

                    OutputEvents(events);
                }
                else
                {
                    Console.WriteLine("No recent activities");
                }

            }

            catch (Exception e)
            {
                Console.WriteLine("No such user ");

            }
            
        }

        private static void OutputEvents(List<GitHubEvent> events)
        {
            var eventHandlers = new Dictionary<string, Action<GitHubEvent>>
            {   { "CreateEvent",gitEvent => Console.WriteLine($"Created a repository {gitEvent.Repo.Name}")},
                { "PushEvent", gitEvent => Console.WriteLine($"Pushed {gitEvent.Payload.Commits.Count} commit(s) to {gitEvent.Repo.Name}") },
                { "IssuesEvent", gitEvent => Console.WriteLine($"Opened a new issue in {gitEvent.Repo.Name}") },
                { "WatchEvent", gitEvent => Console.WriteLine($"Starred {gitEvent.Repo.Name}") }
            };

            Console.WriteLine("Output:");

            foreach (var gitEvent in events)
            {
                if (eventHandlers.TryGetValue(gitEvent.Type, out var handleEvent))
                {
                  
                    handleEvent(gitEvent);
                }
                else
                {
                   
                    Console.WriteLine($"Performed an event of type {gitEvent.Type} in {gitEvent.Repo.Name}");
                }
            }
        }
    }

    }
