using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections;
using System.Collections.Generic;
using ValidationSample.Dto;

namespace ValidationSample.Validator
{
    public class GetListRequestDtoValidator : CustomAbstractValidator<GetListRequestDto>
    {
        public GetListRequestDtoValidator()
        {

            When(v => v.criteria != null, () =>
            {
                RuleFor(v => v.criteria.keyword)
                   .MinimumLength(3)
                   .WithMessage("Length can not be less than 3 characters.")
                   .MaximumLength(10)
                   .WithMessage("Length can not exceed than 10 characters.");

                RuleFor(v => v.criteria.dateRange)
                    .Must(v => v.Length == 2)
                    .WithMessage("Length must be 2.")
                    .DependentRules(() =>
                    {
                        RuleFor(v => v.criteria.dateRange).Custom(ValidateDateRange);
                    });
            });
        }
        private bool ValidateDateTime(string datetime)
        {
            DateTime calculateDatetime;
            return DateTime.TryParse(datetime, out calculateDatetime);
        }

        private void ValidateDateRange(string[] dateRange, CustomContext cctx)
        {
            string startDate = dateRange[0];
            string endDate = dateRange[1];

            IDictionary<string, string> errors = new Dictionary<string, string>();

            bool hasStartDate = !string.IsNullOrWhiteSpace(startDate);
            bool hasEndDate = !string.IsNullOrWhiteSpace(endDate);
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            if (hasStartDate)
            {
                string propName = "StartDate";

                if (!ValidateDateTime(startDate))
                {
                    errors.Add(propName, "StartDate format is incorrect.");
                }
                else if(currentDate.CompareTo(startDate) > 0)
                {
                    errors.Add(propName, "StartDate must be greater than current date.");
                }
            }

            if (hasEndDate)
            {
                string propName = "EndDate";

                if (!ValidateDateTime(endDate))
                {
                    errors.Add(propName, "EndDate format is incorrect.");
                }
                else if(currentDate.CompareTo(endDate) > 0)
                {
                    errors.Add(propName, "EndDate must be greater than current date.");
                }
            }

            if (errors.Count > 0 )
            {
                foreach(var e in errors)
                {
                    cctx.AddFailure(e.Key, e.Value);
                }

                return;
            }
           
            if(hasStartDate && hasEndDate && (startDate.CompareTo(endDate) >= 0))
            {
                cctx.AddFailure("StartDate", "EndDate must greater than StartDate.");
            }
        }

    }


}