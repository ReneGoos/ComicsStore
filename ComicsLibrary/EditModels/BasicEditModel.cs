using ComicsLibrary.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ComicsLibrary.EditModels
{
    public abstract class BasicEditModel : ObservableObject, IDataErrorInfo
    {
        private DateTime _creationDate;
        private DateTime _dateUpdate;
        private string _error;

        public BasicEditModel()
        {
        }

        public DateTime CreationDate { get => _creationDate; protected set => Set(ref _creationDate, value); }
        public DateTime DateUpdate { get => _dateUpdate; protected set => Set(ref _dateUpdate, value); }

        public string Error { get => _error; set => Set(ref _error, value); }

        public virtual bool Validate()
        {
            var properties = GetType().GetProperties().Where(prop => prop.IsDefined(typeof(ValidationAttribute), false));
            foreach (var prop in properties)
            {
                Errors[prop.Name] = new List<string>();
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
                    Errors[prop.Name].Add(validation.FormatErrorMessage(dn));
                }
                if (Errors[prop.Name].Count == 0)
                {
                    _ = Errors.Remove(prop.Name);
                }
            }

            var collections = GetType().GetProperties().Where(prop => prop.PropertyType.GetInterfaces().Contains(typeof(System.Collections.IEnumerable)) && prop.PropertyType != typeof(String));
            foreach (var coll in collections)
            {
                Type type = coll.PropertyType;
                if (type.IsGenericType)
                {
                    Type itemType = type.GetGenericArguments()[0];
                    if (itemType.IsSubclassOf(typeof(CrossEditModel)))
                    {
                        Errors[coll.Name] = new List<string>();
                        foreach (var element in (System.Collections.IEnumerable)coll.GetValue(this))
                        {
                            var crossEditModel = (CrossEditModel)element;
                            if (!crossEditModel.Validate())
                            {
                                Errors[coll.Name].Add(crossEditModel.Error);
                            }
                        }
                        if (Errors[coll.Name].Count == 0)
                        {
                            _ = Errors.Remove(coll.Name);
                        }
                    } 
                }
            }

            if (Errors.Count == 0)
            {
                return true;
            }

            Error = SetError();
            return false;
        }

        private string SetError()
        {
            var builder = new StringBuilder();
            foreach (var error in Errors)
            {
                if (error.Value.Count > 0)
                {
                    foreach (var text in error.Value)
                    {
                        _ = builder.AppendLine(text);
                    }
                }
            }
            return builder.Length > 0 ? builder.ToString(0, builder.Length - 2) : builder.ToString();
        }

        public virtual string this[string columnName]
        {
            get
            {
                var modelClassProperties = TypeDescriptor.GetProperties(GetType());
                foreach (PropertyDescriptor prop in modelClassProperties)
                {
                    if (prop.Name != columnName)
                    {
                        continue;
                    }
                    Errors[columnName] = new List<string>();
                    foreach (var attribute in prop.Attributes)
                    {
                        if (!(attribute is ValidationAttribute))
                        {
                            continue;
                        }
                        var validation = attribute as ValidationAttribute;
                        if (validation.IsValid(prop.GetValue(this)))
                        {
                            continue;
                        }
                        var dn = prop.Name;
                        foreach (var pa in prop.Attributes.OfType<DisplayNameAttribute>())
                        {
                            dn = pa.DisplayName;
                        }
                        Errors[columnName].Add(validation.FormatErrorMessage(dn));
                        Error = SetError();
                        return validation.FormatErrorMessage(dn);
                    }
                }
                _ = Errors.Remove(columnName);
                Error = SetError();
                return null;
            }
        }

        internal Dictionary<string, List<string>> Errors { get; } = new();
    }
}
