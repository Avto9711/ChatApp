using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Bl.Dto;
using ChatApp.Model.Contexts.ChatApp;
using ChatApp.Model.Entities;
using ChatApp.Model.UnitOfWorks;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class KeyValueController : BaseController<KeyValue, KeyValueDto>
    {
        public KeyValueController(IMapper mapper, IUnitOfWork<IChatAppDbContext> uow, IValidatorFactory validationFactory) : base(mapper, uow, validationFactory)
        {
        }
    }
}
