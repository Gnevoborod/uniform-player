using System.ComponentModel.DataAnnotations;
using System.Text;
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
            if (value == null)
                throw new ApiException(ExceptionEvents.EnumValidatorValueIsNull);
            if(Enum.IsDefined(testEnum, value))
                return ValidationResult.Success;
            var enumNames = Enum.GetNames(testEnum);
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < enumNames.Length; i++)
            {
                var nextItem = enumNames[i] + (i + 1 == enumNames.Length ? "]" : ", ");
                sb.Append(nextItem);
            }
            throw new ApiException(ExceptionEvents.EnumValidatorValidationFailed,sb.ToString());
        }
    }
}
