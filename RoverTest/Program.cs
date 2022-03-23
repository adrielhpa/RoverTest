// See https://aka.ms/new-console-template for more information

using RoverTest.Model;
using RoverTest_Console;
using System.Net.Http.Headers;

HttpClient client = new();

// Execution Code
Console.WriteLine("Please insert the command: ");

StringCommand strCommand = new();
strCommand.PlateauSize = Console.ReadLine().ToUpper().Split();
strCommand.Position = Console.ReadLine().ToUpper().Split();
strCommand.Movement = Console.ReadLine().ToUpper();

await RunAsync();


// Methods
void ShowResult(Command command)
{
    Console.WriteLine($"Plateau Size: W{command.PlateauWidth}xH{command.PlateauHeight}");
    Console.WriteLine($"Position: {command.PositionWidth} and {command.PositionHeight}");
    Console.WriteLine($"Movement that was done: {command.MovementCommand}");
    Console.ReadLine();
}

async Task RunAsync()
{
    client.BaseAddress = new Uri("https://localhost:7015/");
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));

    var result = await GetCommand();
    ShowResult(result);
} 

async Task<Command> GetCommand()
{
    Command command = null;
    HttpResponseMessage response = await client.PostAsJsonAsync("api/TranslateCommand", strCommand);

    if (response.IsSuccessStatusCode)
    {
        command = await response.Content.ReadAsAsync<Command>();
    }

    return command;
}




