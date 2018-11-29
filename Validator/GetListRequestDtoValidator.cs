using FluentValidation;
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
                   .WithMessage("Length can not exceed than 10 characters.")
                   .WithName((a) =>
                   {

                       return "1";
                   });

                RuleFor(v => v.criteria.dateRange)
                    .Must(v => v.Length == 2)
                    .WithMessage("Length must be 2.");
            });
        }
    }
}
