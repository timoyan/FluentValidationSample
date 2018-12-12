using FluentValidation;
using FluentValidation.Internal;
using System;
using ValidationSample.Dto;

namespace ValidationSample.Validator
{
    public class CreatePeoplePayloadDtoValidator : CustomAbstractValidator<CreatePeoplePayloadDto>
    {
        public CreatePeoplePayloadDtoValidator()
        {
            RuleFor(payload => payload.JoinDate)
                .NotEmpty()
                .WithMessage("JoinDate required!")
                .WithName("JoinTime")
                .DependentRules(() =>
                {
                    RuleFor(payload => payload.JoinHour)
                    .NotEmpty()
                    .WithMessage("JoinHour required!")
                    .WithName("JoinTime")
                    .DependentRules(() =>
                    {
                        RuleFor(payload => payload.JoinMin)
                        .NotEmpty()
                        .WithMessage("JoinMin required!")
                        .WithName("JoinTime")
                        .DependentRules(() =>
                        {
                            RuleFor(payload => ValidateDateTime(payload.JoinDate, payload.JoinHour, payload.JoinMin))
                            .Equal(true)
                            .WithMessage("JoinTime format is incorrect")
                            .WithName("JoinTime");
                        });
                    });
                });
        }

        private bool ValidateDateTime(string date, string hour, string min)
        {
            DateTime calculateDatetime;
            return DateTime.TryParse($"{date}T{hour}:{min}:00Z", out calculateDatetime);
        }
    }
}
