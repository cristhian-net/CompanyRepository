using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallengeApp.Utils
{
    public class IdHelper
    {
        /// <summary>
        /// Generates an Id based on a List and the name of the `object` property
        /// </summary>
        /// <param name="list"></param>
        /// <param name="nameOfProperty"></param>
        /// <returns></returns>
        public static int GenerateId(IEnumerable<object> list, string nameOfProperty)
        {

            return list.Count() == 0 ? 1 : list.Max(myObject => {
                var propertyInfo = myObject.GetType().GetProperty(nameOfProperty);
                var value = propertyInfo.GetValue(myObject, null);
                return (int)value;
            }) + 1;
        }
    }
}
