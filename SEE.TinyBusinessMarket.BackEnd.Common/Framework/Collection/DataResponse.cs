using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEE.Framework.Core.Collection
{
    public class DataResponse<TRequest, TResponse> 
        where TRequest : DataRequest 
        where TResponse : class
    {
        public TRequest Request { get; set; }
        public List<TResponse> List { get; set; } = new List<TResponse>();

        public DataResponse() { }
        public DataResponse(TRequest request, List<TResponse> list)
        {
            Request = request;
            List = list;
        }
    }
}
