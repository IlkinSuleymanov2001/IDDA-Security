
namespace Goverment.Core.CrossCuttingConcers.Exceptions
{
    public  class UnVerifyException:Exception
    {
        public UnVerifyException(string message) : base(message)
        {
        }

        public UnVerifyException() : base("zehmet olmasa hesabinizi tediqleyin..")
        {
        }
    }
}
