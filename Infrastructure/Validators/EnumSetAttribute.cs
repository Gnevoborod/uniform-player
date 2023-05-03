using System.ComponentModel.DataAnnotations;
using uniform_player.Infrastructure.Exceptions;

namespace uniform_player.Infrastructure.Validators
{
    public class EnumSetAttribute: ValidationAttribute
    {
        private readonly Type testEnum;
        public EnumSetAttribute(Type @enum) 
        {
            if (!@enum.IsEnum)
                throw new ApiException(ExceptionEvents.EnumValidatorNotEnum);
            testEnum = @enum;
        }


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(Enum.IsDefined(testEnum,value))
                return ValidationResult.Success;
            throw new ApiException(ExceptionEvents.EnumValidatorValidationFailed);
        }
    }
}
