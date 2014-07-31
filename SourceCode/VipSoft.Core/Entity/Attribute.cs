using System;
using System.Data;

namespace VipSoft.Core.Entity
{
    /// <summary>
    /// Initializes a new instance of the TableAttribute class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class TableAttribute : Attribute
    {
        public TableAttribute()
        {
        }

        public TableAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets and sets the name of the table. By default, the value is the same as the class name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets and sets the name of the associate table.
        /// </summary>
        public string AssociateTable { get; set; }

        /// <summary>
        /// Gets and sets the name of the foreign key. 
        /// </summary>
        public string ForeignKey { get; set; }
    }

    public enum ColumnType
    {
        IncrementPrimary,
        Primary,
        Unique,
        Nullable
    }

    public enum DeleteCheck
    {
        Always,
        Never,
        WhenChanged
    }

    public enum InsertCheck
    {
        Always,
        Never,
        WhenChanged
    }

    /// <summary>
    /// Initializes a new instance of the ColumnAttribute class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ColumnAttribute : Attribute
    {

        public ColumnAttribute()
        {
            ParameterDirection = ParameterDirection.Input;
        }

        public ColumnAttribute(ColumnType type)
        {
            switch (type)
            {
                case ColumnType.IncrementPrimary:
                    ParameterDirection = ParameterDirection.InputOutput;
                    IsUnique = IsPrimaryKey = IsIncrement = true;
                    break;
                case ColumnType.Primary:
                    ParameterDirection = ParameterDirection.Input;
                    IsUnique = IsPrimaryKey = true;
                    break;
                case ColumnType.Unique:
                    ParameterDirection = ParameterDirection.Input;
                    IsUnique = true;
                    break;
                case ColumnType.Nullable:
                    IsNullable = true;
                    ParameterDirection = ParameterDirection.Input;
                    break;
                default:
                    ParameterDirection = ParameterDirection.Input;
                    break;
            }

        }
                                                
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

    }
}