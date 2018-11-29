using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using ValidationSample.Attribute;

namespace ValidationSample.Validator
{
    public class CustomAbstractValidator<T> : AbstractValidator<T>
    {
        public CustomAbstractValidator()
        {
            ValidatorOptions.PropertyNameResolver = (type, member, lab) =>
            {
                if (member != null)
                {
                    if (member.CustomAttributes != null && member.CustomAttributes.Count() > 0)
                    {
                        var errorPropertyAttributes = member.GetCustomAttributes(typeof(ErrorPropertyAttribute), false);
                        if (errorPropertyAttributes != null && errorPropertyAttributes.Count() > 0)
                        {
                            return ((ErrorPropertyAttribute)errorPropertyAttributes[0]).propertyName;
                        }
                    }
                    return member.Name;
                }

                return null;
            };
        }

        public override ValidationResult Validate(ValidationContext<T> context)
        {
            #region Return immediately if rule hit
            IEnumerator<IValidationRule> e = this.GetEnumerator();
            ValidationResult validationResult = new ValidationResult();

            while (e.MoveNext())
            {
                IValidationRule rule = e.Current;

                IEnumerable<ValidationFailure> result = rule.Validate(context);

                if (result.Count() > 0)
                {
                    validationResult = new ValidationResult(result);
                }
            }

            return validationResult;
            #endregion

            return base.Validate(context);
        }
    }
}
