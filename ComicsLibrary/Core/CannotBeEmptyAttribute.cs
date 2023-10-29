using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ComicsLibrary.Core;
[AttributeUsage(AttributeTargets.Property)]
public sealed class CannotBeEmptyAttribute : RequiredAttribute
{
    public override bool IsValid(object value)
    {
        return value is IEnumerable list && list.GetEnumerator().MoveNext();
    }
}