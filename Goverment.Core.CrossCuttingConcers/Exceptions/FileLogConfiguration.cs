using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goverment.Core.CrossCuttingConcers.Exceptions
{
    public class FileLogConfiguration
    {
        public const string Connection = "SeriLog:FileLogConfiguration";
        public string FolderPath { get; set; } = string.Empty;
    }
    
}
