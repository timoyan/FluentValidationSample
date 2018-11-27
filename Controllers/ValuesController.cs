using System.Collections.Generic;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ValidationSample.Dto;

namespace ValidationSample.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Index()
        {
            return "1";
        }

        [HttpPost("getlist")]
        public ActionResult<FluentValidation.Results.ValidationResult> GetList([FromBody]GetListRequestDto request)
        {
            GetListRequestDtoValidator validator = new GetListRequestDtoValidator();
            var validationResult = validator.Validate(request);
            return validationResult;
        }
    }
}
