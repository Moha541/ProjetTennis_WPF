using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Task match1 = PlayMatchAsync("Match 1");
        Task match2 = PlayMatchAsync("Match 2");
        Task match3 = PlayMatchAsync("Match 3");

        await Task.WhenAll(match1, match2, match3);
    }

    static async Task PlayMatchAsync(string matchName)
    {
        await Task.Run(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{matchName} is playing...");
                Task.Delay(1000).Wait(); // Simulate time for playing the match
            }
        });
    }
}
