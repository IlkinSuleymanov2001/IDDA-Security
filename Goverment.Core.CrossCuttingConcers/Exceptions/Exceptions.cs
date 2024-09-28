using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goverment.Core.CrossCuttingConcers.Exceptions
{
    public  static   class Exceptions
    {


        /// <summary>
        /// Throws a BusinessException with the provided message.
        /// </summary>
        /// <param name="message">The message to include in the exception.</param>
        /// <exception cref="BusinessException">Always thrown when called.</exception>
        public static  BusinessException   ThrowBusineesException(string message)
        {
            throw   new BusinessException(message);
        }
    }
}
