//-----------------------------------------------------------------------
// <copyright file="CollectionRequiredAttribute.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace System.ComponentModel.DataAnnotations
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class CollectionRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var collection = value as ICollection;
            if (collection != null && collection.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
