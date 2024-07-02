
namespace Goverment.Core.Security.Entities.Interfaces
{
    public  interface  ICreatedTime
    {
        public DateTime? CreatedTime { get; set; }
    }

    public interface IModifiedTime
    {
        public DateTime? ModifiedTime { get; set; }
    }
    public interface IDeletedTime:ISoftDeleted
    {
        public DateTime? DeleteTime { get; set; }
    }
    public interface ISoftDeleted
    {
        public bool IsDelete { get; set; }
    }
}
