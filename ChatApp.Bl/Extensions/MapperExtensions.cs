using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using ChatApp.Core.BaseModel.BaseEntity;
using ChatApp.Core.BaseModel.BaseEntityDto;

namespace ChatApp.Bl.Extensions
{
    public static class MapperExtensions
    {
        public static Profile _CreateMap_WithConventions_FromAssemblies<T, TDto>(this Profile profile, bool reverseMap = true) where T : class where TDto : class, IBaseEntityDto
        {
            Type source = typeof(T);
            Type destionation = typeof(TDto);

            var source_types = GetTypes(source);
            foreach (var item in source_types)
            {
                var types = GetTypes(destionation);

                var dto = types.FirstOrDefault(t => t.Name.ToLower() == $"{item.Name}Dto".ToLower());

                if (dto is null)
                    continue;

                var result = profile.CreateMap(item, dto);

                if (reverseMap)
                {
                    result = profile.CreateMap(dto, item);

                    foreach (var property in dto.GetProperties())
                    {
                        PropertyDescriptor descriptor = TypeDescriptor.GetProperties(dto)[property.Name];
                        ReadOnlyAttribute attribute = (ReadOnlyAttribute)descriptor.Attributes[typeof(ReadOnlyAttribute)];
                        if (attribute.IsReadOnly == true)
                            result.ForMember(property.Name, opt => opt.Ignore());
                    }
                }
            }
            return profile;
        }

        private static IEnumerable<Type> GetTypes(Type type)
        {
            Assembly asm = Assembly.GetAssembly(type);
            var types = asm.GetTypes().Where(t => t.Namespace == type.Namespace);
            return types;
        }
    }
}
