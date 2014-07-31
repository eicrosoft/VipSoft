using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using VipSoft.Core.Driver;
using VipSoft.Core.Entity;
using VipSoft.FastReflection;

namespace VipSoft.Core.Utility
{
    public class Criterion
    {
        //private Map<String, SqlParameter> parameterMap;
        public IDriver Driver { get; set; }
        public IDictionary<string, DbParameter> DicParameter { get; set; }
        public IList<DbParameter> DbParameter { get; set; } //= new List<DbParameter>();
        #region Private

        private string OperationToString(ClassMetadata classMetadata)
        {
            var result = new StringBuilder();
            var columnName = "";
            var parameters = new List<DbParameter>();
            foreach (ColumnInfo columnInfo in classMetadata.ColumnInfos)
            {
                if (!columnInfo.IsColumn) continue;
                if (columnInfo.PropertyName.ToUpper() == Name.ToUpper())
                {
                    columnName = columnInfo.Name;
                }

                //if (IsNull(value)) continue;
                //if (IsNullValue(value) && columnInfo.IsNullable) continue;

                //var index = Contains(obj.Conditaion, columnInfo.PropertyName);
                //if (index == 0)
                //{
                //    rConditaion = rConditaion.Replace(string.Format(LikeString[0], columnInfo.PropertyName), entityPersister.Driver.FormatNameForSql(columnInfo.Name));
                //    entityPersister.SetDbParamter(session, parameters, columnInfo.Name, value);
                //    //entityPersister.SetDbParamter(session, parameters, classMetadata.PrimaryKey, value);
                //}
                //else if (index > 0)
                //{
                //    rConditaion = rConditaion.Replace(string.Format(LikeString[index], columnInfo.PropertyName), string.Format(LikeValueString[index - 1], Escape(value.ToString(), "")));
                //}
            }

            var paramName = Driver.FormatNameForSql(columnName);
            switch (Operation)
            {
                case OP.EQ:
                    result.Append(columnName).Append("=").Append(paramName);
                    SetDbParamter(DbParameter, columnName, Value);
                    break;     
                case OP.NE:
                    result.Append(columnName).Append("<>").Append(paramName);
                    SetDbParamter(DbParameter, columnName, Value);
                    break;
                case OP.GE:
                    result.Append(columnName).Append(">=").Append(paramName);
                    SetDbParamter(DbParameter, columnName, Value);
                    break;
                case OP.LE:
                    result.Append(columnName).Append("<=").Append(paramName);
                    SetDbParamter(DbParameter, columnName, Value);
                    break;
                case OP.GT:
                    result.Append(columnName).Append(">").Append(paramName);
                    SetDbParamter(DbParameter, columnName, Value);
                    break;
                case OP.LT:
                    result.Append(columnName).Append("<").Append(paramName);
                    SetDbParamter(DbParameter, columnName, Value);
                    break;
                //        case OP.BETWEEN:
                //            String min = new StringBuilder("min").append(propertyName).toString();
                //            String max = new StringBuilder("max").append(propertyName).toString();
                //            sql.append("(").append(columnName).append(">=").append(prefixInSql).append(min);
                //            sql.append(" AND ");
                //            sql.append(columnName).append("<=").append(prefixInSql).append(max).append(")");
                //            Object[] v = ArrayUtil.toObjectArray(value);
                //            parameterMap.put(min, new SqlParameter(min, v[0], typeName, sqlType, scale));
                //            parameterMap.put(max, new SqlParameter(max, v[1], typeName, sqlType, scale));
                //            break;
                //case OP.IN:
                //    result.Append(columnName).Append(" IN (").Append(paramName).Append(") ");
                //    SetDbParamter(DbParameter, columnName, Value);

                //    //sql.append(columnName).append(" IN (");
                //    ////如果数据库是Oralce时，使用时注意In表达式内的变量数在100之内，当变量数大于100时建议客户程序分批处理。
                //    //buildInOperation(sql);//update by LDG，				
                //    ////buildInOperation(prefixInSql, sql, typeName, sqlType, scale, propertyName);
                //    //sql.append(") ");
                //    break;
                //        case OP.NOTIN:
                //            sql.append(columnName).append(" NOT IN (");
                //            buildInOperation(sql);
                //            //buildInOperation(prefixInSql, sql, typeName, sqlType, scale, propertyName);
                //            sql.append(") ");
                //            break;                                         
                case OP.LIKE:
                    result.Append(columnName).Append("  LIKE ").Append(paramName);   
                    SetDbParamter(DbParameter, columnName, string.Format("%{0}%",Value));                                                       
                    break;
                case OP.FLIKE:
                    result.Append(columnName).Append("  LIKE ").Append(paramName);
                    SetDbParamter(DbParameter, columnName, string.Format("{0}%", Value));             
                    break;
                case OP.ELIKE:                                                                 
                    result.Append(columnName).Append(" LIKE ").Append(paramName);
                    SetDbParamter(DbParameter, columnName, string.Format("%{0}", Value));   
                    break;
                //case OP.IS: 
                //    result.Append(columnName).Append(" IS ").Append(paramName);
                //    SetDbParamter(DbParameter, columnName, Value);
                //    break;
                //case OP.UEMPTY:                                    
                //    result.Append(columnName).Append(" IS NOT NULL");
                //    break;
            }
            return result.ToString();
        }

