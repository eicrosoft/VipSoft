using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Text;
using VipSoft.Core.Entity;
using VipSoft.Core.Utility;
using VipSoft.Data.Engine;
using VipSoft.FastReflection;
using log4net;

namespace VipSoft.Data.Persister
{
    public class Map
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(Map));

        private static readonly string MinDateString = MinDateTime.ToString();
        private static readonly string MinDateShortDateString = MinDateTime.ToShortDateString();
        private static readonly string MinDateShortTimeString = MinDateTime.ToShortTimeString();
        private static readonly DateTime MinDateTime = DateTime.MinValue;
        private static readonly string[] LikeString = new[] { "[{0}]", "['%{0}%']", "['%{0}']", "['{0}%']" };
        private static readonly string[] LikeValueString = new[] { "'%{0}%'", "'%{0}'", "'{0}%'" };

        private static int Contains(string conditaion, string columnName)
        {
            int result = -1;

            if (!string.IsNullOrEmpty(conditaion))
            {
                for (int i = 0; i < LikeString.Length; i++)
                {
                    if (conditaion.Contains(string.Format(LikeString[i], columnName)))
                    {
                        result = i;
                        break;
                    }
                }
            }

            return result;
        }

        private static string GetConditaion(ISessionImplementor session, IEntityPersister entityPersister, IEntity obj, out List<DbParameter> parameters)
        {
            var rConditaion = obj.Conditaion;
            var classMetadata = entityPersister.ClassMetadata;
            parameters = new List<DbParameter>();
            foreach (ColumnInfo columnInfo in classMetadata.ColumnInfos)
            {
                var value = columnInfo.PropertyInfo.FastGetValue(obj);
                if (IsNull(value)) continue;
                if (IsNullValue(value) && columnInfo.IsNullable) continue;

                var index = Contains(obj.Conditaion, columnInfo.PropertyName);
                if (index == 0)
                {
                    rConditaion = rConditaion.Replace(string.Format(LikeString[0], columnInfo.PropertyName), entityPersister.Driver.FormatNameForSql(columnInfo.Name));
                    entityPersister.SetDbParamter(session, parameters, columnInfo.Name, value);
                    //entityPersister.SetDbParamter(session, parameters, classMetadata.PrimaryKey, value);
                }
                else if (index > 0)
                {
                    rConditaion = rConditaion.Replace(string.Format(LikeString[index], columnInfo.PropertyName), string.Format(LikeValueString[index - 1], Escape(value.ToString(), "")));
                }
            }
            return rConditaion;
        }

        private static string GetWhereCaluse(string conditaion)
        {
            if (string.IsNullOrEmpty(conditaion)) return "";

            var where = new StringBuilder();
            var scodetion = conditaion.Split(';');
            for (var i = 0; i < scodetion.Length; i++)
            {
                if (string.IsNullOrEmpty(scodetion[i]) || (scodetion[i].Contains("["))) continue;
                if (where.Length == 0)
                {
                    where.AppendFormat(" {0}", scodetion[i].Replace("AND ", "").Replace("OR ", ""));
                }
                else
                {
                    where.AppendFormat(" {0}", scodetion[i]);
                }
            }
            return where.ToString();
        }

        private static string GetDbString(ColumnInfo columnInfo, object v)
        {
            if (!IsNull(v))
            {
                if (columnInfo.IsNullable && IsNullValue(v)) return "NULL";
                if (columnInfo.PropertyType == typeof(string) || columnInfo.PropertyType == typeof(DateTime)) return string.Format("'{0}'", v);
                return v.ToString();
            }
            if (!columnInfo.IsNullable)
            {
                if (columnInfo.PropertyType == typeof(string)) return "''";
                return columnInfo.PropertyType == typeof(DateTime) ? string.Format("'{0}'", MinDateString) : "0";
            }
            return "NULL";
        }

        private static List<string> CreateBatchInsertSql(IEntityPersister entityPersister, int listCount, int batchSize, int batchTime, IEntity[] list)
        {
            var result = new List<string>();
            var columns = new StringBuilder();
            StringBuilder value;
            StringBuilder values;
            var classMetadata = entityPersister.ClassMetadata;
            var columnInfos = classMetadata.ColumnInfos.FindAll(t => t.IsColumn);
            object v;
            for (var i = 0; i < batchTime; i++)
            {
                values = new StringBuilder();
                for (var j = 0; j < batchSize; j++)
                {
                    var s = i * batchSize + j;
                    value = new StringBuilder();
                    foreach (var columnInfo in columnInfos)
                    {
                        if (s > listCount) break;
                        v = columnInfo.PropertyInfo.FastGetValue(list[s]);
                        if (columnInfo.IsIncrement) continue;
                        if (s == 0) columns.AppendFormat("{0},", columnInfo.PropertyName);
                        value.AppendFormat("{0},", GetDbString(columnInfo, v));
                    }
                    if (value.Length > 0) values.AppendFormat("({0}),", value.Remove(value.Length - 1, 1));
                }
                result.Add(string.Format("INSERT INTO {0} ({1}) VALUES {2}{3}", classMetadata.TableName, columns.Remove(columns.Length - 1, 1), values.Remove(values.Length - 1, 1), entityPersister.Driver.MultipleQueriesSeparator));
            }
            return result;
        }

        private static List<string> CreateNoBatchInsertSql(IEntityPersister entityPersister, int listCount, int batchSize, int batchTime, IEntity[] list)
        {
            var result = new List<string>();
            var columns = new StringBuilder();
            StringBuilder value;
            StringBuilder values;
            StringBuilder batch;
            var classMetadata = entityPersister.ClassMetadata;
            var columnInfos = classMetadata.ColumnInfos.FindAll(t => t.IsColumn);
            object v;
            for (var i = 0; i < batchTime; i++)
            {
                batch = new StringBuilder();
                for (var j = 0; j < batchSize; j++)
                {
                    values = new StringBuilder();
                    var s = i * batchSize + j;
                    value = new StringBuilder();
                    foreach (var columnInfo in columnInfos)
                    {
                        if (s > listCount) break;
                        v = columnInfo.PropertyInfo.FastGetValue(list[s]);
                        if (columnInfo.IsIncrement) continue;
                        if (s == 0) columns.AppendFormat("{0},", columnInfo.PropertyName);
                        value.AppendFormat("{0},", GetDbString(columnInfo, v));
                    }
                    if (s == 0) columns = columns.Remove(columns.Length - 1, 1);

                    if (value.Length > 0) values.AppendFormat("({0}),", value.Remove(value.Length - 1, 1));
                    batch.AppendFormat("INSERT INTO {0} ({1}) VALUES {2}{3}", classMetadata.TableName, columns, values.Remove(values.Length - 1, 1), entityPersister.Driver.MultipleQueriesSeparator);
                }
                result.Add(batch.ToString());
            }
            return result;
        }

        //private static List<string> CreateNoBatchInsertSql(IEntityPersister entityPersister, int listCount, int batchSize, int batchTime, IEntity[] list)
        //{
        //    var result = new List<string>();
        //    var columns = new StringBuilder();
        //    StringBuilder value;
        //    StringBuilder values;
        //    StringBuilder batch ;
        //    var classMetadata = entityPersister.ClassMetadata;
        //    var columnInfos = classMetadata.ColumnInfos.FindAll(t => t.IsColumn);
        //    object v;
        //    batch = new StringBuilder();
        //    batch.AppendFormat("INSERT INTO {0}",classMetadata.TableName);
        //    for (var i = 0; i < batchTime; i++)
        //    {
        //        //batch = new StringBuilder();
        //        for (var j = 0; j < batchSize; j++)
        //        {
        //            values = new StringBuilder();
        //            var s = i * batchSize + j;
        //            value = new StringBuilder();
        //            foreach (var columnInfo in columnInfos)
        //            {
        //                if (s > listCount) break;
        //                v = columnInfo.PropertyInfo.FastGetValue(list[s]);
        //                if (columnInfo.IsIncrement) continue;
        //                if (s == 0) columns.AppendFormat("{0},", columnInfo.PropertyName);
        //                value.AppendFormat("{0},", GetDbString(columnInfo, v));
        //            }
        //            if (s == 0) columns = columns.Remove(columns.Length - 1, 1);

        //            if (value.Length > 0) values.AppendFormat("{0},", value.Remove(value.Length - 1, 1));
        //            batch.AppendFormat(" SELECT {0} UNION", values.Remove(values.Length - 1, 1));
        //        }
        //        batch = batch.Remove(batch.Length-5, 5);
        //        result.Add(batch.ToString());
        //    }
        //    return result;
        //}

        private static bool IsNull(object value)
        {
            if ((value == null)) return true;
            var nullable = value as INullable;
            return ((nullable != null) && nullable.IsNull);
        }

        private static bool IsNullValue(object value)
        {
            if (IsNull(value)) return true;

            if (value.GetType().Name == "DateTime")
            {
                var s = Convert.ToDateTime(value);
                return (s.CompareTo(MinDateTime) <= 0);
            }
            else
            {
                var s = value.ToString().Trim();
                return s == "" || s == "0" || s == "0.0" || s == MinDateString || s == MinDateShortDateString || s == MinDateShortTimeString;
            }
        }

        #region public

        public static CmdParameter CreateInsertReturnIdentitySql(ISessionImplementor session, IEntityPersister entityPersister, IEntity obj)
        {

            var result = CreateInsertSql(session, entityPersister, obj);
            result.CommandText = string.Format("{0}{1}", result.CommandText, entityPersister.Driver.GetIdentityString);
            return result;

        }

        public static CmdParameter CreateInsertSql(ISessionImplementor session, IEntityPersister entityPersister, IEntity obj)
        {
            var result = new CmdParameter();

            var classMetadata = entityPersister.ClassMetadata;
            var driver = entityPersister.Driver;

            var sql = new StringBuilder("INSERT INTO");
            sql.AppendFormat(" {0} (", classMetadata.TableName);
            var values = new StringBuilder();
            var tParameters = new List<DbParameter>();
            var columnInfos = classMetadata.ColumnInfos.FindAll(t => t.IsColumn);

            foreach (var columnInfo in columnInfos)
            {
                var v = columnInfo.PropertyInfo.FastGetValue(obj);
                if (IsNull(v)) continue;
                if (columnInfo.IsNullable && IsNullValue(v)) continue;
                sql.AppendFormat("{0},", columnInfo.Name);
                values.AppendFormat("{0},", driver.FormatNameForSql(columnInfo.Name));
                entityPersister.SetDbParamter(session, tParameters, columnInfo.Name, v);
            }
            if (values.Length > 0)
            {
                sql.Replace(',', ')', sql.Length - 1, 1);
                sql.AppendFormat(" VALUES ({0}){1}", values.Remove(values.Length - 1, 1), driver.MultipleQueriesSeparator);

                result.CommandText = sql.ToString();
                result.Parameters = tParameters.ToArray();
            }

            return result;
        }

        public static List<string> CreateInsertSql(IEntityPersister entityPersister, IEntity[] list)
        {
            var result = new List<string>();
            var listCount = list.Length;
            if (listCount > 0)
            {
                var batchSize = entityPersister.Factory.Settings.BatchSize;
                if (batchSize > listCount) batchSize = listCount;
                var batchTime = (listCount + batchSize - 1) / batchSize;
                result = entityPersister.Driver.SupportsBacthInsert ? CreateBatchInsertSql(entityPersister, listCount, batchSize, batchTime, list) : CreateNoBatchInsertSql(entityPersister, listCount, batchSize, batchTime, list);
            }
            return result;
        }

        public static CmdParameter CreateUpdateSql(ISessionImplementor session, IEntityPersister entityPersister, IEntity obj)
        {
            var classMetadata = entityPersister.ClassMetadata;
            var rConditaion = obj.Conditaion;
            var columns = new StringBuilder();

            List<DbParameter> parameters = new List<DbParameter>();
            foreach (ColumnInfo columnInfo in classMetadata.ColumnInfos)
            {
                var value = columnInfo.PropertyInfo.FastGetValue(obj);
                if (IsNull(value)) continue;
                if (columnInfo.IsPrimaryKey && string.IsNullOrEmpty(obj.Conditaion))
                {
                    rConditaion = string.Format("{0}={1}", columnInfo.Name, entityPersister.Driver.FormatNameForSql(columnInfo.Name));
                    entityPersister.SetDbParamter(session, parameters, columnInfo.Name, value);
                }
                else
                {
                    var index = Contains(obj.Conditaion, columnInfo.Name);
                    if (index == 0)
                    {
                        rConditaion = rConditaion.Replace(string.Format(LikeString[0], columnInfo.Name), entityPersister.Driver.FormatNameForSql(columnInfo.Name));
                        entityPersister.SetDbParamter(session, parameters, classMetadata.PKColumnName, value);
                    }
                    else if (index > 0)
                    {
                        rConditaion = rConditaion.Replace(string.Format(LikeString[index], columnInfo.Name), string.Format(LikeValueString[index - 1], Escape(value.ToString(), "")));
                    }
                    else if (index == -1)
                    {
                        columns.AppendFormat("{0}={1},", columnInfo.Name, entityPersister.Driver.FormatNameForSql(columnInfo.Name));

                        if (columnInfo.IsNullable && IsNullValue(value))
                        {
                            entityPersister.SetDbParamter(session, parameters, columnInfo.Name, DBNull.Value);
                        }
                        else
                        {
                            entityPersister.SetDbParamter(session, parameters, columnInfo.Name, value);
                        }
                    }

                }

            }
            var where = GetWhereCaluse(rConditaion);
            var result = new CmdParameter();
            if (where.Length > 0 && columns.Length > 0)
            {
                result.Parameters = parameters.ToArray();
                result.CommandText = string.Format("UPDATE {0} SET {1} WHERE {2}{3}", classMetadata.TableName, columns.Remove(columns.Length - 1, 1), where, entityPersister.Driver.MultipleQueriesSeparator);
            }

            return result;
        }

        public static CmdParameter CreateDeleteSql(ISessionImplementor session, IEntityPersister entityPersister, IEntity obj)
        {
            var classMetadata = entityPersister.ClassMetadata;

            if (string.IsNullOrEmpty(obj.Conditaion))
            {
                var v = classMetadata.ColumnInfos.Find(t => t.IsPrimaryKey).PropertyInfo.FastGetValue(obj);
                return !IsNull(v) ? CreateDeleteSql(session, entityPersister, (int)v) : null;
            }
            List<DbParameter> parameters;
            string rConditaion = GetConditaion(session, entityPersister, obj, out parameters);
            var where = GetWhereCaluse(rConditaion);
            var result = new CmdParameter();
            if (where.Length > 0)
            {
                result.Parameters = parameters.ToArray();
                result.CommandText = string.Format("DELETE FROM {0} WHERE {1}{2}", classMetadata.TableName, where, entityPersister.Driver.MultipleQueriesSeparator);
            }
            return result;
        }

        public static CmdParameter CreateDeleteSql(ISessionImplementor session, IEntityPersister entityPersister, params int[] pKeys)
        {
            var result = new CmdParameter();
            var classMetadata = entityPersister.ClassMetadata;
            var driver = entityPersister.Driver;
            if (pKeys.Length == 1)
            {
                result.CommandText = string.Format("DELETE FROM {0} WHERE {1}={2}{3}", classMetadata.TableName, classMetadata.PKColumnName, driver.FormatNameForSql(classMetadata.PKColumnName), driver.MultipleQueriesSeparator);
                var parameters = new List<DbParameter>();
                entityPersister.SetDbParamter(session, parameters, classMetadata.PKColumnName, pKeys[0]);
                result.Parameters = parameters.ToArray();
            }
            else if (pKeys.Length > 1)
            {
                result.CommandText = string.Format("DELETE FROM {0} WHERE {1} {2}{3}", classMetadata.TableName, classMetadata.PKColumnName, GetInExpression(pKeys), driver.MultipleQueriesSeparator);
            }

            return result;
        }

        public static CmdParameter CreateDeleteWithValidateSql(ISessionImplementor session, IEntityPersister entityPersister, params int[] pKeys)
        {
            var result = new CmdParameter();
            var classMetadata = entityPersister.ClassMetadata;
            var driver = entityPersister.Driver;

            if (pKeys.Length == 1)
            {
                result.CommandText = string.Format("Delete FROM {0} WHERE {1} IN ( SELECT {1} FROM (SELECT M.{1} FROM {0} AS M  LEFT JOIN {2} AS S ON M.ID=S.{3} WHERE M.{1}={4} AND S.{3} IS NULL )AS MS ){5}", classMetadata.TableName, classMetadata.PrimaryKey, classMetadata.AssociateTable, classMetadata.ForeignKey, driver.FormatNameForSql(classMetadata.PrimaryKey), driver.MultipleQueriesSeparator);
                var parameters = new List<DbParameter>();
                entityPersister.SetDbParamter(session, parameters, classMetadata.PrimaryKey, pKeys[0]);
                result.Parameters = parameters.ToArray();
            }
            else if (pKeys.Length > 1)
            {
                result.CommandText = string.Format("Delete FROM {0} WHERE {1} IN ( SELECT {1} FROM (SELECT M.{1} FROM {0} AS M  LEFT JOIN {2} AS S ON M.ID=S.{3} WHERE M.{1} {4} AND S.{3} IS NULL )AS MS ){5}", classMetadata.TableName, classMetadata.PrimaryKey, classMetadata.AssociateTable, classMetadata.ForeignKey, GetInExpression(pKeys), driver.MultipleQueriesSeparator);
            }

            return result;
        }

        public static CmdParameter CreateDeleteWithAssociateSql(ISessionImplementor session, IEntityPersister entityPersister, params int[] pKeys)
        {
            var result = new CmdParameter();
            var classMetadata = entityPersister.ClassMetadata;
            var driver = entityPersister.Driver;

            if (pKeys.Length == 1)
            {
                result.CommandText = string.Format("DELETE FROM {0} WHERE {1}={2}{5}DELETE FROM {3} WHERE {4}={2}{5}", classMetadata.AssociateTable, classMetadata.ForeignKey, driver.FormatNameForSql(classMetadata.PrimaryKey), classMetadata.TableName, classMetadata.PrimaryKey, driver.MultipleQueriesSeparator);
                var parameters = new List<DbParameter>();
                entityPersister.SetDbParamter(session, parameters, classMetadata.PrimaryKey, pKeys[0]);
                result.Parameters = parameters.ToArray();
            }
            else if (pKeys.Length > 1)
            {
                result.CommandText = string.Format("DELETE FROM {0} WHERE {1} {2}{5}DELETE FROM {3} WHERE {4} {2}{5}", classMetadata.AssociateTable, classMetadata.ForeignKey, GetInExpression(pKeys), classMetadata.TableName, classMetadata.PrimaryKey, driver.MultipleQueriesSeparator);
            }

            return result;
        }

        public static CmdParameter CreateSelectSql(ISessionImplementor session, IEntityPersister entityPersister, object pKey)
        {
            var result = new CmdParameter();
            if (pKey != null)
            {
                var classMetadata = entityPersister.ClassMetadata;
                var driver = entityPersister.Driver;
                result.CommandText = string.Format("SELECT * FROM {0} WHERE {1}={2}", classMetadata.TableName, classMetadata.PrimaryKey, driver.FormatNameForSql(classMetadata.PrimaryKey));
                var parameters = new List<DbParameter>();
                entityPersister.SetDbParamter(session, parameters, classMetadata.PKColumnName, pKey);
                result.Parameters = parameters.ToArray();
            }
            return result;
        }

        public static CmdParameter CreateSelectSql(ISessionImplementor session, IEntityPersister entityPersister, IEntity obj)
        {
            var classMetadata = entityPersister.ClassMetadata;
            var result = new CmdParameter();
            if (string.IsNullOrEmpty(obj.Conditaion))
            {
                var columnInfo = classMetadata.ColumnInfos.Find(t => t.IsPrimaryKey);
                if (columnInfo != null)
                {
                    var v = columnInfo.PropertyInfo.FastGetValue(obj);
                    if (!IsNull(v)) result = CreateSelectSql(session, entityPersister, v);
                }

            }

            List<DbParameter> parameters;
            string rConditaion = GetConditaion(session, entityPersister, obj, out parameters);

            var sql = new StringBuilder("SELECT ");
            sql.Append(string.IsNullOrEmpty(obj.ResultColumns) ? "*" : obj.ResultColumns);
            //sql.Append("*");        //delete
            sql.AppendFormat(" FROM {0} ", classMetadata.TableName);
            var where = GetWhereCaluse(rConditaion);
            if (where.Length > 0)
            {
                sql.AppendFormat(" WHERE {0} ", where);
                result.Parameters = parameters.ToArray();
            }
            sql.Append(obj.OrderBy ?? "");
            sql.Append(entityPersister.Driver.MultipleQueriesSeparator);
            result.CommandText = sql.ToString();
            return result;
        }


        public static CmdParameter CreateSelectSql2(ISessionImplementor session, IEntityPersister entityPersister, Criteria criteria, Order order)
        {
            var classMetadata = entityPersister.ClassMetadata;
            var result = new CmdParameter();


            //if (string.IsNullOrEmpty(obj.Conditaion))
            //{
            //    var columnInfo = classMetadata.ColumnInfos.Find(t => t.IsPrimaryKey);
            //    if (columnInfo != null)
            //    {
            //        var v = columnInfo.PropertyInfo.FastGetValue(obj);
            //        if (!IsNull(v)) result = CreateSelectSql(session, entityPersister, v);
            //    }

            //}
            var parameters = new List<DbParameter>();
            criteria.Driver = entityPersister.Driver;
            criteria.DicParameter = entityPersister.GetParameter(session);
            criteria.DbParameter = parameters;
            //entityPersister.Driver.FormatNameForSql(columnInfo.Name)

            // string rConditaion = GetConditaion(session, entityPersister, obj, out parameters);

            var sql = new StringBuilder("SELECT ");
            //sql.Append(string.IsNullOrEmpty(obj.ResultColumns) ? "*" : obj.ResultColumns);
            sql.Append("*");
            sql.AppendFormat(" FROM {0} ", classMetadata.TableName);
            if (criteria != null && criteria.CriterionList != null && criteria.CriterionList.Count > 0)
            {
                sql.Append(" WHERE ").Append(criteria.ToString(classMetadata));
                result.Parameters = parameters.ToArray();
            }
            // entityPersister.SetDbParamter(session, parameters, columnInfo.Name, value);
            //var where = GetWhereCaluse(rConditaion);
            //if (where.Length > 0)
            //{
            //    sql.AppendFormat(" WHERE {0} ", where);
            //    result.Parameters = parameters.ToArray();
            //}
            //sql.Append(obj.OrderBy ?? "" );
            sql.Append(order != null ? order.ToString(classMetadata) : "");
            sql.Append(entityPersister.Driver.MultipleQueriesSeparator);
            result.CommandText = sql.ToString();
            log.Info(sql);
            return result;
        }

        #endregion

        public static void StringBuilderAppend(StringBuilder sb, string template, int value)
        {
            if (value > 0) sb.AppendFormat(template, value);
        }

        public static void StringBuilderAppend(StringBuilder sb, string template, float value)
        {
            if (value > 0) sb.AppendFormat(template, value);
        }

        public static void StringBuilderAppend(StringBuilder sb, string template, string value)
        {
            var v = Escape(value);
            if (!string.IsNullOrEmpty(v)) sb.AppendFormat(template, v);
        }

        public static void StringBuilderAppend(StringBuilder sb, string template, string value, string defValue)
        {
            var v = Escape(value);
            if (!string.IsNullOrEmpty(v))
            {
                sb.AppendFormat(template, v);
            }
            else
            {
                sb.AppendFormat(template, defValue);
            }
        }

        public static void StringBuilderAppend(StringBuilder sb, string template, DateTime value)
        {
            if (value > MinDateTime) sb.AppendFormat(template, value);
        }

        public static string FiltrateSql(StringBuilder sb, char chr, char endChar)
        {
            if (sb[sb.Length - 1] == chr) sb[sb.Length - 1] = endChar;
            return sb.ToString();
        }

        public static DateTime ConvertDateFromLong(long second)
        {
            return new DateTime(second * (long)1e7);
        }

        public static long ConvertDateToLong(DateTime dateTime)
        {
            return dateTime.Ticks / (long)1e7;
        }

        /// <summary>
        /// Escapes an input string for database processing, that is, 
        /// surround it with quotes and change any quote in the string to 
        /// two adjacent quotes (i.e. escape it). 
        /// If input string is null or empty a NULL string is returned.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>Escaped output string.</returns>
        public static string Escape(string str)
        {
            return Escape(str, "");
        }

        public static string Escape(string text, char escape)
        {
            if ((!text.Contains("%") && !text.Contains("_")) && (!text.Contains("[") && !text.Contains("^")))
            {
                return text;
            }
            var builder = new StringBuilder(text.Length);
            foreach (var ch in text)
            {
                if (((ch == '%') || (ch == '_')) || (((ch == '[') || (ch == '^')) || (ch == escape)))
                {
                    builder.Append(escape);
                }
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static string Escape(string text, string escape)
        {
            if (!text.Contains("%") && !text.Contains("_") && !text.Contains("[") && !text.Contains("^") && !text.Contains("'") && !text.Contains("\"")) return text;
            return text.Replace("%", escape).Replace("_", escape).Replace("[", escape).Replace("^", escape).Replace(",", escape).Replace("\"", escape);
        }

        /// <summary>
        /// Get in expression with vaules
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string GetInExpression(params int[] values)
        {
            var sql = new StringBuilder();
            int count = values.Length;
            if (count > 1)
            {
                sql.Append(" IN (");
                for (int i = 0; i < count; i++) sql.AppendFormat("{0},", values[i]);
                sql.Replace(',', ')', sql.Length - 1, 1);

            }
            else if (count == 1)
            {
                sql.AppendFormat(" = {0}", values[0]);
            }
            return sql.ToString();
        }

        /// <summary>
        /// Get in expression with vaules
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string GetInExpression(IList<string> values)
        {
            var sql = new StringBuilder();
            int count = values.Count;
            if (count > 1)
            {
                sql.Append(" IN (");
                for (int i = 0; i < count; i++) sql.AppendFormat("'{0}',", Escape(values[i], ""));
                sql.Replace(',', ')', sql.Length - 1, 1);

            }
            else if (count == 1)
            {
                sql.AppendFormat(" = '{0}'", values[0]);
            }
            return sql.ToString();
        }

    }

    public class CmdParameter
    {
        public string CommandText { get; set; }

        public DbParameter[] Parameters { get; set; }

    }
}


