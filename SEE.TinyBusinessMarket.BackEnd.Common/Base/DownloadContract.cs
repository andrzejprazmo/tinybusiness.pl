//-----------------------------------------------------------------------
// <copyright file="DownloadContract.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Base
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class DownloadContract
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
