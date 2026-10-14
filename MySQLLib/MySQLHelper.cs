using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Common;
using System.Reflection;
using System.Data;
//using BMS.Utils;
using MySql.Data.MySqlClient;
//using BMS.Utils;

namespace MySQLLib
{
    public partial class MySQLHelper
    {

        #region PROCEDURE

        public T ProcedureToModel<T>(string procedureName, string[] paramName, object[] paramValue) where T : new()
        {
            T model = new T();
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand(procedureName, conn);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.StoredProcedure;
                if (paramName != null)
                {
                    for (int i = 0; i < paramName.Length; i++)
                    {
                        MySqlParameter sqlParam = new MySqlParameter(paramName[i], paramValue[i]);
                        cmd.Parameters.Add(sqlParam);
                    }
                }

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                model = reader.MapToSingle<T>();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return model;
        }

        public List<T> ProcedureToList<T>(string procedureName, string[] paramName, object[] paramValue) where T : new()
        {
            List<T> lst = new List<T>();
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand(procedureName, conn);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.StoredProcedure;
                if (paramName != null)
                {
                    for (int i = 0; i < paramName.Length; i++)
                    {
                        MySqlParameter sqlParam = new MySqlParameter(paramName[i], paramValue[i]);
                        cmd.Parameters.Add(sqlParam);
                    }
                }

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                lst = reader.MapToList<T>();
            }
            catch (Exception e)
            {
                //throw new Exception(e.ToString());
                //Lib.saveToLog(e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return lst;
        }

        public DataTable LoadDataFromSP(string procedureName, string[] paramName, object[] paramValue)
        {
            DataTable table = new DataTable();
            MySqlConnection MySqlConnection = new MySqlConnection(ConnectionString);
            MySqlParameter sqlParam;
            MySqlConnection.Open();

            try
            {
                MySqlCommand MySqlCommand = new MySqlCommand(procedureName, MySqlConnection);
                MySqlCommand.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(MySqlCommand);

                DataSet myDataSet = new DataSet();
                if (paramName != null)
                {
                    for (int i = 0; i < paramName.Length; i++)
                    {
                        sqlParam = new MySqlParameter(paramName[i], paramValue[i]);
                        MySqlCommand.Parameters.Add(sqlParam);
                    }
                }

                mySqlDataAdapter.Fill(myDataSet);

                table = myDataSet.Tables[0];
            }
            catch (MySqlException e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                MySqlConnection.Close();
            }

            return table;
        }

        public DataSet GetDataSetFromSP(string procedureName, string[] paramName, object[] paramValue)
        {
            DataSet myDataSet = new DataSet();
            MySqlConnection myMySqlConnection = new MySqlConnection(ConnectionString);
            MySqlParameter sqlParam;
            try
            {
                myMySqlConnection.Open();

                MySqlCommand myMySqlCommand = new MySqlCommand(procedureName, myMySqlConnection);
                myMySqlCommand.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter myMySqlDataAdapter = new MySqlDataAdapter(myMySqlCommand);

                if (paramName != null)
                {
                    for (int i = 0; i < paramName.Length; i++)
                    {
                        sqlParam = new MySqlParameter(paramName[i], paramValue[i]);
                        myMySqlCommand.Parameters.Add(sqlParam);
                    }
                }
                myMySqlDataAdapter.Fill(myDataSet);
            }
            catch (MySqlException e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                myMySqlConnection.Close();
            }
            return myDataSet;
        }

        #endregion

        #region SELECT

        public List<T> FindAll<T>() where T : new()
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            List<T> lst = new List<T>();
            T model = new T();
            Type type = model.GetType();
            try
            {
                string sql = string.Format("SELECT * FROM {0}", type.Name);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                lst = reader.MapToList<T>();
            }
            catch (Exception e)
            {
            }
            finally
            {
                conn.Close();
            }

            return lst;
        }

        public T FindByID<T>(Int64 id) where T : new()
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            T model = new T();
            Type type = model.GetType();
            try
            {
                string sql = string.Format("SELECT * FROM {0} WHERE ID = {1} limit 1", type.Name, id);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                model = reader.MapToSingle<T>();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return model;
        }

