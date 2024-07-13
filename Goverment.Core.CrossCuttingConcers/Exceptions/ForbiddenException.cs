

namespace Goverment.Core.CrossCuttingConcers.Exceptions
{
    public  class ForbiddenException :Exception
    {
        public ForbiddenException(string message) : base(message)
        {
        }
        public ForbiddenException():base("forbidden error")
        {

        }
    }
}
