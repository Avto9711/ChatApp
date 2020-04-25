using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Bl.Validators.Generic
{
    public interface IBaseValidator : IValidator
    {
        /// <summary>
        /// Este metodo se encarga de decidir cual RuleSet se se ejecutara, en base a una evaluacion del objeto enviado
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        string[] GetRuleSetName(object dto);
    }
}
