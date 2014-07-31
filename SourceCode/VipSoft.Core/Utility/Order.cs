using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VipSoft.Core.Entity;

namespace VipSoft.Core.Utility
{
    public class Order
    {
        private readonly IList<SubOrder> subOrders;

        /// <summary>
        /// Default Ascending
        /// </summary>
        /// <param name="fieldName"></param>
        public Order(String fieldName)
        {
            if (subOrders == null)
            {
                subOrders = new List<SubOrder>();
            }
            subOrders.Add(new SubOrder(fieldName));
        }

        public Order(String fieldName, bool ascending)
        {
            if (subOrders == null)
            {
                subOrders = new List<SubOrder>();
            }
            subOrders.Add(new SubOrder(fieldName, ascending));
        }

        public Order Asc(String fieldName)
        {
            subOrders.Add(new SubOrder(fieldName, true));
            return this;
        }

        public void Desc(String fieldName)
        {
            subOrders.Add(new SubOrder(fieldName, false));
        }

        public string ToString(ClassMetadata classMetadata)
        {
            var orderStr = new StringBuilder();
            var i = 0;
            foreach (var subOrder in subOrders)
            {
                if (i > 0)
                {
                    orderStr.Append(",");
                }
                orderStr.Append(subOrder.ToString(classMetadata));
                i++;
            }
            return orderStr.Length > 0 ? string.Format(" ORDER BY {0}" , orderStr) : "";
        }

    }

    class SubOrder
    {
        private bool IsAscending { get; set; }   
        private string PropertyName { get; set; }

        public string ToString(ClassMetadata classMetadata)
        {
            var columnInfo = classMetadata.ColumnInfos.Find(p => p.PropertyName == this.PropertyName);
            return columnInfo != null ? new StringBuilder().Append(columnInfo.Name).Append(" ").Append(IsAscending ? "ASC" : "DESC").ToString() : string.Empty;
        }

        public SubOrder(String propertyName)
        {
            this.PropertyName = propertyName;
            this.IsAscending = true;
        }

        public SubOrder(String propertyName, bool ascending)
        {
            this.PropertyName = propertyName;
            this.IsAscending = ascending;
        }
    }
}