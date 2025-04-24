using Microsoft.AspNetCore.Mvc;

namespace TaskManagerApi.Models.Response
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }


        public static IActionResult OkResponse(string message)
        {
            var response = new BaseResponse
            {
                Success = true,
                Message = message
            };
            return new OkObjectResult(response);
        }

        public static IActionResult NotFoundResponse(string message)
        {
            var response = new BaseResponse
            {
                Success = false,
                Message = message
            };
            return new NotFoundObjectResult(response);
        }

        public static IActionResult SuccessResponse(string message, object data = null)
        {
            var response = new BaseResponse
            {
                Success = true,
                Message = message,
            };
            return new OkObjectResult(response);
        }

        public static IActionResult ErrorResponse(string message)
        {
            var response = new BaseResponse
            {
                Success = false,
                Message = message
            };
            return new ObjectResult(response)
            {
                StatusCode = 400
            };
        }
    }


}
