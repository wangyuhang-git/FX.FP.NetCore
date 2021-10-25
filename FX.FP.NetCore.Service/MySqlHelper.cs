using FX.FP.NetCore.Common;
using FX.FP.NetCore.Common.DotNetData;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX.FP.NetCore.Service
{
    public class MySqlHelper : IDisposable
    {
        /// <summary>
        /// 批量操作每批次记录数
        /// </summary>
        protected static int BatchSize = 2000;

        /// <summary>
        /// 超时时间
        /// </summary>
        protected static int CommandTimeOut = 600;

        protected MySqlConnection connection;
        private string conntionStr;
        public MySqlHelper(string connStr = "DefaultConnection")
        {
            conntionStr = ConfigHelper.GetSectionValue($"ConnectionStrings:{connStr}");
            connection = new MySqlConnection(conntionStr);
        }

        public async Task InsertAsync()
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO `BlogPost` (`Title`, `Content`) VALUES (@title, @content)";
            command.Parameters.Add(new MySqlParameter()
            {
                ParameterName = "@title",
                DbType = DbType.String,
                Value = ""
            });
            await command.ExecuteNonQueryAsync();
        }

        /// <summary> 
        /// 执行一个sql命令（不返回数据集） 
        /// </summary> 
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns>执行命令所影响的行数</returns> 
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = connection.CreateCommand();
            using (connection)
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }


        /// <summary> 
        /// 异步执行一个sql命令（不返回数据集） 
        /// </summary> 
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns>执行命令所影响的行数</returns> 
        public async Task<int> ExecuteNonQueryAsync(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = connection.CreateCommand();
            using (connection)
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                int val = await cmd.ExecuteNonQueryAsync();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary> 
        ///使用现有的SQL事务执行一个sql命令（不返回数据集） 
        /// </summary> 
        /// <remarks> 
        ///举例: 
        /// int result = ExecuteNonQuery(CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24)); 
        /// </remarks> 
        /// <param name="trans">一个现有的事务</param> 
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns>执行命令所影响的行数</returns> 
        public int ExecuteNonQuery(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = connection.CreateCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }


        /// <summary> 
        ///使用现有的SQL事务异步执行一个sql命令（不返回数据集） 
        /// </summary> 
        /// <remarks> 
        ///举例: 
        /// int result =await ExecuteNonQueryAsync(CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24)); 
        /// </remarks> 
        /// <param name="trans">一个现有的事务</param> 
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns>执行命令所影响的行数</returns> 
        public async Task<int> ExecuteNonQueryAsync(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = connection.CreateCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = await cmd.ExecuteNonQueryAsync();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary> 
        /// 返回数据集的sql命令 
        /// </summary> 
        /// <remarks> 
        /// 举例: 
        /// MySqlDataReader r = ExecuteReader(CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24)); 
        /// </remarks> 
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns>包含结果的读取器</returns> 
        public MySqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = connection.CreateCommand();
            try
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return reader;
            }
            catch (Exception ex)
            {
                connection.Close();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 用指定的数据库连接字符串执行一个命令并返回一个数据表 
        /// </summary>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        public DataTable GetDataTable(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = connection.CreateCommand();
            try
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();

                adapter.Fill(dt);
                cmd.Parameters.Clear();
                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary> 
        /// 执行一个命令并返回一个数据集的第一列 
        /// </summary> 
        /// <remarks> 
        ///例如:  Object obj = ExecuteScalar(CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24)); 
        /// </remarks> 
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns> 
        public object ExecuteScalar(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = connection.CreateCommand();
            using (connection)
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary> 
        /// 异步执行一个命令并返回一个数据集的第一列 
        /// </summary> 
        /// <remarks> 
        ///例如: 
        /// Object obj = ExecuteScalar(CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24)); 
        /// </remarks> 
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns> 
        public async Task<object> ExecuteScalarAsync(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = connection.CreateCommand();
            using (connection)
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = await cmd.ExecuteScalarAsync();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 返回插入值ID
        /// </summary>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns></returns>
        public long ExecuteNonExist(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = connection.CreateCommand();
            using (connection)
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteNonQuery();
                return cmd.LastInsertedId;
            }
        }


        /// <summary>
        /// 异步返回插入值ID
        /// </summary>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns></returns>
        public async Task<long> ExecuteNonExistAsync(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = connection.CreateCommand();
            using (connection)
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = await cmd.ExecuteNonQueryAsync();
                return cmd.LastInsertedId;
            }
        }

        /// <summary> 
        /// 返回DataSet 
        /// </summary>  
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns></returns> 
        public DataSet GetDataSet(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = connection.CreateCommand();
            try
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();

                adapter.Fill(ds);
                cmd.Parameters.Clear();
                connection.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据sql语句获取DataTable
        /// </summary>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <returns>数据集DataTable</returns>
        public DataTable GetDataTableBySQL(CommandType cmdType, string cmdText)
        {
            return GetDataTableBySQL(cmdType, cmdText, null);
        }

        /// <summary>
        /// 根据sql语句及参数集合获取DataTable
        /// </summary>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns>数据集DataTable</returns>
        public DataTable GetDataTableBySQL(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            DataTable result;
            try
            {
                result = ReaderToIListHelper.IDataReaderToDataTable(ExecuteReader(cmdType, cmdText, commandParameters));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 根据sql语句进行分页获取dataTable
        /// </summary>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="orderByStr">排序字段 asc,排序字段 desc</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="commandParameters">参数</param>
        /// <param name="count">返回的数量</param>
        /// <returns>符合条件的DataTable数据集</returns>
        public DataTable GetPageList(CommandType cmdType, string cmdText, string orderByStr, int pageIndex, int pageSize, MySqlParameter[] commandParameters, ref int count)
        {
            DataTable result;
            try
            {
                int num = (pageIndex - 1) * pageSize;
                string countSql = $"SELECT COUNT(1) FROM ({cmdText}) AS T";//查询数量的语句
                object obj = this.ExecuteScalar(cmdType, countSql, commandParameters);
                if (null != obj && !Convert.IsDBNull(obj))
                {
                    count = Convert.ToInt32(obj);
                }
                string sql = $"SELECT * FROM ({cmdText}) AS T ORDER BY {orderByStr} LIMIT {num},{pageSize}";
                result = this.GetDataTableBySQL(cmdType, cmdText, commandParameters);
            }
            catch (Exception ex)
            {
                result = null;
                //throw new Exception(ex.Message + ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// 根据sql语句获取Model数据列表
        /// </summary>
        /// <typeparam name="T">要转换的model类型</typeparam>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <returns></returns>
        public T GetModelBySql<T>(CommandType cmdType, string cmdText) where T : class, new()
        {
            return GetModelBySql<T>(cmdType, cmdText, null);
        }

        /// <summary>
        /// 根据sql语句和参数集合获取List数据列表
        /// </summary>
        /// <typeparam name="T">要转换的model类型</typeparam>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns></returns>
        public T GetModelBySql<T>(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters) where T : class, new()
        {
            return ReaderToIListHelper.IDataReaderToModel<T>(ExecuteReader(cmdType, cmdText, commandParameters));
        }

        /// <summary>
        /// 根据sql语句获取List数据列表
        /// </summary>
        /// <typeparam name="T">要转换的model类型</typeparam>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <returns></returns>
        public IList<T> GetListBySQL<T>(CommandType cmdType, string cmdText) where T : class, new()
        {
            return GetListBySQL<T>(cmdType, cmdText, null);
        }

        /// <summary>
        /// 根据sql语句和参数集合获取List数据列表
        /// </summary>
        /// <typeparam name="T">要转换的model类型</typeparam>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns></returns>
        public IList<T> GetListBySQL<T>(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters) where T : class, new()
        {
            return ReaderToIListHelper.IDataReaderToList<T>(ExecuteReader(cmdType, cmdText, commandParameters));
        }

        /// <summary>
        /// 根据Hashtable新增数据
        /// </summary>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等</param>
        /// <param name="tableName">数据表名称</param>
        /// <param name="ht">Hashtable包含要新增的字段</param>
        /// <returns></returns>
        public int InsertByHashtable(CommandType cmdType, string tableName, Hashtable ht)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" INSERT INTO ");
            sb.Append(tableName.ToUpper());
            sb.Append("(");
            StringBuilder sp = new StringBuilder();
            StringBuilder sb_prame = new StringBuilder();
            foreach (string key in ht.Keys)
            {
                sb_prame.Append("," + key.ToUpper());
                sp.Append(",@" + key);
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") VALUES (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
            return ExecuteNonQuery(cmdType, sb.ToString(), GetSqlParameter(ht));
        }

        /// <summary>
        /// 根据Hashtable和关键字段更新数据
        /// </summary>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等</param>
        /// <param name="tableName">数据表名称</param>
        /// <param name="pkName">数据表更新条件字段</param>
        /// <param name="pkVal">数据表更新条件字段值</param>
        /// <param name="ht">Hashtable包含要更新的字段</param>
        /// <returns></returns>
        public int UpdateByHashtable(CommandType cmdType, string tableName, string pkName, string pkVal, Hashtable ht)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE ");
            sb.Append(tableName.ToUpper());
            sb.Append(" SET ");
            bool isFirstValue = true;
            foreach (string key in ht.Keys)
            {
                if (isFirstValue)
                {
                    isFirstValue = false;
                    sb.Append(key.ToUpper());
                    sb.Append("=");
                    sb.Append("@" + key);
                }
                else
                {
                    sb.Append("," + key.ToUpper());
                    sb.Append("=");
                    sb.Append("@" + key);
                }
            }
            sb.Append(" WHERE ").Append(pkName.ToUpper()).Append("=").Append("@" + pkName);
            ht[pkName] = pkVal;
            return ExecuteNonQuery(cmdType, sb.ToString(), GetSqlParameter(ht));
        }

        /// <summary>
        /// 提交数据，不存在添加，存在则更新
        /// </summary>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等</param>
        /// <param name="tableName">数据表名称</param>
        /// <param name="pkName">数据表更新条件字段</param>
        /// <param name="pkVal">数据表更新条件字段值</param>
        /// <param name="ht">Hashtable包含要更新的字段</param>
        /// <returns></returns>
        public bool Submit_AddOrEdit(CommandType cmdType, string tableName, string pkName, string pkVal, Hashtable ht)
        {
            bool result;
            if (string.IsNullOrEmpty(pkVal))
            {
                result = (InsertByHashtable(cmdType, tableName, ht) > 0);
            }
            else
            {
                result = (UpdateByHashtable(cmdType, tableName, pkName, pkVal, ht) > 0);
            }
            return result;
        }

        /// <summary>
        ///使用MySqlDataAdapter批量更新数据
        /// </summary>
        /// <param name="table">dataTable数据表</param>
        /// <returns>返回成功的更新数量</returns>
        public int BatchUpdate(DataTable table)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandTimeout = CommandTimeOut;
            command.CommandType = CommandType.Text;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            MySqlCommandBuilder commandBulider = new MySqlCommandBuilder(adapter);
            commandBulider.ConflictOption = ConflictOption.OverwriteChanges;
            MySqlTransaction transaction = null;

            int updateCount = 0;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                //设置批量更新的每次处理条数
                adapter.UpdateBatchSize = BatchSize;
                //设置事务
                adapter.SelectCommand.Transaction = transaction;

                if (table.ExtendedProperties["SQL"] != null)
                {
                    adapter.SelectCommand.CommandText = table.ExtendedProperties["SQL"].ToString();
                }
                updateCount = adapter.Update(table);
                transaction.Commit();/////提交事务
            }
            catch (MySqlException ex)
            {
                if (transaction != null) transaction.Rollback();
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return updateCount;
        }

        /// <summary>
        ///大批量数据插入,返回成功插入行数
        ///1、MySQL配置默认不开启允许本地导入文件的设置，设置如下：
        ///  1）SET GLOBAL local_infile=1；//1表示开启，0表示关闭
        ///     SHOW VARIABLES LIKE '%local%';设置后通过该语句查看，值为ON证明已开启
        ///  2）数据库连接字符串要加上"AllowLoadLocalInfile=true"
        ///2、数据库和项目站点在不同的服务器上，会提示找不到数据集文件，导致错误，
        ///   因为项目中将数据集生成的文件保存在了项目所在的服务器
        ///  1）MySqlBulkLoader类中要设置Local = true 读取本地文件，进行导入
        /// </summary>
        /// <param name="table">dataTable数据表数据表</param>
        /// <returns>返回成功插入行数</returns>
        public int BulkInsert(DataTable table, bool isLocal = true)
        {
            if (string.IsNullOrEmpty(table.TableName)) throw new Exception("请给DataTable的TableName属性附上表名称");
            if (table.Rows.Count == 0) return 0;
            int insertCount = 0;
            string tmpPath = Path.GetTempFileName();
            string csv = DataTableToCsv(table);
            File.WriteAllText(tmpPath, csv);
            using (connection)
            {
                MySqlTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    MySqlBulkLoader bulk = new MySqlBulkLoader(connection)
                    {
                        FieldTerminator = ",",
                        FieldQuotationCharacter = '"',
                        EscapeCharacter = '"',
                        LineTerminator = "\r\n",
                        FileName = tmpPath,
                        Local = isLocal,
                        NumberOfLinesToSkip = 0,
                        TableName = table.TableName,
                    };
                    bulk.Columns.AddRange(table.Columns.Cast<DataColumn>().Select(colum => colum.ColumnName).ToList());
                    insertCount = bulk.Load();
                    tran.Commit();
                }
                catch (MySqlException ex)
                {
                    if (tran != null) tran.Rollback();
                    throw ex;
                }
            }
            File.Delete(tmpPath);
            return insertCount;
        }

        /// <summary>
        ///将DataTable转换为标准的CSV
        /// </summary>
        /// <param name="table">数据表</param>
        /// <returns>返回标准的CSV</returns>
        private static string DataTableToCsv(DataTable table)
        {
            //以半角逗号（即,）作分隔符，列为空也要表达其存在。
            //列内容如存在半角逗号（即,）则用半角引号（即""）将该字段值包含起来。
            //列内容如存在半角引号（即"）则应替换成半角双引号（""）转义，并用半角引号（即""）将该字段值包含起来。
            StringBuilder sb = new StringBuilder();
            DataColumn colum;
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    colum = table.Columns[i];
                    if (i != 0) sb.Append(",");
                    if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                    {
                        sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else sb.Append(row[colum].ToString());
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将Hashtable转为MySqlParameter数组
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public MySqlParameter[] GetSqlParameter(Hashtable ht)
        {
            MySqlParameter[] _params = new MySqlParameter[ht.Count];
            int i = 0;
            foreach (string key in ht.Keys)
            {
                _params[i] = new MySqlParameter("@" + key, ht[key] == null ? DBNull.Value : ht[key]);
                i++;
            }
            return _params;
        }

        /// <summary> 
        /// 准备执行一个命令 
        /// </summary> 
        /// <param name="cmd">sql命令</param> 
        /// <param name="conn">OleDb连接</param> 
        /// <param name="trans">OleDb事务</param> 
        /// <param name="cmdType">命令类型例如 存储过程或者文本</param> 
        /// <param name="cmdText">命令文本,例如:Select * from Products</param> 
        /// <param name="cmdParms">执行命令的参数</param> 
        private void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
