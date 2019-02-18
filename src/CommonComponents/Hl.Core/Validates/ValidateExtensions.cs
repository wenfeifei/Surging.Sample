
using FluentValidation.Results;
using Surging.Core.CPlatform.Exceptions;
using System.Text;

namespace Hl.Core.Validates
{
    public static class ValidateExtensions
    {
        public static void IsValidResult(this ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                var sb = new StringBuilder();
                foreach (var error in validationResult.Errors)
                {
                    sb.Append(error.ErrorMessage + "|");
                }
                var errorMessage = sb.ToString().Remove(sb.Length - 1);
                throw new ValidateException(errorMessage);
            }
        }
    }
}
