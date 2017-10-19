//-----------------------------------------------------------------------
// <copyright file="ILog.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Abstract
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public interface ILog<TService>
    {
        void Error(string methodName, Exception e);
        void Error(string methodName, object model, Exception e);
        void Info(string methodName, object model);
        void InfoFormat(string formattedString, params object[] args);
    }
}