        private void SetDbParamter(ICollection<DbParameter> paramters, string columnName, object vaule)
        {
            DbParameter result;
            DicParameter.TryGetValue(columnName, out result);
            if (result == null) return;
            var p = (DbParameter)((ICloneable)(result)).Clone();
            p.Size = result.Size;
            p.Value = vaule ?? DBNull.Value;
            paramters.Add(p);
        }
        #endregion

        #region Public

        #region Feild
        public LOP LogicOperation { get; set; }

        //属性名
        public String Name { get; set; }

        public Criteria Criteria { get; set; }

        public Type DataType { get; set; }

        public OP Operation { get; set; }

        public Object Value { get; set; }
        #endregion

        #region Constructor

        public Criterion(Criteria criteria)
        {
            LogicOperation = LOP.AND;
            Value = criteria;
        }

        public Criterion(String name, Object value)
        {
            //Criterion(LOP.AND, name, OP.EQ, value);
            this.LogicOperation = LOP.AND;
            Name = name;
            Operation = OP.EQ;
            Value = value;
        }

        public Criterion(String name, OP operation, Object value)
        {
            //Criterion(LOP.AND, name, operation, value);
            this.LogicOperation = LOP.AND;
            Name = name;
            Operation = operation;
            Value = value;
        }

        public Criterion(LOP logicOperation, Criteria criteria)
        {
            LogicOperation = logicOperation;
            Value = criteria;
        }

        public Criterion(LOP logicOperation, String name, OP operation, Object value)
        {
            this.LogicOperation = logicOperation;
            Name = name;
            Operation = operation;
            Value = value;
        }
        #endregion


        public string ToString(ClassMetadata classMetadata)
        {
            var result = new StringBuilder();
            if (Value is Criteria)
            {
                result.Append("depth=1");
            }
            else
            {
                result.Append(OperationToString(classMetadata));
            }
            return result.ToString();
        }


        //private void buildInOperation(StringBuilder sql) {
        //    if ((((Object[]) value).Length > 0)) {
        //        Object[] objects = (Object[]) value;
        //        String flag = ObjectUtil.isNumber(objects[0]) ? "" : "'";
        //        for (int i = 0; i < objects.length; i++) {
        //            if (i > 0) {
        //                sql.append(",");
        //            }
        //            sql.append(flag).append(objects[i]).append(flag);
        //        }
        //    }
        //    if (ObjectUtil.isCollection(value)) {
        //        Collection objects = (Collection) value;
        //        String flag = ObjectUtil.isNumber(objects.iterator().next()) ? "" : "'";
        //        int i = 0;
        //        for (Object obj : objects) {
        //            if (i > 0) {
        //                sql.append(",");
        //            }
        //            i++;
        //            sql.append(flag).append(obj).append(flag);
        //        }
        //    }
        //}


