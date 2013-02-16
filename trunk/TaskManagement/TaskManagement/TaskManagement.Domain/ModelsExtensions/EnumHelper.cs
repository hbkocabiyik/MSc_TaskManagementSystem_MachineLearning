using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagement.Web.Models.ModelsExtensions
{
    public static class EnumHelper
    {
        public static IEnumerable<EnumEntity> GetItemsOf<T>() where T : struct
        {
            return from T enumEntity in Enum.GetValues(typeof(T))
                   select new EnumEntity {
                                             ID = Convert.ToInt32(enumEntity),
                                             Name = enumEntity.ToString()
                                         };
        }
    }
}