        public List<T> FindByListID<T>(List<int> _ids) where T : new()
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            List<T> lst = new List<T>();
            if (_ids.Count == 0) return lst;
            T model = new T();
            Type type = model.GetType();
            try
            {
                string sql = string.Format("SELECT * FROM [{0}] where ID IN ({1})", type.Name, string.Join(", ", _ids));
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = Timeout;
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                lst = reader.MapToList<T>();
                reader.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return lst;
        }

        public T FindByField<T>(string fieldName, object value) where T : new()
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            T model = new T();
            Type type = model.GetType();
            string tableName = type.Name;
            try
            {
                string sql = string.Format("SELECT * FROM {0} WHERE {1} = '{2}' LIMIT 1", tableName, fieldName, value);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = Timeout;
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                model = reader.MapToSingle<T>();
                reader.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return model;
        }
        public List<T> FindByExpression<T>(Expression exp) where T : new()
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            List<T> lst = new List<T>();
            T model = new T();
            Type type = model.GetType();
            try
            {
                string sql = SQLSelect(type.Name, exp);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = Timeout;
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                lst = reader.MapToList<T>();
                reader.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return lst;
        }

        public T SqlToModel<T>(string sql) where T : new()
        {
            T model = new T();
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                model = reader.MapToSingle<T>();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return model;
        }

        public List<T> SqlToList<T>(string sql) where T : new()
        {
            List<T> lst = new List<T>();
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                lst = reader.MapToList<T>();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return lst;
        }

        public object ExecuteScalar(string sqlQuery)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(ConnectionString);
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandTimeout = Timeout;
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = sqlQuery;
                object value = cmd.ExecuteScalar();
                con.Close();
                return value;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region INSERT

