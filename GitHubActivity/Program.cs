using System;
using System.Threading.Tasks;

namespace GitHubActivity
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
            Console.WriteLine("Enter the GitHub username:");
            string username = Console.ReadLine();

            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Username cannot be empty.");
                return;
            }
            
            await GitHubService.GitTask(username);
        }
    }
}