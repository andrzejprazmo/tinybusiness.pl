//-----------------------------------------------------------------------
// <copyright file="CustomerRequest.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Request
{
    using SEE.Framework.Core.Collection;
    using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class CustomerRequest : DataRequest
    {
        public const string SortByCreated = "Created";
        public const string SortByName = "Name";
    }
}
