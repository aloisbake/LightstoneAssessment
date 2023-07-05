using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LightstoneAssessment
{
    public interface IReverseStringInput
    {
        string InsertInputData();
        void BuildOutputReversed(string input);
        string ReverseSentence(string line); // TODO fix this
    }

    public static class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                services.GetRequiredService<App>().Run(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            IHostBuilder CreateHostBuilder(string[] strings)
            {
                return Host.CreateDefaultBuilder()
                    .ConfigureServices((_, services) =>
                    {
                        services.AddSingleton<IReverseStringInput, ReverseStringInput>();
                        services.AddSingleton<App>();
                    });
            }
        }
    }

    public class App
    {
        private readonly IReverseStringInput _reverseStringInput;

        public App(IReverseStringInput reverseStringInput)
        {
            _reverseStringInput = reverseStringInput;
        }

        public void Run(string[] args)
        {
            string input = _reverseStringInput.InsertInputData();

            Console.Clear();

            _reverseStringInput.BuildOutputReversed(input);

            Console.ReadKey();
        }
    }

    public class ReverseStringInput : IReverseStringInput
    {
        public void BuildOutputReversed(string input)
        {
            Console.WriteLine("Input: \n" + input);
            var sr = new StringReader(input);

            int count = 0;
            string line;

            Console.WriteLine("\n Output: \n");

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            while ((line = sr.ReadLine()) != null)
            {
                count++;
                if (count == 0)
                {
                    Console.WriteLine(line);
                }
                else
                {
                    Console.WriteLine("Line {0}: {1}", count, ReverseSentence(line));
                }
            }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }

        public string InsertInputData()
        {
            Console.WriteLine("Console enter number of tests");
            int numOfTests = 0;
            int.TryParse(Console.ReadLine(), out numOfTests);

            var input = numOfTests + "\n";
            string innerInput = string.Empty;
            for (int i = 0; i < numOfTests; i++)
            {
                Console.WriteLine($"Enter test number {i + 1}");
                innerInput += Console.ReadLine() + "\n";
            }

            input = input += innerInput;
            return input;
        }

        public string ReverseSentence(string line)
        {
            string[] words = line.Split(' ');
            Array.Reverse(words);
            string wordsAnew = string.Join(" ", words);
            return wordsAnew;
        }
    }
}