namespace Goverment.AuthApi.Services.Dtos.Response.Staff
{

    public class StaffResponse
    {
        public string? Fullname { get; set; }
        public  string Username { get; set; }
        public  string? OrganizationName { get; set; }
    }

    public class HttpResponse<TData>
    {
        public required TData Data { get; set; }
        public required string Message { get; set; }
        public required bool Success { get; set; }
    }

}
