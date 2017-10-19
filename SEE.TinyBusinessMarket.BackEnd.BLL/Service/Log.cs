//-----------------------------------------------------------------------
// <copyright file="Log.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Service
{
    using Microsoft.Extensions.Logging;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Abstract;
    using System;
	using System.Collections.Generic;
	using System.Text;
    using Newtonsoft.Json;
    using Microsoft.Extensions.Configuration;
    using Serilog;

    public class Log<TService> : ILog<TService>
    {
        public Log()
        {
        }

        public void Error(string methodName, Exception e)
        {
            Log.Logger.Fatal(e, methodName);
        }

        public void Error(string methodName, object model, Exception e)
        {
            string serializedObject = JsonConvert.SerializeObject(model);
            Log.Logger.Fatal(e, "Method: [{0}], Data: [{1}]", methodName, serializedObject);
        }

        public void Info(string methodName, object model)
        {
            string serializedObject = JsonConvert.SerializeObject(model);
            Log.Logger.Information("Method: [{0}], Data: [{1}]", methodName, serializedObject);
        }

        public void InfoFormat(string formattedString, params object[] args)
        {
            Log.Logger.Information(formattedString, args);
        }
    }
}
