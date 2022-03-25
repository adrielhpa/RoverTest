using NUnit.Framework;
using RoverTest.Model;
using RoverTest_Console;
using RoverTest_Service;
using System;
using System.Net.Http.Headers;

namespace RoverTest_UnitTests
{
    [TestFixture]
    public class CommandServiceTests
    {
        private Command command;
        private TranslateCommandService translateCommandService;
        private ConsoleServices consoleServices;

        #region Rover Movements
        [Test]
        public void ParseCommand_CommandFaliures_ThrowsArgumentException()
        {
            string[] emptyArray = new string[] {null};
            string[] plateau = new string[] {"1", "2"};
            string[] position = new string[] {"1", "2", "N"};
            translateCommandService = new();

            var exceptionAllNull = Assert.Throws<ArgumentException>(() => translateCommandService.ParseCommand(emptyArray, emptyArray, ""));
            Assert.AreEqual("Command has any empty values. Please check your input.", exceptionAllNull.Message);

            var exceptionTwoNull = Assert.Throws<ArgumentException>(() => translateCommandService.ParseCommand(plateau, emptyArray, ""));
            Assert.AreEqual("Command has any empty values. Please check your input.", exceptionTwoNull.Message);

            var exceptionOneNull = Assert.Throws<ArgumentException>(() => translateCommandService.ParseCommand(plateau, position, ""));
            Assert.AreEqual("Command has any empty values. Please check your input.", exceptionTwoNull.Message);

        }

        [Test]
        public void ParseCommand_WrongFormatCommand_ReturnErrorMessage()
        {
            string[] plateau = new string[] { "1", "y" };
            string[] position = new string[] { "t", "2", "N" };
            string movement = "1lk";
            translateCommandService = new();

            var result = translateCommandService.ParseCommand(plateau, position, movement);

            Assert.That(result.Error, Is.EqualTo("Command has invalid formats. Please check your input."));
        }

        [Test]
        public void DoingMovements_AllCompleted_ReturnPositionWidthEqualsTwo()
        {
            command = new()
            {
                PlateauHeight = 2,
                PlateauWidth = 2,
                PositionHeight = 1,
                PositionWidth = 1,
                PositionDirection = "N",
                MovementCommand = "RML",
                IsValid = true
            };

            translateCommandService = new();

            var result = translateCommandService.DoingMovements(command);

            Assert.That(result.PositionWidth, Is.EqualTo(2));
        }

        [Test]
        public void DoingMovements_AllCompletedWalkingRoverFoward_ReturnPositionHeightEqualsFour()
        {
            command = new()
            {
                PlateauHeight = 5,
                PlateauWidth = 5,
                PositionHeight = 1,
                PositionWidth = 2,
                PositionDirection = "N",
                MovementCommand = "MMM",
                IsValid = true
            };

            translateCommandService = new();

            var result = translateCommandService.DoingMovements(command);

            Assert.That(result.PositionHeight, Is.EqualTo(4));
        }

        [Test]
        public void DoingMovements_AllCompletedTurningRoverRight_ReturnPositionDirectionEqualsToWest()
        {
            command = new()
            {
                PlateauHeight = 5,
                PlateauWidth = 5,
                PositionHeight = 1,
                PositionWidth = 2,
                PositionDirection = "N",
                MovementCommand = "RRR",
                IsValid = true
            };

            translateCommandService = new();

            var result = translateCommandService.DoingMovements(command);

            Assert.That(result.PositionDirection, Is.EqualTo("W"));
        }

        [Test]
        public void DoingMovements_AllCompletedTurningRoverLeft_ReturnPositionDirectionEqualsToEast()
        {
            command = new()
            {
                PlateauHeight = 5,
                PlateauWidth = 5,
                PositionHeight = 1,
                PositionWidth = 2,
                PositionDirection = "N",
                MovementCommand = "LLL",
                IsValid = true
            };

            translateCommandService = new();

            var result = translateCommandService.DoingMovements(command);

            Assert.That(result.PositionDirection, Is.EqualTo("E"));
        }

        [Test]
        public void DoingMovements_AllCompletedWalkingRoverBack_ReturnPositionDirectionEqualsToSouthAndPositionHeightEqualsTwo()
        {
            command = new()
            {
                PlateauHeight = 5,
                PlateauWidth = 5,
                PositionHeight = 3,
                PositionWidth = 2,
                PositionDirection = "N",
                MovementCommand = "RRM",
                IsValid = true
            };

            translateCommandService = new();

            var result = translateCommandService.DoingMovements(command);

            Assert.That(result.PositionDirection, Is.EqualTo("S"));
            Assert.That(result.PositionHeight, Is.EqualTo(2));
        }
        #endregion

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