// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEntity.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IEntity type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VipSoft.Core.Entity
{
   public abstract class IEntity
    {
        #region Properties

        /// <summary>
        /// Gets and sets the reult columns that is used for query data(Select method)
        /// </summary>
       public string ResultColumns { get; set; }

        /// <summary>
        /// Gets and sets the conditaion.
        /// </summary>
       public string Conditaion { get; set; }

       public string OrderBy { get; set; }

        #endregion Properties
    }
}
