using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ValidationSample.Attribute;

namespace ValidationSample.Dto
{
    public class GetListRequestDto
    {
        public Criteria criteria { get; set; }

        public GetListRequestDto()
        {
            criteria = new Criteria();
        }
    }

    public class Criteria
    {
        public string keyword { get; set; }
        [ErrorProperty(propertyName: "dateRange")]
        public string[] dateRange { get; set; }
        public string[] status { get; set; }

        public Criteria()
        {
            keyword = "";
            dateRange = new string[] { };
            status = new string[] { };
        }
    }

    public class GetListRequestDtoValidator : AbstractValidator<GetListRequestDto>
    {
        public GetListRequestDtoValidator()
        {
            ValidatorOptions.PropertyNameResolver = (type, member, lab) =>
            {
                if (member.CustomAttributes != null && member.CustomAttributes.Count() > 0)
                {
                    var errorPropertyAttributes = member.GetCustomAttributes(typeof(ErrorPropertyAttribute), false);
                    if(errorPropertyAttributes!=null && errorPropertyAttributes.Count() > 0)
                    {
                        return ((ErrorPropertyAttribute)errorPropertyAttributes[0]).propertyName;
                    }
                }
                return member.Name;
            };

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

        protected override void EnsureInstanceNotNull(object instanceToValidate)
        {
            base.EnsureInstanceNotNull(instanceToValidate);
        }

        public override FluentValidation.Results.ValidationResult Validate(ValidationContext<GetListRequestDto> context)
        {
            #region Return immediately if rule hit
            //IEnumerator<IValidationRule> e = this.GetEnumerator();
            //FluentValidation.Results.ValidationResult validationResult = new FluentValidation.Results.ValidationResult();



            //while (e.MoveNext())
            //{
            //    IValidationRule rule = e.Current;

            //    IEnumerable<ValidationFailure> result = rule.Validate(context);

            //    if (result.Count() > 0)
            //    {
            //        validationResult = new FluentValidation.Results.ValidationResult(result);
            //    }
            //}

            //return validationResult;
            #endregion

            return base.Validate(context); 
        }
    }
}
