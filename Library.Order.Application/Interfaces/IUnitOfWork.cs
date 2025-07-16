using System.Threading;
using System.Threading.Tasks;

namespace Library.Order.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}