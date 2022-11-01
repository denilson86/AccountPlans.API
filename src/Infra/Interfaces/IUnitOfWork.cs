using System;
using System.Data;

namespace Infra.Interfaces
{
    public interface IUnitOfWork
    {
        IDbConnection DbConnection { get; }
        IDbTransaction? Transaction { get; }

        void Open();
        void Begin();
        void Commit();
        void Rollback();
        void Close();
    }
}
