using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Data.SqlClient;
using System.Collections;
using System.Data.Common;
using System.Reflection;
using System.Data;
using MySql.Data.MySqlClient;

namespace MySQLLib
{
	public static class DataReaderExtension
    {
        /// <Summary>
        /// Map data from DataReader to an object
        /// </Summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="dr">Data Reader</param>
        /// <returns>Object having data from Data Reader</returns>
        public static T MapToSingle<T>(this MySqlDataReader dr) where T : new()
        {
            T RetVal = new T();
            var Entity = typeof(T);
            var PropDict = new Dictionary<string, PropertyInfo>();
            try
            {
                if (dr != null && dr.HasRows)
                {
                    var Props = Entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    PropDict = Props.ToDictionary(p => p.Name.ToUpper(), p => p);
                    dr.Read();
                    for (int Index = 0; Index < dr.FieldCount; Index++)
                    {
                        if (PropDict.ContainsKey(dr.GetName(Index).ToUpper()))
                        {
                            var Info = PropDict[dr.GetName(Index).ToUpper()];
                            if ((Info != null) && Info.CanWrite)
                            {
                                var Val = dr.GetValue(Index);
                                if (Info.PropertyType.FullName.ToLower().Contains("bool"))
                                {
                                    Val = Convert.ToInt32(Val) == 1 ? true : false;
                                }
                                Info.SetValue(RetVal, (Val == DBNull.Value) ? null : Val, null);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RetVal;
        }

        public static List<T> MapToList<T>(this MySqlDataReader dr) where T : new()
        {
            List<T> RetVal = new List<T>();
            var Entity = typeof(T);
            var PropDict = new Dictionary<string, PropertyInfo>();
            try
            {
                if (dr != null && dr.HasRows)
                {
                    RetVal = new List<T>();
                    var Props = Entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    PropDict = Props.ToDictionary(p => p.Name.ToUpper(), p => p);
                    while (dr.Read())
                    {
                        T newObject = new T();
                        for (int Index = 0; Index < dr.FieldCount; Index++)
                        {
                            if (PropDict.ContainsKey(dr.GetName(Index).ToUpper()))
                            {
                                var Info = PropDict[dr.GetName(Index).ToUpper()];
                                if ((Info != null) && Info.CanWrite)
                                {
                                    var Val = dr.GetValue(Index);
                                    if(Info.PropertyType.FullName.ToLower().Contains("bool"))
                                    {
                                        Val = Convert.ToInt32(Val) == 1 ? true : false;
                                    }    
                                    Info.SetValue(newObject, (Val == DBNull.Value) ? null : Val, null);
                                }
                            }
                        }
                        RetVal.Add(newObject);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return RetVal;
        }
    }

  
}

