
using System;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Common.Logging;

namespace Interface.Infrastructure.Persistence
{
    public static class DataConvert
    {
        private static ILog logeer = LogManager.GetLogger("Persistence");

        public static DataTable GetDataTableFromList<TEntity>(this List<TEntity> entities) where TEntity : class, new()
        {
            if (null == entities || entities.Count == 0)
                return null;

            DataTable dt = new DataTable();
            PropertyInfo[] pArray = typeof(TEntity).GetProperties();
            try
            {
                Array.ForEach<PropertyInfo>(pArray, p =>
                {
                    dt.Columns.Add(p.Name);
                });

                entities.ForEach(t =>
                {
                    DataRow dr = dt.NewRow();
                    Array.ForEach<PropertyInfo>(pArray, p =>
                    {
                        if (dt.Columns.Contains(p.Name))
                            dr[p.Name] = p.GetValue(t);
                    });
                    dt.Rows.Add(dr);
                });
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<TEntity> GetListFromTable<TEntity>(DataTable dt) where TEntity : class, new()
        {
            if (dt == null || dt.Rows.Count == 0)
                return null;

            var objList = new List<TEntity>();

            PropertyInfo[] pArray = typeof(TEntity).GetProperties();
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    TEntity obj = new TEntity();
                    Array.ForEach<PropertyInfo>(pArray, p =>
                    {
                        if (dt.Columns.Contains(p.Name))
                        {
                            object value = row[p.Name];
                            if (value != DBNull.Value)
                                p.SetValue(obj, value, null);
                        }
                    });

                    objList.Add(obj);
                }
                return objList;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
