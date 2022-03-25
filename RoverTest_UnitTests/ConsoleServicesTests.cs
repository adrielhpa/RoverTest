using NUnit.Framework;
using RoverTest.Model;
using RoverTest_Console;
using RoverTest_Service;
using System;
using System.Net.Http.Headers;

namespace RoverTest_UnitTests
{
    [TestFixture]
    public class ConsoleServicesTests
    {
        private Command command;
        private ConsoleServices consoleServices;

        [Test]
        public void ShowResult_IsValid_ReturnTrue()
        {
            consoleServices = new();
            command = new()
            {
                PlateauHeight = 2,
                PlateauWidth = 2,
                PositionHeight = 1,
                PositionWidth = 1,
                PositionDirection = "N",
                MovementCommand = "RML",
                IsValid = true,
                AfterCommand = new Command()
                {
                    PositionHeight = 1,
                    PositionWidth = 2,
                    PositionDirection = "N",
                }
            };

            var result = consoleServices.ShowResult(command, 1);

            Assert.IsTrue(result);
        }

        [Test]
        public void ShowResult_IsInvalid_ReturnFalse()
        {
            consoleServices = new();
            command = new()
            {
                PlateauHeight = 2,
                PlateauWidth = 2,
                PositionHeight = 1,
                PositionWidth = 1,
                PositionDirection = "N",
                MovementCommand = "MMM",
                IsValid = false,
                AfterCommand = new Command()
                {
                    PositionHeight = 1,
                    PositionWidth = 2,
                    PositionDirection = "N",
                }
            };

            var result = consoleServices.ShowResult(command, 1);

            Assert.IsFalse(result);
        }

        [Test]
        public void GetCommand_ParseCommand_ReturnParsedCommand()
        {
            consoleServices = new();
            StringCommand strCommand = new()
            {
                PlateauSize = new string[] {"4", "4"},
                Position = new string[] {"1", "2", "N"},
                Movement = "RML"
            };

            HttpClient client = new();
            client.BaseAddress = new Uri("https://localhost:7015/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var result = consoleServices.GetCommand(client, strCommand).Result;

            Assert.That(result.PlateauWidth, Is.EqualTo(4));
            Assert.That(result.PlateauHeight, Is.EqualTo(4));
            Assert.That(result.PositionHeight, Is.EqualTo(1));
            Assert.That(result.PositionWidth, Is.EqualTo(2));
            Assert.That(result.PositionDirection, Is.EqualTo("N"));
        }
    }
}