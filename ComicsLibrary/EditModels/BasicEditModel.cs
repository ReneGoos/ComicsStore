using ComicsLibrary.Core;
using ComicsLibrary.EditModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ComicsLibrary.EditModels
{
    public abstract class BasicEditModel : ObservableObject, IBasicEditModel
    {
        private DateTime _creationDate;
        private DateTime _dateUpdate;

        public BasicEditModel()
        {
        }

        public DateTime CreationDate { get => _creationDate; protected set => Set(ref _creationDate, value); }
        public DateTime DateUpdate { get => _dateUpdate; protected set => Set(ref _dateUpdate, value); }

        public virtual bool Validate(Dictionary<string, List<string>> errors)
        {
            var properties = GetType().GetProperties().Where(prop => prop.IsDefined(typeof(ValidationAttribute), false));
            foreach (var prop in properties)
            {
                errors[prop.Name] = [];
                foreach (var validation in (ValidationAttribute[])prop.GetCustomAttributes(typeof(ValidationAttribute), false))
                {
                    if (validation.IsValid(prop.GetValue(this)))
                    {
                        continue;
                    }
                    var dn = prop.Name;
                    foreach (var pa in (DisplayNameAttribute[])prop.GetCustomAttributes(typeof(DisplayNameAttribute), false))
                    {
                        dn = pa.DisplayName;
                    }
                    errors[prop.Name].Add(validation.FormatErrorMessage(dn));
                }
                if (errors[prop.Name].Count == 0)
                {
                    errors.Remove(prop.Name); 
                }
            }

            var collections = GetType().GetProperties().Where(prop => prop.PropertyType.GetInterfaces().Contains(typeof(System.Collections.IEnumerable)) && prop.PropertyType != typeof(String));
            foreach (var coll in collections)
            {
                Type type = coll.PropertyType;
                if (type.IsGenericType)
                {
                    Type itemType = type.GetGenericArguments()[0];
                    if (typeof(ICrossEditModel).IsAssignableFrom(itemType))
                    {
                        //errors[coll.Name] = new List<string>();
                        foreach (var element in (System.Collections.IEnumerable)coll.GetValue(this))
                        {
                            var crossEditModel = (ICrossEditModel)element;
                            if (!crossEditModel.Validate(errors)) {}
                        }
                    }
                }
            }

            return (errors.Count == 0);
        }
    }
}
