//-----------------------------------------------------------------------
// <copyright file="DownloadMailModel.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Mail
{
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class DownloadMailModel
    {
        public string BaseUrl { get; set; }
        public TokenContract Token { get; set; }
    }
}
