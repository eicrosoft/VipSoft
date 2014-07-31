using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace VipSoft.Core.Entity
{
    public class ClassMetadata
    {

        public List<ColumnInfo> ColumnInfos { get; set; }

        public System.Type EntityType { get; set; }

        public string EntityName { get; set; }
        /// <summary>
        /// Gets and sets the name of the table. By default, the value is the same as the class name.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets and sets the name of the associate table.
        /// </summary>
        public string AssociateTable { get; set; }

        /// <summary>
        /// Gets and sets the name of the foreign key. 
        /// </summary>
        public string ForeignKey { get; set; }

        public string PrimaryKey { get; set; }

        public string PKColumnName { get; set; }

    }

    public sealed class ColumnInfo
    {
        public PropertyInfo PropertyInfo { get; set; }

        public Type PropertyType { get; set; }

        public string PropertyName { get; set; }

        public string Name { get; set; }

        public bool IsInsertable { get; set; }

        public bool IsUpdatable { get; set; }

        public string ColumnDefinition { get; set; }

        public string Table { get; set; }

        public int Length { get; set; }

        public int Precision { get; set; }

        public int Scale { get; set; }

        public ParameterDirection ParameterDirection { get; set; }

        /// <summary>
        /// Gets and sets parameters as same as the property of the coloumn,except parameter name.
        /// </summary>
        public string SameParameters { get; set; }

        /// <summary>
        /// Gets and sets whether the class member represents a column that is part or all of the primary key of the table.
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// Gets and sets whether the column vaule that is the unique  of the table. 
        /// </summary>
        public bool IsUnique { get; set; }

        /// <summary>
        ///  Gets and sets whether a column can contain null vaules.
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// Gets and sets whether a column contains values that the database auto-increment
        /// </summary>
        public bool IsIncrement { get; set; }

        public bool IsColumn { get; set; }

    }
}