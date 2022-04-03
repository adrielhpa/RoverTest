using RoverTest.Model;
using RoverTest_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RoverTest_Service
{
    public class TranslateCommandService : ITranslateCommandService
    {
        public Command ParseCommand(string[] plateauCommand, string[] positionCommand, string movement)
        {
            if (plateauCommand.Length == 0 || positionCommand.Length == 0 || movement == "")
            {
                throw new ArgumentException("Command has any empty values. Please check your input.");
            }

            var regexPlateau = @"^[1-9]$";
            var regexPosition = @"^[1-9NWES]$";
            var regexMovement = @"^[MRL]+$";
            bool matchPlateau = true;
            bool matchPosition = true;

            foreach (var item in plateauCommand)
            {
                if (item != "")
                    matchPlateau = Regex.IsMatch(item, regexPlateau);
                if (!matchPlateau)
                    break;
            }

            foreach (var item in positionCommand)
            {
                if (item != "")
                    matchPosition = Regex.IsMatch(item, regexPosition);
                if (!matchPosition)
                    break;
            }

            var matchMovement = Regex.IsMatch(movement, regexMovement);

            Command command = new();

            if (!matchPlateau || !matchPosition || !matchMovement)
            {
                command.IsValid = false;
                command.Error = "Command has invalid formats. Please check your input.";
            }

            if (command.Error == "")
            {
                command.PlateauHeight = Convert.ToInt32(plateauCommand[0]);
                command.PlateauWidth = Convert.ToInt32(plateauCommand[1]);

                command.PositionDirection = positionCommand[2];
                command.PositionHeight = Convert.ToInt32(positionCommand[0]);
                command.PositionWidth = Convert.ToInt32(positionCommand[1]);

                command.MovementCommand = movement;

                command.AfterCommand = DoingMovements(command);
            }

            return command;
        }

        public Command DoingMovements(Command command)
        {
            Command afterCommand = new()
            {
                PlateauHeight = command.PlateauHeight,
                PlateauWidth = command.PlateauWidth,
                PositionDirection = command.PositionDirection,
                PositionHeight = command.PositionHeight,
                PositionWidth = command.PositionWidth,
                IsValid = true
            };

            var movement = command.MovementCommand.ToCharArray();
            afterCommand.PositionDirection = command.PositionDirection;
            afterCommand.PositionHeight = command.PositionHeight;
            afterCommand.PositionWidth = command.PositionWidth;

            for (int i = 0; i < movement.Length; i++)
            {
                if (movement[i] == 'L')
                {
                    if (afterCommand.PositionDirection == "N")
                        afterCommand.PositionDirection = "W";
                    else if (afterCommand.PositionDirection == "W")
                        afterCommand.PositionDirection = "S";
                    else if (afterCommand.PositionDirection == "S")
                        afterCommand.PositionDirection = "E";
                    else if (afterCommand.PositionDirection == "E")
                        afterCommand.PositionDirection = "N";
                }

                if (movement[i] == 'R')
                {
                    if (afterCommand.PositionDirection == "N")
                        afterCommand.PositionDirection = "E";
                    else if (afterCommand.PositionDirection == "E")
                        afterCommand.PositionDirection = "S";
                    else if (afterCommand.PositionDirection == "S")
                        afterCommand.PositionDirection = "W";
                    else if (afterCommand.PositionDirection == "W")
                        afterCommand.PositionDirection = "N";
                }

                if (movement[i] == 'M')
                {
                    if (afterCommand.PositionDirection == "N")
                        afterCommand.PositionHeight += 1;
                    else if (afterCommand.PositionDirection == "S")
                        afterCommand.PositionHeight -= 1;
                    else if (afterCommand.PositionDirection == "E")
                        afterCommand.PositionWidth += 1;
                    else if (afterCommand.PositionDirection == "W")
                        afterCommand.PositionWidth -= 1;
                }

                if (afterCommand.PositionWidth <= 0 || afterCommand.PositionHeight <= 0 || afterCommand.PositionWidth > command.PlateauWidth || afterCommand.PositionHeight > command.PlateauHeight)
                {
                    command.IsValid = false;
                    afterCommand.IsValid = false;
                }
            }
            return afterCommand;
        }
    }
}
