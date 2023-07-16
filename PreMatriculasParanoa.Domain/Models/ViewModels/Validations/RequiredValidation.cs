using PreMatriculasParanoa.Domain.Extensions;
using PreMatriculasParanoa.Domain.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PreMatriculasParanoa.Domain.Models.ViewModels.Validations
{
    public class RequiredValidation : ValidationAttribute
    {
        protected string FieldName { get; set; }

        public RequiredValidation(string fieldName)
        {
            FieldName = fieldName;
        }

        public string GetErrorMessage() => string.Format(ValidationMessageResource.CampoObrigatorio, FieldName);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(GetErrorMessage());

            var valueType = value.GetType();

            var isValid = true;

            if (valueType == typeof(int) || valueType == typeof(int?))
                isValid = ((int?)value).PossuiValor();

            if (valueType == typeof(string))
                isValid = ((string)value).PossuiValor();

            if (valueType == typeof(decimal) || valueType == typeof(decimal?))
                isValid = ((decimal?)value).PossuiValor();

            if (valueType == typeof(DateTime) || valueType == typeof(DateTime?))
                isValid = ((DateTime?)value).PossuiValor();

            if (!isValid)
                return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }
    }
}
