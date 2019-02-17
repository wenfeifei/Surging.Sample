using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using Surging.Core.Dapper;
using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace Hl.Core.Manager
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

        /// <summary>
        /// 使用事务无法自动生成Id,如果执行软删除、无法过滤软删除条件等
        /// 请通过DapperExtension的扩展方法或写Sql语句操作数据库
        /// </summary>
        /// <param name="action"></param>
        protected void UnitOfWork(Action<DbConnection, DbTransaction> action, DbConnection conn)
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
    }

}
