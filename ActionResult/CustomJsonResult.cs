using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ValidationSample.ActionResult
{
    public class CustomJsonResult
    {
        public object Data { get; set; }
        public object Error { get; set; }
        public object Meta { get; set; }

        public CustomJsonResult()
        {
            Data = new object();
            Error = new object();
            Meta = new object();
        }
    }

    public class CustomJson : IActionResult
    {
        private readonly object _result;

        public CustomJson(ValidationResult validationResult)
        {
            _result = validationResult;
        }

        public CustomJson(CustomJsonResult customJsonResult)
        {
            _result = customJsonResult;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var jsonResult = new JsonResult("")
            {
                StatusCode = GetStatusCode(_result),
                Value = GetValueObject(_result)
            };

            await jsonResult.ExecuteResultAsync(context);
        }

        private CustomJsonResult GetValueObject(object result)
        {
            CustomJsonResult jsonResult = new CustomJsonResult()
            {
                Data = new object(),
                Error = new object(),
                Meta = new object()
            };

            var resultType = result.GetType();

            if (resultType == typeof(ValidationResult))
            {
                var validationResult = (ValidationResult)result;
                if (validationResult != null)
                {
                    if (validationResult.Errors != null && validationResult.Errors.Count > 0)
                    {

                        ExpandoObject errorObject = new ExpandoObject();

                        foreach (var e in validationResult.Errors)
                        {
                            AddProperty(errorObject, e.PropertyName, e.ErrorMessage);
                        }

                        jsonResult.Error = errorObject;
                    }
                }

                return jsonResult;
            }
            else if (resultType == typeof(CustomJsonResult))
            {
                var customJsonResult = (CustomJsonResult)result;
                if (customJsonResult == null)
                {
                    throw new ArgumentNullException();
                }

                return customJsonResult;
            }


            throw new NotImplementedException();
        }

        private int? GetStatusCode(object result)
        {
            // TODO: Need to clarify does it need to return other status code.

            var resultType = result.GetType();
            if (resultType == typeof(ValidationResult))
            {
                return ((ValidationResult)result).IsValid ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest;
            }
            else if (resultType == typeof(CustomJsonResult))
            {
                var customJsonResult = (CustomJsonResult)result;

                if(customJsonResult.Error == null || customJsonResult.Error.GetType().GetProperties().Length == 0)
                {
                    return (int)HttpStatusCode.OK;
                }

                return (int)HttpStatusCode.BadRequest;
            }

            throw new NotImplementedException();
        }

        private void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
            {
                expandoDict[propertyName] = propertyValue;
            }
            else
            {
                expandoDict.Add(propertyName, propertyValue);
            }
        }
    }
}
