using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Api.Models
{
    public class ValidationFailedModel
    {
        public ValidationFailedModel()
        {

        }
        public ValidationFailedModel(FluentValidation.Results.ValidationFailure validationFailure = null)
        {
            if (validationFailure != null)
            {
                PropertyName = validationFailure.PropertyName;
                ErrorCode = validationFailure.ErrorCode;
                ErrorMessage = validationFailure.ErrorMessage;
                PropertyValue = validationFailure.AttemptedValue;
                PropertyText = validationFailure.FormattedMessagePlaceholderValues["PropertyName"]?.ToString();
            }
        }
        public string PropertyName { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public object PropertyValue { get; set; }
        public string PropertyText { get; set; }
    }
}
