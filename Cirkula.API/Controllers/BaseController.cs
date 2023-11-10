using Azure;
using Cirkula.API.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace Cirkula.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected DefaultResponse<object> response = new DefaultResponse<object>();

        public IActionResult BadRequestResult(string message = "")
        {
            response.StatusCode = StatusCodes.Status400BadRequest;
            response.Message = message;
            response.Error = true;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        public IActionResult OkResult(string message = "", object? data = null)
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.Message = message;
            response.Result = data;
            return StatusCode(StatusCodes.Status200OK, response);
        }

        public IActionResult NotFoundResult(string message = "")
        {
            response.StatusCode = StatusCodes.Status404NotFound;
            response.Message = message;
            response.Error = true;
            return StatusCode(StatusCodes.Status404NotFound, response);
        }
    }
}
