
namespace Goverment.Core.CrossCuttingConcers.Exceptions
{
    public  class UnVerifyOrDuplicatedException:Exception
    {
        public UnVerifyOrDuplicatedException(string message) : base(message)
        {
        }

        public UnVerifyOrDuplicatedException() : base("zehmet olmasa hesabinizi tediqleyin..")
        {
        }
    }
}
