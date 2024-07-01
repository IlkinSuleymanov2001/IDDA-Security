using Core.Security.Entities;
using Goverment.Core.Persistance.Repositories;
using Goverment.Core.Security.TIme;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;

namespace Goverment.Core.Security.Entities.Audit
{
    public class UserAudit : IAudit
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsVerify { get; set; }
        public bool Status { get; set; }
        public bool IsDelete { get; set; }

        [Key]
        public int AuditId { get; set; }
        public string UserName { get; set; }
        public string? Method  { get; set; }
        public string Action  { get; set; }
        public DateTime Timestamp { get; set; }

        public UserAudit(EntityEntry entityEntry,string username,IHttpContextAccessor http)
        {
                var user = (User)entityEntry.Entity;
                UserId = user.Id;
                IsVerify = user.IsVerify;
                Email = user.Email;
                FullName = user.FullName;
                Status = user.Status;
                IsDelete = user.IsDelete;

                UserName = string.IsNullOrEmpty(username) ? user.Email : username;
                Timestamp = Date.UtcNow;
                Method = http.HttpContext.Request.Method;
                Action = http.HttpContext.Request.Path.Value;

        }

        public UserAudit()
        {
            
        }
    }


    public interface IAudit
    {
        public int AuditId { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string? Method { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public interface IAuditEntity { }
}
