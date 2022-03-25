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
        public List<StringCommand> GetData()
        {
            var list = new List<StringCommand>();

            Console.WriteLine("Please insert the command: ");

            StringCommand strCommand = new();

            strCommand.PlateauSize = Console.ReadLine().ToUpper().Split();
            strCommand.Position = Console.ReadLine().ToUpper().Split();
            strCommand.Movement = Console.ReadLine().ToUpper();

            list.Add(strCommand);

            StringCommand strCommand2 = new();

            strCommand2.PlateauSize = strCommand.PlateauSize;
            strCommand2.Position = Console.ReadLine().ToUpper().Split();

            if (strCommand2.Position[0] != "")
            {
                strCommand2.Movement = Console.ReadLine().ToUpper();
                list.Add(strCommand2);
            }

            return list;
        }

        public async Task<bool> RunAsync(List<StringCommand> listStrCommand)
        {
            var listCommand = listStrCommand.ToArray();
            var verification = true;
            for (var i = 0; i < listCommand.Length; i++)
            {
                HttpClient client = new();
                client.BaseAddress = new Uri("https://localhost:7015/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await GetCommand(client, listStrCommand[i]);

                var isValid = ShowResult(result, i);

                verification = isValid;
            }

            return verification;
        }

        public bool ShowResult(Command command, int rover)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Rover {rover+1}");
            Console.WriteLine("-----------------");
            Console.ResetColor();
            Console.WriteLine();

            if (command.IsValid)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Plateau Size: h{command.PlateauHeight} x w{command.PlateauWidth}");
                Console.WriteLine($"Position: h{command.PositionHeight} and w{command.PositionWidth} pointed to {command.PositionDirection}");
                Console.WriteLine($"Movement: {command.MovementCommand}");
                Console.WriteLine();
                Console.WriteLine($"Final Position: h{command.AfterCommand.PositionHeight} and w{command.AfterCommand.PositionWidth} pointed to {command.AfterCommand.PositionDirection}");
                Console.WriteLine();
                Console.ResetColor();
            }
            else if (command.Error != "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(command.Error);
                Console.ResetColor();
                return false;
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

        public async Task<Command> GetCommand(HttpClient client, StringCommand str)
        {
            Command command = null;
            HttpResponseMessage response = await client.PostAsJsonAsync("api/TranslateCommand", str);

            if (response.IsSuccessStatusCode)
            {
                command = await response.Content.ReadAsAsync<Command>();
            }

            return command;
        }
    }
}
