using System;
using System.Collections.Generic;
using System.Text;
using ChatApp.Core.BaseModel.BaseEntityDto;

namespace ChatApp.Bl.Dto
{
    public class KeyValueDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
