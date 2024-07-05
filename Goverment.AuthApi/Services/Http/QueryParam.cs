namespace Goverment.AuthApi.Services.Http
{
    public record  QueryParam:PathParam
    {
        public required string  QueryParamName { get; set; }

    }
    public record PathParam
    {
        public required string Data { get; set; }

    }
}
