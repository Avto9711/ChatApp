using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using ChatApp.Bl.Dto;
using ChatApp.Bl.Validators.Generic;
using ChatApp.Bl.Extensions;
using ChatApp.Model.Entities;

namespace ChatApp.Bl.Validators
{
    public class KeyValueValidator : BaseValidator<KeyValueDto>
    {
        public KeyValueValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("El campo nombre es obligatorio.");

        }
    }
}
