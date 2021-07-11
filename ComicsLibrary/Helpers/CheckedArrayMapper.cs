using ComicsLibrary.EditModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace ComicsLibrary.Helpers
{
    public static class CheckedArrayMapper<T> where T : Enum
    {
        public static ICollection<RoleType> GetCheckedList(ICollection<string> names)
        {
            var checks = new ObservableCollection<RoleType>();

            foreach (var value in typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                checks.Add(new RoleType
                {
                    Checked = names.Contains(value.Name),
                    Name = value.Name
                });
            }

            return checks;
        }

        public static ICollection<string> GetStringList(ICollection<RoleType> checks)
        {
            var values = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public);
            var names = new List<string>();

            foreach (var value in checks)
            {
                if ((value.Checked) && values.Any(x => x.Name.Equals(value.Name)))
                {
                    names.Add(value.Name);
                }
            }

            return names;
        }
    }
}