        //private String operationToString(ClassMetadata classMetadata, String prefixInSql) {
        //    StringBuilder sql = new StringBuilder();
        //    String typeName = null;
        //    int sqlType = Integer.MIN_VALUE;
        //    Integer scale = null;
        //    String columnName = null;
        //    String propertyName = null;		
        //    if (classMetadata != null && (classMetadata.getProperty().containsKey(name) || classMetadata.getColumns().containsKey(name.toUpperCase()))) {		
        //        LinkedHashMap<String, PropertyMetadata> pMetadata= classMetadata.getProperty();
        //        LinkedHashMap<String, String> columns=classMetadata.getColumns(); 
        //        PropertyMetadata propertyMetada =pMetadata.containsKey(name) ?pMetadata.get(name) :pMetadata.get(columns.get(name.toUpperCase()));
        //        propertyName = propertyMetada.getName();
        //        columnName = propertyMetada.getColumn();
        //        sqlType = ObjectUtil.javaTypeToSqlParameterType(propertyMetada.getPropertyDescriptor().getPropertyType());
        //        typeName = propertyMetada.getPropertyDescriptor().getName();
        //        scale = propertyMetada.getScale();
        //    } else {
        //        columnName = name;
        //        if (dataType == null) {
        //            propertyName = StringUtil.formatKey(name);
        //            Class pClazz = ObjectUtil.isArray(value) ? value.getClass().getComponentType() : value.getClass();
        //            typeName = pClazz.getName();
        //            sqlType = ObjectUtil.javaTypeToSqlParameterType(pClazz);
        //        } else {
        //            if ((StringUtils.contains(columnName, "MIN_") || StringUtils.contains(columnName, "MAX_")) && StringUtils.endsWith(columnName, "DATE")) {
        //                propertyName = columnName;
        //                if ((StringUtils.contains(columnName, "MIN_")))columnName = StringUtils.replace(columnName, "MIN_", "", 1);
        //                if ((StringUtils.contains(columnName, "MAX_")))columnName = StringUtils.replace(columnName, "MAX_", "", 1);

        //            } else {
        //                propertyName = columnName;
        //            }
        //            typeName = dataType.getName();
        //            sqlType = ObjectUtil.javaTypeToSqlParameterType(dataType);
        //        }
        //    }

