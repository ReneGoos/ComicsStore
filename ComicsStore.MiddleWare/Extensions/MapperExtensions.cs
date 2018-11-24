using AutoMapper;
using System.Linq;

namespace ComicsStore.MiddleWare.Extensions
{
    public static class MapperExtensions
    {
        //mapper.MergeInto<PersonCar>(person, car)
        //with the accepted answer as extension-methods, simple and general version:

        public static TResult MergeInto<TResult>(this IMapper mapper, object item1, object item2)
        {
            return mapper.Map(item2, mapper.Map<TResult>(item1));
        }

        public static TResult MergeInto<TResult>(this IMapper mapper, params object[] objects)
        {
            var res = mapper.Map<TResult>(objects.First());
            return objects.Skip(1).Aggregate(res, (r, obj) => mapper.Map(obj, r));
        }
        //after configuring mapping for each input-type:
    }
}
