/*
 *  Author: Nguyen Huu Tuan
 *  Create date: 22/01/2007
 *  Description: support class for query data.
 * 
 */

using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;

namespace TNGames.Core
{
    /// <summary>
    /// Summary description for DataService.
    /// </summary>
    public class Database
    {
        // The connection to a database of this data service.
        private IDbConnection _connection;

        // The command to execute query or non-query command on a database of this data service.
        private IDbCommand _command;

        // The data adapter to execute query on a database of this data service.
        private IDbDataAdapter _dataAdapter;

        public Database()
        {
            //string strCn = CM.Util.Properties.Settings.Default.DatabaseConnectionString;
            // string strCn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            string strCn = SessionFactory.GetCurrentSession().Connection.ConnectionString;

            _connection = new SqlConnection(strCn);

            _command = new SqlCommand();
            _command.Connection = _connection;

            _dataAdapter = new SqlDataAdapter();
            _dataAdapter.SelectCommand = _command;
        }

        public Database(string strConnection)
        {
            _connection = new SqlConnection(strConnection);

            _command = new SqlCommand();
            _command.Connection = _connection;

            _dataAdapter = new SqlDataAdapter();
            _dataAdapter.SelectCommand = _command;
        }

        public bool TestConnection()
        {
            try
            {
                _connection.Open();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Opens the connection of this data service.
        /// </summary>
        private void openConnection()
        {
            // Opens the connection if it is closed.
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
        }

        /// <summary>
        /// Closes the connection of this data service.
        /// </summary>
        private void closeConnection()
        {
            _connection.Close();
        }

        /// <summary>
        /// Gets a data table from executing the specified query string.
        /// </summary>
        /// <param name="strSql">The query string specified.</param>
        /// <returns>The data table.</returns>
        public DataTable GetDataTable(string strSql, CommandType cmdType)
        {
            try
            {
                openConnection();

                DataSet ds = new DataSet();

                _command.CommandText = strSql;
                _command.CommandType = cmdType;
                _dataAdapter.Fill(ds);

                closeConnection();

                return ds.Tables[0];
            }
            catch { throw; }
        }

        /// <summary>
        /// Gets a data table from executing the specified query string.
        /// </summary>
        /// <param name="strSql">The query string specified.</param>
        /// <returns>The data table.</returns>
        public DataTable GetDataTable(string strSql, CommandType cmdType, SqlParameter para)
        {
            try
            {
                openConnection();

                DataSet ds = new DataSet();

                _command.CommandText = strSql;
                _command.CommandType = cmdType;
                _command.Parameters.Add(para);
                _dataAdapter.Fill(ds);

                closeConnection();

                return ds.Tables[0];
            }
            catch { throw; }
        }

        /// <summary>
        /// Gets a data table from executing the specified query string.
        /// </summary>
        /// <param name="strSql">The query string specified.</param>
        /// <returns>The data table.</returns>
        public DataTable GetDataTable(string strSql, CommandType cmdType, params SqlParameter[] paras)
        {
            try
            {
                openConnection();

                DataSet ds = new DataSet();

                _command.CommandText = strSql;
                _command.CommandType = cmdType;
                foreach (SqlParameter para in paras)
                    _command.Parameters.Add(para);

                _dataAdapter.Fill(ds);

                closeConnection();

                return ds.Tables[0];
            }
            catch { throw; }
        }

        /// <summary>
        /// Gets a data table from executing the specified query string.
        /// </summary>
        /// <param name="queryString">The query string specified.</param>
        /// <returns>The data set.</returns>
        public DataSet GetDataSet(string queryString, CommandType cmdType)
        {
            try
            {
                openConnection();

                DataSet ds = new DataSet();

                _command.CommandText = queryString;
                _command.CommandType = cmdType;
                _dataAdapter.Fill(ds);

                return ds;
            }
            catch { throw; }
        }       
      
        /// <summary>
        /// Gets a data table from executing the specified query string.
        /// </summary>
        /// <param name="queryString">The query string specified.</param>
        /// <returns>The data set.</returns>
        public DataSet GetDataSet(string queryString, CommandType cmdType, params SqlParameter[] paras)
        {
            try
            {
                openConnection();

                DataSet ds = new DataSet();

                _command.CommandText = queryString;
                _command.CommandType = cmdType;
                foreach (SqlParameter para in paras)
                    _command.Parameters.Add(para);

                _dataAdapter.Fill(ds);
                closeConnection();
                return ds;
            }
            catch { throw; }
        }

        /// <summary>
        /// Executes the specified SQL string on the database.
        /// </summary>
        /// <param name="sqlString">The SQL string specified.</param>
        public int ExecuteQuery(string sqlString, CommandType cmdType)
        {
            int iError = 0;
            try
            {
                openConnection();
                _command.CommandText = sqlString;
                _command.CommandType = cmdType;
                iError = _command.ExecuteNonQuery();
            }
            catch { throw; }
            finally
            {
                // Closes the connection if there is no transaction.
                if (_command.Transaction == null)
                    closeConnection();
            }
            return iError;
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="strSql">The STR SQL.</param>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="paramList">The param list.</param>
        /// <returns></returns>
        public int ExecuteQuery(string strSql, CommandType cmdType, params SqlParameter[] paramList)
        {
            int iError = 0;
            try
            {
                openConnection();
                _command.CommandText = strSql;
                _command.CommandType = cmdType;
                for (int i = 0; i < paramList.Length; i++)
                {
                    _command.Parameters.Add(paramList[i]);
                }

                //_command.Parameters.Add(para);
                iError = _command.ExecuteNonQuery();
            }
            catch { throw; }
            finally
            {
                // Closes the connection if there is no transaction.
                if (_command.Transaction == null)
                    closeConnection();
            }
            return iError;
        }   
    }
}