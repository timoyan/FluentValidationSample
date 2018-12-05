using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using ValidationSample.Dto;

namespace ValidationSample.ActionResult
{
    /// <summary>
    /// Custom json spec action result
    /// </summary>
    /// <
    public class JsonSpecResult : IActionResult
    {
        private JsonSpecDto _result;

        public int? statusCode { get; set; }

        public JsonSpecResult()
        {
            _result = new JsonSpecDto();
        }

        /// <param name="validationResult"></param>
        public JsonSpecResult(ValidationResult validationResult):this()
        {
            // Handle error object if exist
            if (validationResult != null)
            {
                if (validationResult.Errors != null && validationResult.Errors.Count > 0)
                {
                    foreach (var e in validationResult.Errors)
                    {
                        _result.Error.Add(new JsonSpecErrorDto()
                        {
                            Title = e.PropertyName,
                            Detail = e.ErrorMessage
                        });
                    }
                }
            }

            // Handle statusCode if exist
            if (statusCode == null)
            {
                statusCode = validationResult.IsValid ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customJsonResult">Throw Exception if content is null</param>
        public JsonSpecResult(JsonSpecDto customJsonResult) : this()
        {
            _result = customJsonResult ?? throw new ArgumentNullException();

            // Handle statusCode if exist
            if(statusCode == null)
            {
                if (customJsonResult.Error == null || customJsonResult.Error.GetType().GetProperties().Length == 0)
                {
                    statusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    statusCode = (int)HttpStatusCode.BadRequest;
                }
            }
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var jsonResult = new JsonResult("")
            {
                StatusCode = statusCode,
                Value = _result
            };

            await jsonResult.ExecuteResultAsync(context);
        }
    }
}
