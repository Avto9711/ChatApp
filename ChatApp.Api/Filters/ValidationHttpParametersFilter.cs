using FluentValidation;
using FluentValidation.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ChatApp.Api.Controllers;
using ChatApp.Api.Models;
using ChatApp.Bl.Validators.Generic;

namespace ChatApp.Api.Filters
{
    public class ValidationHttpParametersFilter : ActionFilterAttribute
    {
        private readonly IServiceProvider serviceProvider;


        //public ValidationHttpParametersFilter()
        //{
        //    this.serviceProvider = serviceProvider;

        //}
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as IBaseController;
            if (controller != null)
            {
                var _validationFactory = controller._validationFactory;
                var parameters = context.ActionDescriptor.Parameters;
                foreach (var parameter in parameters)
                {
                    if (parameter.ParameterType.IsPrimitive) continue;
                    var parmeterValue = context.ActionArguments[parameter.Name];
                    var validator = _validationFactory.GetValidator(parameter.ParameterType);
                    if (validator != null)
                    {
                        var resultSet = ((IBaseValidator)validator).GetRuleSetName(parmeterValue);
                        var valueObject = parmeterValue ?? Activator.CreateInstance(parameter.ParameterType);
                        var validationResult = validator.Validate(new ValidationContext<object>(valueObject, new PropertyChain(), new RulesetValidatorSelector(resultSet)));

                        if (!validationResult.IsValid)
                        {
                            var result = controller.UnprocessableEntity(validationResult.Errors.Select(e => new ValidationFailedModel(e)));
                            context.Result = result;
                        }
                    }

                }
            }
           
            base.OnActionExecuting(context);
        }

    }
}