        //    // String parameterName = new
        //    // StringBuilder(prefixInSql).append(propertyName).toString();
        //    switch (Operation) {
        //        case OP.EQ:
        //            sql.append(columnName).append("=").append(prefixInSql).append(propertyName);
        //            parameterMap.put(propertyName, new SqlParameter(propertyName, value, typeName, sqlType, scale));
        //            break;
        //        case OP.NE:
        //            sql.append(columnName).append("<>").append(prefixInSql).append(propertyName);
        //            parameterMap.put(propertyName, new SqlParameter(propertyName, value, typeName, sqlType, scale));
        //            break;
        //        case OP.GE:
        //            sql.append(columnName).append(">=").append(prefixInSql).append(propertyName);
        //            parameterMap.put(propertyName, new SqlParameter(propertyName, value, typeName, sqlType, scale));
        //            break;
        //        case OP.LE:
        //            sql.append(columnName).append("<=").append(prefixInSql).append(propertyName);
        //            parameterMap.put(propertyName, new SqlParameter(propertyName, value, typeName, sqlType, scale));
        //            break;
        //        case OP.GT:
        //            sql.append(columnName).append(">").append(prefixInSql).append(propertyName);
        //            parameterMap.put(propertyName, new SqlParameter(propertyName, value, typeName, sqlType, scale));
        //            break;
        //        case OP.LT:
        //            sql.append(columnName).append("<").append(prefixInSql).append(propertyName);
        //            parameterMap.put(propertyName, new SqlParameter(propertyName, value, typeName, sqlType, scale));
        //            break;
        //        case OP.BETWEEN:
        //            String min = new StringBuilder("min").append(propertyName).toString();
        //            String max = new StringBuilder("max").append(propertyName).toString();
        //            sql.append("(").append(columnName).append(">=").append(prefixInSql).append(min);
        //            sql.append(" AND ");
        //            sql.append(columnName).append("<=").append(prefixInSql).append(max).append(")");
        //            Object[] v = ArrayUtil.toObjectArray(value);
        //            parameterMap.put(min, new SqlParameter(min, v[0], typeName, sqlType, scale));
        //            parameterMap.put(max, new SqlParameter(max, v[1], typeName, sqlType, scale));
        //            break;
        //        case OP.IN:
        //            sql.append(columnName).append(" IN (");
        //            //如果数据库是Oralce时，使用时注意In表达式内的变量数在100之内，当变量数大于100时建议客户程序分批处理。
        //            buildInOperation(sql);//update by LDG，				
        //            //buildInOperation(prefixInSql, sql, typeName, sqlType, scale, propertyName);
        //            sql.append(") ");
        //            break;
        //        case OP.NOTIN:
        //            sql.append(columnName).append(" NOT IN (");
        //            buildInOperation(sql);
        //            //buildInOperation(prefixInSql, sql, typeName, sqlType, scale, propertyName);
        //            sql.append(") ");
        //            break;
        //        case OP.LIKE:
        //            sql.append(columnName).append(" LIKE '%'||").append(prefixInSql).append(propertyName).append("||'%'");
        //            parameterMap.put(propertyName, new SqlParameter(propertyName, value, typeName, sqlType, scale));
        //            break;
        //        case OP.FLIKE:
        //            sql.append(columnName).append(" LIKE ").append(prefixInSql).append(propertyName).append("||'%'");
        //            parameterMap.put(propertyName, new SqlParameter(propertyName, value, typeName, sqlType, scale));
        //            break;
        //        case OP.ELIKE:
        //            sql.append(columnName).append(" LIKE '%'||").append(prefixInSql).append(propertyName);
        //            parameterMap.put(propertyName, new SqlParameter(propertyName, value, typeName, sqlType, scale));
        //            break;
        //        case OP.IS:
        //            sql.append(columnName).append(" IS ").append(value);
        //            //parameterMap.put(propertyName, new SqlParameter(propertyName, value, typeName, sqlType, scale));
        //            break;
        //        case OP.UEMPTY:
        //            sql.append(columnName).append(" IS NOT NULL");
        //            break;
        //    }
        //    return sql.toString();
        //}



        //public String toSql(ClassMetadata classMetadata, String prefixInSql, int index) {
        //    StringBuilder sql = new StringBuilder();
        //    if (index > 0) {
        //        sql.append(logicOperation.equals(LOP.OR) ? " OR " : " AND ");
        //    }
        //    if (ObjectUtil.isNullOrEmpty(parameterMap)) {
        //        parameterMap = new HashMap<String, SqlParameter>();
        //    }
        //    if (value instanceof Criteria) {
        //        sql.append("(");
        //        sql.append(((Criteria) value).toSql(classMetadata, prefixInSql));
        //        sql.append(")");
        //        parameterMap.putAll(((Criteria) value).getParameterMap());
        //    } else {
        //        sql.append(operationToString(classMetadata, prefixInSql));
        //    }
        //    return sql.toString();
        //}

        //@Override
        //public String toString() {
        //    return new ToStringBuilder(this).append("name", name).append("operation", operation).append("value", value).toString();
        //}

        //public Map<String, SqlParameter> getParameterMap() {
        //    return parameterMap;
        //}

        //public Class<?> getDataType() {
        //    return dataType;
        //}

        //public void setDataType(Class<?> dataType) {
        //    this.dataType = dataType;
        //}

        #endregion
    }
}
