// See https://aka.ms/new-console-template for more information

using RoverTest.Model;
using RoverTest_Console;
using System.Net.Http.Headers;

ConsoleServices consoleServices = new();

bool valid =true;

do
{
    var list = consoleServices.GetData();
    valid = await consoleServices.RunAsync(list);

}while(!valid);






