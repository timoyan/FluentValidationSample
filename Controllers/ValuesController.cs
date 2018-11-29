﻿using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ValidationSample.ActionResult;
using ValidationSample.Dto;
using ValidationSample.Validator;

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
        public IActionResult GetList([FromBody]GetListRequestDto request)
        {
            GetListRequestDtoValidator validator = new GetListRequestDtoValidator();
            return new CustomJson(validator.Validate(request));
        }

        [HttpGet("getlist")]
        public IActionResult GetList_CustomJsonResult()
        {
            IDictionary<int, string> result = new Dictionary<int, string>() {
                {1, "Timo"},
                {2, "Justin" },
                {3, "Susan" }
            };


            return new CustomJson(new CustomJsonResult()
            {
                Data = result
            });
        }

        [HttpPost("addpeople")]
        public IActionResult AddPeople([FromBody] CreatePeoplePayloadDto payload)
        {
            CreatePeoplePayloadDtoValidator validator = new CreatePeoplePayloadDtoValidator();
            return new CustomJson(validator.Validate(payload));
        }
    }
}
