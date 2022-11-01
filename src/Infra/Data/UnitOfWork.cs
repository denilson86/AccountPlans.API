using Infra.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Infra.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IDbConnection DbConnection { get; }

        public IDbTransaction? Transaction { get; private set; }

        public UnitOfWork(string connectionString) => DbConnection = new SqlConnection(connectionString);

        public void Begin()
        {
            Transaction = DbConnection.BeginTransaction();
        }

        public void Close()
        {
            DbConnection.Close();
        }

        public void Commit()
        {
            Transaction?.Commit();
            Dispose();
        }

        public void Dispose()
        {
            Transaction?.Dispose();
            Transaction = null;
        }

        public void Open()
        {
            DbConnection.Open();
        }

        public void Rollback()
        {
            Transaction?.Rollback();
            Dispose();
        }
    }
}