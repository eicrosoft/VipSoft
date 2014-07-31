using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using VipSoft.Core.Driver;
using VipSoft.Core.Entity;

namespace VipSoft.Core.Utility
{
    public class Criteria
    {

        //private Map<String, SqlParameter> parameterMap;
        public IDriver Driver { get; set; }
        public IList<Criterion> CriterionList { get; set; }
        public IDictionary<string, DbParameter> DicParameter { get; set; }
        public IList<DbParameter> DbParameter { get; set; }

        #region Constructor


        public Criteria()
        {
            CriterionList = new List<Criterion>();
        }

        /// <summary>
        /// 添加 = 判断条件。
        /// </summary>
        /// <param name="name">PropertyName</param>
        /// <param name="value">Value</param>
        public Criteria(String name, Object value)
        {
            CriterionList = new List<Criterion>();
            AddCriterion(new Criterion(name, OP.EQ, value));
        }

        public Criteria(String name, OP operation, Object value)
        {
            CriterionList = new List<Criterion>();
            AddCriterion(new Criterion(name, operation, value));
        }
        #endregion

        #region Public
        
       
        public void AddCriterion(Criterion criterion)
        {
            CriterionList.Add(criterion);
        }

        public Criteria Add(Criteria criteria)
        {
            this.AddCriterion(new Criterion(criteria));
            return this;
        }

        public Criteria Add(String name, Object value)
        {
            this.AddCriterion(new Criterion(name, value));
            return this;
        }

        public Criteria Add(String name, OP operation, Object value)
        {
            this.AddCriterion(new Criterion(name, operation, value));
            return this;
        }

        public Criteria Add(LOP logicOperation, String name, Object value)
        {
            this.AddCriterion(new Criterion(logicOperation, name, OP.EQ, value));
            return this;
        }

        public Criteria Add(LOP logicOperation, String name, OP operation, Object value)
        {
            this.AddCriterion(new Criterion(logicOperation, name, operation, value));
            return this;
        }


        public Criteria Add(LOP logicOperation, Criteria criteria)
        {
            this.AddCriterion(new Criterion(logicOperation, criteria));
            return this;
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


        public string ToString(ClassMetadata classMetadata)
        {
            var result = new StringBuilder();
            foreach (var criterion in this.CriterionList)
            {                
                criterion.Driver = this.Driver;
                criterion.DicParameter = this.DicParameter;
                criterion.DbParameter = this.DbParameter;
                result.Append(criterion.ToString(classMetadata));    
            }
            return result.ToString();
        }

        #endregion
        //        private static string GetConditaion(ISessionImplementor session, IEntityPersister entityPersister, IEntity obj, out List<DbParameter> parameters)
        //        {
        //            var rConditaion = obj.Conditaion;
        //            var classMetadata = entityPersister.ClassMetadata;
        //            parameters = new List<DbParameter>();
        //            foreach (ColumnInfo columnInfo in classMetadata.ColumnInfos)
        //            {
        //                var value = columnInfo.PropertyInfo.FastGetValue(obj);
        //                if (IsNull(value)) continue;
        //                if (IsNullValue(value) && columnInfo.IsNullable) continue;
        //
        //                var index = Contains(obj.Conditaion, columnInfo.PropertyName);
        //                if (index == 0)
        //                {
        //                    rConditaion = rConditaion.Replace(string.Format(LikeString[0], columnInfo.PropertyName), entityPersister.Driver.FormatNameForSql(columnInfo.Name));
        //                    entityPersister.SetDbParamter(session, parameters, columnInfo.Name, value);
        //                    //entityPersister.SetDbParamter(session, parameters, classMetadata.PrimaryKey, value);
        //                }
        //                else if (index > 0)
        //                {
        //                    rConditaion = rConditaion.Replace(string.Format(LikeString[index], columnInfo.PropertyName), string.Format(LikeValueString[index - 1], Escape(value.ToString(), "")));
        //                }
        //            }
        //            return rConditaion;
        //        }
        //
        //
        //        public String toSql(IEntityPersister entityPersister ClassMetadata classMetadata, String prefixInSql) {
        //        
        //
        //            StringBuilder sql = new StringBuilder();
        //            if (ObjectUtil.isNullOrEmpty(parameterMap)) {parameterMap = new HashMap<String, SqlParameter>();}
        //            int i = 0;
        //            for (Criterion criterion : criteria) {
        //                sql.append(criterion.toSql(classMetadata, prefixInSql, i));
        //                if (ObjectUtil.isNotEmpty(criterion.getParameterMap())) {parameterMap.putAll(criterion.getParameterMap());}
        //                i++;
        //            }
        //            return sql.toString();
        //        }


        ///**
        // * 
        // * 加同级条件而不是作为子条件
        // */
        //public Criteria addAll(Criteria otherCriteria) {
        //    criteria.addAll(otherCriteria.getCriteria());
        //    return this;
        //}

        //public Boolean contains(String Key){
        //    for(Criterion criterion:criteria){
        //        if(criterion.getName().equals(Key)){
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        //public Object get(String Key){
        //    for(Criterion criterion:criteria){
        //        if(Key.equals(criterion.getName())){
        //            return criterion.getValue();
        //        }
        //    }
        //    return null;
        //}
        //public Boolean remove(String Key){
        //    for(Criterion criterion:criteria){
        //        if(Key.equals(criterion.getName())){
        //            criteria.remove(criterion);
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        //@Override
        //public boolean equals(final Object other) {
        //    if (!(other instanceof Criteria)) {
        //        return false;
        //    }
        //    Criteria castOther = (Criteria) other;
        //    return new EqualsBuilder().append(criteria, castOther.criteria).isEquals();
        //}

        //public List<Criterion> getCriteria() {
        //    return criteria;
        //}

        //@Override
        //public int hashCode() {
        //    return new HashCodeBuilder().append(criteria).toHashCode();
        //}

        //public void setCriteria(List<Criterion> criteria) {
        //    this.criteria = criteria;
        //}

        //@Override
        //public String toString() {
        //    return new ToStringBuilder(this).append("criteria", criteria).toString();
        //}

        //public Map<String, SqlParameter> getParameterMap() {
        //    return parameterMap;
        //}

        //public String toSql(ClassMetadata classMetadata, String prefixInSql) {
        //    StringBuilder sql = new StringBuilder();
        //    if (ObjectUtil.isNullOrEmpty(parameterMap)) {parameterMap = new HashMap<String, SqlParameter>();}
        //    int i = 0;
        //    for (Criterion criterion : criteria) {
        //        sql.append(criterion.toSql(classMetadata, prefixInSql, i));
        //        if (ObjectUtil.isNotEmpty(criterion.getParameterMap())) {parameterMap.putAll(criterion.getParameterMap());}
        //        i++;
        //    }
        //    return sql.toString();
        //}
    }
}
