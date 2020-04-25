using System;
using System.Collections.Generic;
using System.Text;
using ChatApp.Core.BaseModel.BaseEntity;

namespace ChatApp.Model.Entities
{
    public class KeyValue : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
