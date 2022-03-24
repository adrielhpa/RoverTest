using NUnit.Framework;
using RoverTest.Model;
using RoverTest_Service;
using System;

namespace RoverTest_UnitTests
{
    [TestFixture]
    public class CommandServiceTests
    {
        private Command command;
        private TranslateCommandService translateCommandService;

        [Test]
        public void SetCommand_CommandFaliures_ThrowsNullException()
        {
            string[] emptyArray = new string[] {null};
            string[] plateau = new string[] {"1", "2"};
            string[] position = new string[] {"1", "2", "N"};
            translateCommandService = new();

            var exceptionAllNull = Assert.Throws<ArgumentException>(() => translateCommandService.SetCommand(emptyArray, emptyArray, ""));
            Assert.AreEqual("Command has null values. Please check your input.", exceptionAllNull.Message);

            var exceptionTwoNull = Assert.Throws<ArgumentException>(() => translateCommandService.SetCommand(plateau, emptyArray, ""));
            Assert.AreEqual("Command has null values. Please check your input.", exceptionTwoNull.Message);

            var exceptionOneNull = Assert.Throws<ArgumentException>(() => translateCommandService.SetCommand(plateau, position, ""));
            Assert.AreEqual("Command has null values. Please check your input.", exceptionTwoNull.Message);

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

    }
}