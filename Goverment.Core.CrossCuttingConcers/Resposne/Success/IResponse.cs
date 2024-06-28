using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goverment.Core.CrossCuttingConcers.Resposne.Success
{
    public interface IResponse
    {
        public string? Message { get; set; }
        public bool Success { get; }
    }
}
