using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ChatApp.Core.BaseModel.BaseEntity;
using ChatApp.Core.IoC;
using ChatApp.Model.Contexts.ChatApp;
using ChatApp.Model.UnitOfWorks;

namespace ChatApp.Bl.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<TItem, TProperty> IsUnique<TItem, TProperty>(this IRuleBuilder<TItem, TProperty> ruleBuilder) where TItem : class, IBaseEntity
        {
            var uow = Dependency.ServiceProvider.GetService(typeof(IUnitOfWork<IChatAppDbContext>)) as IUnitOfWork<IChatAppDbContext>;
            return ruleBuilder.SetValidator(new UniqueValidator<TItem>(uow));
        }
    }
    public class UniqueValidator<T> : PropertyValidator where T : class, IBaseEntity
    {
        public IUnitOfWork<IChatAppDbContext> _uow { get; set; }

        public UniqueValidator(IUnitOfWork<IChatAppDbContext> uow)
          : base("{PropertyName} debe ser único")
        {
            _uow = uow;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            T editedItem = context.Instance as T;
            string newValue = context.PropertyValue?.ToString();
            string propertyName = context.PropertyName.GetPropertyName();
            PropertyInfo property = typeof(T).GetTypeInfo().GetDeclaredProperty(propertyName);

            IQueryable<string> _items = _uow.GetRepository<T>()
                .WhereAsNoTracking(x => x.Id != editedItem.Id)
                .Where(x => property.GetValue(x) != null)
                .Select(x => property.GetValue(x).ToString());

            bool result = _items.Contains(newValue);

            return result is false;
        }


    }
}
