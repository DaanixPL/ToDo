using App.Application.Validators.ValidationMessages;
using FluentValidation;
using System.Globalization;
using System.Threading;

namespace App.Application.Validators.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> RequiredField<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage(ValidationMessage.Required(fieldName));
        }
        public static IRuleBuilderOptions<T, TProperty> RequiredField<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string fieldName)
             where TProperty : struct, IComparable, IComparable<TProperty>, IConvertible, IEquatable<TProperty>, IFormattable
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage(ValidationMessage.Required(fieldName));
        }
        public static IRuleBuilderOptions<T, string> MaxLength<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName, int max)
        {
            return ruleBuilder
                .MaximumLength(max)
                .WithMessage(ValidationMessage.TooLong(fieldName, max));
        }
        public static IRuleBuilderOptions<T, string> MinLength<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName, int min)
        {
            return ruleBuilder
                .MinimumLength(min)
                .WithMessage(ValidationMessage.TooShort(fieldName, min));
        }
        public static IRuleBuilderOptions<T, string> EmailFormat<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName)
        {
            return ruleBuilder
                .EmailAddress()
                .WithMessage(ValidationMessage.InvalidEmail(fieldName));
        }
        public static IRuleBuilderOptions<T, int> ValidId<T>(this IRuleBuilder<T, int> ruleBuilder, string fieldName)
        {
            return ruleBuilder
                .GreaterThan(0)
                .WithMessage(ValidationMessage.InvalidId(fieldName));
        }
        public static IRuleBuilderOptions<T, string> InvalidDate<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName)
        {
            return ruleBuilder
                .Must(dateString =>
                {
                    if (string.IsNullOrEmpty(dateString))
                    {
                        return true;
                    }

                    return DateTime.TryParseExact(
                        dateString,
                        "yyyy-MM-dd HH:mm:ss.fffffff",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out _);
                })
                .WithMessage(ValidationMessage.InvalidDate(fieldName));
        }
        public static IRuleBuilderOptions<T, string> NoWhiteSpaces<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName)
        {
            return ruleBuilder
                .Must(value => value != null && !value.Any(char.IsWhiteSpace))
                .WithMessage(ValidationMessage.WhiteSpaces(fieldName));
        }
        public static IRuleBuilderOptions<T, string> NoAlreadyExistsAsync<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            Func<string, CancellationToken, Task<bool>> existsFunc,
            string fieldName)
        {
            return ruleBuilder
                .MustAsync(async (value, cancellation) => await existsFunc(value, cancellation))
                .WithMessage(ValidationMessage.isUnique(fieldName));
        }
    }
}
