using RoverTest.Model;
using RoverTest_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverTest_Service
{
    public class TranslateCommandService : ITranslateCommandService
    {
        public Command SetCommand(string[] plateauCommand, string[] positionCommand, string movement)
        {
            Command command = new();

            command.PlateauHeight = Convert.ToInt32(plateauCommand[0]);
            command.PlateauWidth = Convert.ToInt32(plateauCommand[1]);
            
            command.PositionDirection = positionCommand[2];
            command.PositionHeight = Convert.ToInt32(positionCommand[0]);
            command.PositionWidth = Convert.ToInt32(positionCommand[1]);
            
            command.MovementCommand = movement;

            return command;
        }
    }
}
