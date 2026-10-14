using System;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.IO;

namespace MySQLLib
{
    public partial class MySQLHelper//<T> where T : class, new()
    {
        static DateTime _minDate = new DateTime(1900, 01, 01);
        public static string password = "123@123a";
        public static string ConnectionString = $"Server=localhost; Database=discrondiamond_new; Port = 3306; Uid=root; Password={password};";
        public int Timeout = 2000;
        #region CONSTRUCTORS
        public MySQLHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public MySQLHelper()
        {
        }

        #endregion

        #region STRING QUERY
        string SQLSelect(string tableName, Expression exp)
        {
            string sql = "SELECT * FROM " + tableName;
            if (exp != null)
                sql += " WHERE " + exp.ToString();
            return sql;
        }
        string SQLInsert<T>(T model)
        {
            Type type = model.GetType();
            string tableName = type.Name;

            string Insert = "insert into " + tableName + " (";
            PropertyInfo[] pis = type.GetProperties();

            for (int i = 0; i < pis.Length; i++)
            {
                if (!pis[i].Name.Equals("ID"))
                {
                    Insert = Insert + pis[i].Name;
                    Insert = Insert + ",";
                }
            }
            Insert = Insert.Substring(0, Insert.Length - 1);
            Insert = Insert + ") values (";
            for (int i = 0; i < pis.Length; i++)
            {
                if (!pis[i].Name.Equals("ID"))
                {
                    Insert = Insert + "@";
                    Insert = Insert + pis[i].Name;
                    Insert = Insert + ",";
                }
            }
            Insert = Insert.Substring(0, Insert.Length - 1);
            Insert = Insert + ") ;Select LAST_INSERT_ID()";
            return Insert;
        }
        string SQLInsert_GuidID<T>(T model)
        {
            Type type = model.GetType();
            string tableName = type.Name;

            string Insert = "insert into " + tableName + " (";
            PropertyInfo[] pis = type.GetProperties();

            for (int i = 0; i < pis.Length; i++)
            {
                //if (!pis[i].Name.Equals("ID"))
                //{
                Insert = Insert + pis[i].Name;
                Insert = Insert + ",";
                //}
            }
            Insert = Insert.Substring(0, Insert.Length - 1);
            Insert = Insert + ") values (";
            for (int i = 0; i < pis.Length; i++)
            {
                //if (!pis[i].Name.Equals("ID"))
                //{
                Insert = Insert + "@";
                Insert = Insert + pis[i].Name;
                Insert = Insert + ",";
                //}
            }
            Insert = Insert.Substring(0, Insert.Length - 1);
            Insert = Insert + ") Select LAST_INSERT_ID()";
            return Insert;
        }
        string SQLUpdate<T>(T model)
        {
            Type type = model.GetType();
            string tableName = type.Name;
            string Update = "UPDATE " + tableName + " SET ";
            PropertyInfo[] pis = type.GetProperties();

            for (int i = 0; i < pis.Length; i++)
            {
                if (!pis[i].Name.Equals("ID"))
                {
                    Update = Update + pis[i].Name;
                    Update = Update + "=@" + pis[i].Name;
                    Update = Update + ",";
                }
            }
            Update = Update.Substring(0, Update.Length - 1);
            Update = Update + " WHERE ID=" + type.GetProperty("ID").GetValue(model, null).ToString();

            return Update;
        }
        string SQLUpdate_GuidID<T>(T model)
        {
            Type type = model.GetType();
            string tableName = type.Name;
            string Update = "UPDATE " + tableName + " SET ";
            PropertyInfo[] pis = type.GetProperties();

            for (int i = 0; i < pis.Length; i++)
            {
                if (!pis[i].Name.Equals("ID"))
                {
                    Update = Update + pis[i].Name;
                    Update = Update + "=@" + pis[i].Name;
                    Update = Update + ",";
                }
            }
            Update = Update.Substring(0, Update.Length - 1);
            Update = Update + $" WHERE ID= '{type.GetProperty("ID").GetValue(model, null).ToString()}'";

            return Update;
        }
        string SQLDelete<T>(T model)
        {
            Type type = model.GetType();
            string tableName = type.Name;
            PropertyInfo pi = type.GetProperty("ID");
            string Delete = "DELETE FROM " + tableName + " WHERE ID = " + pi.GetValue(model);
            return Delete;
        }
        string SQLDelete(string tableName, Guid id)
        {
            string Delete = "DELETE FROM " + tableName + $" WHERE ID = '{id.ToString()}'";
            return Delete;
        }
        string SQLDelete(string tableName, Expression exp) //where T : new()
        {
            string Delete = $"DELETE FROM {tableName} where {exp.ToString()}";
            return Delete;
        }
        #endregion

        static MySqlDbType ConvertToSQLType(Type type)
        {
            if (type == typeof(string))
            {
                return MySqlDbType.VarChar;
            }
            if (type == typeof(int))
            {
                return MySqlDbType.Int32;//.Int;
            }
            if (type == typeof(Int16))
            {
                return MySqlDbType.Int16;//.TinyInt;
            }
            if (type == typeof(Int64))
            {
                return MySqlDbType.Int64;//.BigInt;
            }
            if (type == typeof(DateTime))
            {
                return MySqlDbType.DateTime;
            }
            if (type == typeof(DateTime?))
            {
                return MySqlDbType.DateTime;
            }
            if (type == typeof(long))
            {
                return MySqlDbType.Int64;//.BigInt;
            }
            if (type == typeof(Decimal))
            {
                return MySqlDbType.Decimal;
            }
            if (type == typeof(Double))
            {
                return MySqlDbType.Double;
            }
            if (type == typeof(bool))
            {
                return MySqlDbType.Bit;
            }
            //if (type == typeof(Byte[]))
            //{
            //	return MySqlDbType.Image;
            //}
            //if (type == typeof(Guid))
            //{
            //	return MySqlDbType..UniqueIdentifier;
            //}
            return MySqlDbType.VarChar;
        }
        static bool IsNumberType(object value)
        {
            Type type = value.GetType();
            if (type == typeof(int) || type == typeof(float) || type == typeof(double) || type == typeof(decimal))
                return true;
            else
            {
                return false;
            }
        }


    }
}