        public ResultQuery Insert<T>(T model)
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 1;
            Type type = model.GetType();
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                string sql = SQLInsert(model);
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                PropertyInfo[] pis = type.GetProperties();
                for (int i = 0; i < pis.Length; i++)
                {
                    object value = pis[i].GetValue(model, null);

                    if (!pis[i].Name.Equals("ID") && !pis[i].Name.Equals("iD"))
                    {
                        if (value != null)
                        {
                            if (pis[i].PropertyType.Equals(typeof(DateTime)))
                            {
                                if ((DateTime)value == DateTime.MinValue)
                                    value = _minDate;
                                else
                                {
                                    cmd.Parameters.Add("@" + pis[i].Name, ConvertToSQLType(pis[i].PropertyType)).Value = value;
                                }
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + pis[i].Name, ConvertToSQLType(pis[i].PropertyType)).Value = value;
                            }
                        }
                        else
                        {
                            if (pis[i].PropertyType.Equals(typeof(DateTime?)))
                            {
                                cmd.Parameters.Add("@" + pis[i].Name, ConvertToSQLType(pis[i].PropertyType)).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + pis[i].Name, ConvertToSQLType(pis[i].PropertyType)).Value = "";
                            }
                        }
                    }
                }

                conn.Open();
                r.ID = Convert.ToInt32(cmd.ExecuteScalar());
                r.IsSuccess = true;
            }
            catch (Exception ex)
            {
                r.IsSuccess = false;
                r.ErrorText = ex.ToString();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            }
            return r;
        }
        public ResultQuery InsertGuid<T>(T model)
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 1;
            Type type = model.GetType();
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                string sql = SQLInsert_GuidID(model);
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                PropertyInfo[] pis = type.GetProperties();
                for (int i = 0; i < pis.Length; i++)
                {
                    object value = pis[i].GetValue(model, null);

                    //if (!pis[i].Name.Equals("ID") && !pis[i].Name.Equals("iD"))
                    //{
                    if (value != null)
                    {
                        if (pis[i].PropertyType.Equals(typeof(DateTime)))
                        {
                            if ((DateTime)value == DateTime.MinValue)
                                value = _minDate;
                            else
                            {
                                cmd.Parameters.Add("@" + pis[i].Name, ConvertToSQLType(pis[i].PropertyType)).Value = value;
                            }
                        }
                        else
                        {
                            cmd.Parameters.Add("@" + pis[i].Name, ConvertToSQLType(pis[i].PropertyType)).Value = value;
                        }
                    }
                    else
                    {
                        if (pis[i].PropertyType.Equals(typeof(DateTime?)))
                        {
                            cmd.Parameters.Add("@" + pis[i].Name, ConvertToSQLType(pis[i].PropertyType)).Value = DBNull.Value;
                        }
                        else
                        {
                            cmd.Parameters.Add("@" + pis[i].Name, ConvertToSQLType(pis[i].PropertyType)).Value = "";
                        }

                    }
                    //}
                }
                conn.Open();
                cmd.ExecuteScalar();
                // r.ID = cmd.ExecuteNonQuery();
                r.IsSuccess = true;
            }
            catch (Exception ex)
            {
                r.IsSuccess = false;
                r.ErrorText = ex.ToString();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            }
            return r;
        }
        #endregion

        #region UPDATE

        public ResultQuery Update<T>(T model) //where T : new()
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 0;
            Type type = model.GetType();
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                string sql = SQLUpdate(model);
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                PropertyInfo[] pis = type.GetProperties();
                for (int i = 0; i < pis.Length; i++)
                {
                    MySqlDbType dbType = ConvertToSQLType(pis[i].PropertyType);
                    object value = pis[i].GetValue(model, null);

                    if (value != null)
                    {
                        if (pis[i].PropertyType.Equals(typeof(DateTime)))
                        {
                            if ((DateTime)value == DateTime.MinValue)
                                value = _minDate;
                            else
                            {
                                cmd.Parameters.Add("@" + pis[i].Name, dbType).Value = value;
                            }
                        }
                        else
                        {
                            cmd.Parameters.Add("@" + pis[i].Name, dbType).Value = value;
                        }
                    }
                    else
                    {
                        if (pis[i].PropertyType.Equals(typeof(DateTime?)))
                        {
                            cmd.Parameters.Add("@" + pis[i].Name, dbType).Value = DBNull.Value;
                        }
                        else
                            cmd.Parameters.Add("@" + pis[i].Name, dbType).Value = "";
                    }
                }
                conn.Open();
                r.TotalRow = cmd.ExecuteNonQuery();
                r.IsSuccess = true;
            }
            catch (Exception ex)
            {
                r.IsSuccess = false;
                r.ErrorText = ex.ToString();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            }
            return r;
        }
        public ResultQuery UpdateGuid<T>(T model)
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 0;
            Type type = model.GetType();
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                string sql = SQLUpdate_GuidID(model);
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                PropertyInfo[] pis = type.GetProperties();
                for (int i = 0; i < pis.Length; i++)
                {
                    MySqlDbType dbType = ConvertToSQLType(pis[i].PropertyType);
                    object value = pis[i].GetValue(model, null);

                    if (value != null)
                    {
                        if (pis[i].PropertyType.Equals(typeof(DateTime)))
                        {
                            if ((DateTime)value == DateTime.MinValue)
                                value = _minDate;
                        }
                        else
                        {
                            cmd.Parameters.Add("@" + pis[i].Name, dbType).Value = value;
                        }
                    }
                    else
                    {
                        if (pis[i].PropertyType.Equals(typeof(DateTime?)))
                        {
                            cmd.Parameters.Add("@" + pis[i].Name, dbType).Value = DBNull.Value;
                        }
                        else
                            cmd.Parameters.Add("@" + pis[i].Name, dbType).Value = "";
                    }
                }
                conn.Open();
                r.TotalRow = cmd.ExecuteNonQuery();
                r.IsSuccess = true;
            }
            catch (Exception ex)
            {
                r.IsSuccess = false;
                r.ErrorText = ex.ToString();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            }
            return r;
        }
        public ResultQuery UpdateFields<T>(Dictionary<object, object> fieldValues, Expression exp)
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 1;
            Type type = typeof(T);
            string tableName = type.Name;

            string setClause = string.Join(", ", fieldValues.Keys.Select(key => $"{key} = @{key}"));

            string query = $"UPDATE {tableName} SET {setClause} WHERE {exp.ToString()}";

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                foreach (var field in fieldValues)
                {
                    command.Parameters.AddWithValue($"@{field.Key}", field.Value);
                }

                try
                {
                    connection.Open();
                    r.TotalRow = command.ExecuteNonQuery();
                    r.IsSuccess = true;
                }
                catch (Exception ex)
                {
                    r.IsSuccess = false;
                    r.ErrorText = ex.ToString();
                }
            }
            return r;
        }
        public ResultQuery UpdateFieldsByID<T>(Dictionary<object, object> fieldValues, long ID)
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 1;
            Type type = typeof(T);
            string tableName = type.Name;

            string setClause = string.Join(", ", fieldValues.Keys.Select(key => $"{key} = @{key}"));

            string query = $"UPDATE {tableName} SET {setClause} WHERE ID = {ID}";

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                foreach (var field in fieldValues)
                {
                    command.Parameters.AddWithValue($"@{field.Key}", field.Value);
                }

                try
                {
                    connection.Open();
                    r.TotalRow = command.ExecuteNonQuery();
                    r.IsSuccess = true;
                }
                catch (Exception ex)
                {
                    r.IsSuccess = false;
                    r.ErrorText = ex.ToString();
                }
            }
            return r;
        }
        public ResultQuery UpdateFieldsByGuidID<T>(Dictionary<object, object> fieldValues, Guid ID)
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 1;
            Type type = typeof(T);
            string tableName = type.Name;

            string setClause = string.Join(", ", fieldValues.Keys.Select(key => $"{key} = @{key}"));

            string query = $"UPDATE {tableName} SET {setClause} WHERE ID = '{ID}'";

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                foreach (var field in fieldValues)
                {
                    command.Parameters.AddWithValue($"@{field.Key}", field.Value);
                }

                try
                {
                    connection.Open();
                    r.TotalRow = command.ExecuteNonQuery();
                    r.IsSuccess = true;
                }
                catch (Exception ex)
                {
                    r.IsSuccess = false;
                    r.ErrorText = ex.ToString();
                }
            }
            return r;
        }
        #endregion

        #region DELETE
        public ResultQuery DeleteByGuidID<T>(Guid id) where T : new()
        {
            T model = new T();
            Type type = model.GetType();
            ResultQuery r = new ResultQuery();
            r.TotalRow = 0;
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                string sql = SQLDelete(type.Name, id);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = Timeout;
                cmd.Connection.Open();
                r.TotalRow = cmd.ExecuteNonQuery();
                r.IsSuccess = true;
            }
            catch (Exception ex)
            {
                r.IsSuccess = false;
                r.ErrorText = ex.ToString();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            }
            return r;
        }
        public ResultQuery DeleteByID<T>(Int64 id) where T : new()
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 0;
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            T model = new T();
            string tableName = model.GetType().Name;
            try
            {
                string sql = string.Format("DELETE FROM {0} WHERE ID = {1}", tableName, id);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                conn.Open();
                //cmd.ExecuteNonQuery();
                r.TotalRow = cmd.ExecuteNonQuery();
                r.IsSuccess = true;
            }
            catch (Exception ex)
            {
                r.IsSuccess = false;
                r.ErrorText = ex.ToString();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            }
            return r;
        }
        public ResultQuery DeleteByExpression<T>(Expression exp) where T : new()
        {
            T model = new T();
            Type type = model.GetType();
            ResultQuery r = new ResultQuery();
            r.TotalRow = 0;
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                string sql = SQLDelete(type.Name, exp);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = Timeout;
                cmd.Connection.Open();

                r.TotalRow = cmd.ExecuteNonQuery();
                r.IsSuccess = true;
            }
            catch (Exception ex)
            {
                r.IsSuccess = false;
                r.ErrorText = ex.ToString();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            }
            return r;
        }
        public ResultQuery DeleteByListID<T>(List<int> listID) where T : new()
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 0;
            T model = new T();
            Type type = model.GetType();
            string tableName = type.Name;

            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                string sql = "DELETE FROM " + tableName + " WHERE ID IN (" + string.Join(", ", listID) + ")";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = Timeout;
                cmd.Connection.Open();

                r.TotalRow = cmd.ExecuteNonQuery();
                r.IsSuccess = true;
            }
            catch (Exception ex)
            {
                r.IsSuccess = false;
                r.ErrorText = ex.ToString();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            }
            return r;
        }
        public ResultQuery DeleteByListGuidID<T>(List<string> listID) where T : new()
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 0;
            T model = new T();
            Type type = model.GetType();
            string tableName = type.Name;

            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                string sql = "DELETE FROM " + tableName + " WHERE ID IN (" + string.Join(", ", listID) + ")";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = Timeout;
                cmd.Connection.Open();

                r.TotalRow = cmd.ExecuteNonQuery();
                r.IsSuccess = true;
            }
            catch (Exception ex)
            {
                r.IsSuccess = false;
                r.ErrorText = ex.ToString();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            }
            return r;
        }

        #endregion
    }


}
