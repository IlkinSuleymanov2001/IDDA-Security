using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goverment.Core.CrossCuttingConcers.Resposne.Success
{
    public interface IDataResponse<Type> : IResponse
    {
        Type? Data { get; set; }
    }
}
