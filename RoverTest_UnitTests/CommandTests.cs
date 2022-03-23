using NUnit.Framework;
using RoverTest.Model;

namespace RoverTest_UnitTests
{
    [TestFixture]
    public class CommandTests
    {
        private Command command;

        [Test]
        public void SetCommand_OneRover_IntPropertiesIsNotNullAndMovementAndPositionDirectionIsNotEmpty()
        {
            command = new Command();
            var plateauCommand = new string[]{"4", "5"};
            var positionCommand = new string[]{"1", "3", "s"};
            var movement = "lml";

            command.SetCommand(plateauCommand, positionCommand, movement);

            Assert.NotNull(command.PlateauHeight);
            Assert.NotNull(command.PlateauWidth);
            Assert.NotNull(command.PositionHeight);
            Assert.NotNull(command.PositionWidth);
            Assert.IsNotEmpty(command.PositionDirection);
            Assert.IsNotEmpty(command.MovementCommand);
        }

    }
}