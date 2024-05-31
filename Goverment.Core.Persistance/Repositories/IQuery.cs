using Goverment.Core.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.Repositories;

public interface IQuery<T>
{
    IQueryable<T> Query();

}