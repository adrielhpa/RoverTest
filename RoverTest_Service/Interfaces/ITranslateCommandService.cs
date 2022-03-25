using RoverTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverTest_Service.Interfaces
{
    public interface ITranslateCommandService
    {
        Command ParseCommand(string[] plateauCommand, string[] positionCommand, string movement);
        Command DoingMovements(Command command);
    }
}