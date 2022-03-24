using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using RoverTest.Model;

namespace RoverTest_Console
{
    public class ConsoleServices
    {
        StringCommand strCommand = new();

        public void GetData()
        {
            Console.WriteLine("Please insert the command: ");

            strCommand.PlateauSize = Console.ReadLine().ToUpper().Split();
            strCommand.Position = Console.ReadLine().ToUpper().Split();
            strCommand.Movement = Console.ReadLine().ToUpper();
        }

        public async Task<bool> RunAsync()
        {
            HttpClient client = new();
            client.BaseAddress = new Uri("https://localhost:7015/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await GetCommand(client);
            var isValid = ShowResult(result);

            return isValid;
        }

        public bool ShowResult(Command command)
        {
            if (command.IsValid)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Plateau Size: h{command.PlateauHeight} x w{command.PlateauWidth}");
                Console.WriteLine($"Position: h{command.PositionHeight} and w{command.PositionWidth}");
                Console.WriteLine($"Movement: {command.MovementCommand}");
                Console.WriteLine();
                Console.WriteLine($"Final Position: h{command.AfterCommand.PositionHeight} and w{command.AfterCommand.PositionWidth} pointed to {command.AfterCommand.PositionDirection}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Rover would be off the plateau !! Review your movements command !");
                Console.ResetColor();
                return false;
            }

            return true;
        }

        public async Task<Command> GetCommand(HttpClient client)
        {
            Command command = null;
            HttpResponseMessage response = await client.PostAsJsonAsync("api/TranslateCommand", strCommand);

            if (response.IsSuccessStatusCode)
            {
                command = await response.Content.ReadAsAsync<Command>();
            }

            return command;
        }
    }
}
