using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Surging.Core.Dapper.Manager
{
    public abstract class ManagerBase 
    {
        protected virtual DbConnection Connection
        {
            get
            {
                if (DbSetting.Instance == null)
                {
                    throw new Exception("未设置数据库连接");
                }
                DbConnection conn = null;
                switch (DbSetting.Instance.DbType)
                {
                    case DbType.MySql:
                        conn = new MySqlConnection(DbSetting.Instance.ConnectionString);
                        break;
                    case DbType.Oracle:
                        conn = new OracleConnection(DbSetting.Instance.ConnectionString);
                        break;
                    case DbType.SqlServer:
                        conn = new SqlConnection(DbSetting.Instance.ConnectionString);
                        break;
                }
                conn.Open();
                return conn;
            }
        }

        protected virtual void UnitOfWork(Action<DbConnection, DbTransaction> action, DbConnection conn)
        {
            var trans = conn.BeginTransaction();
            try
            {
                action(conn, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }

        protected virtual async Task UnitOfWorkAsync(Func<DbConnection, DbTransaction, Task> action, DbConnection conn)
        {
            var trans = conn.BeginTransaction();
            try
            {
                await action(conn, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }

    }
}
