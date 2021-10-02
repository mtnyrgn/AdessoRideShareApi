using System;
using System.Threading.Tasks;

namespace AdessoRideShare.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommmitAsync();

        void Commit();
    }
}
