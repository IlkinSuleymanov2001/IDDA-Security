using Core.Persistence.Paging;

namespace Goverment.AuthApi.Business.Dtos.Response.User
{
    public class PaginingGetListUserResponse : BasePageableModel
    {
        public IList<UserListResponse> Items { get; set; }
    }
}
