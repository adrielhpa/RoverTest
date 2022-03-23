using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoverTest.Model;
using RoverTest_Console;
using RoverTest_Service;
using RoverTest_Service.Interfaces;

namespace RoverTest_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslateCommandController : ControllerBase
    {
        private readonly Command command;
        private readonly ITranslateCommandService translateCommandService;

        public TranslateCommandController(ITranslateCommandService translateCommandService)
        {
            this.translateCommandService = translateCommandService;
        }

        [HttpPost]
        public Command TranslateCommand(StringCommand command)
        {
            try
            {
                var result = translateCommandService.SetCommand(command.PlateauSize, command.Position, command.Movement);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
