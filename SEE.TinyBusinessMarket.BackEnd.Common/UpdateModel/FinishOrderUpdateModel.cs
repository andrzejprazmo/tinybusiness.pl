//-----------------------------------------------------------------------
// <copyright file="FinishOrderUpdateModel.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.UpdateModel
{
    using SEE.Framework.Core.DTO;
    using System;
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
	

    public class FinishOrderUpdateModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization.Resource), ErrorMessageResourceName = "Validation_ValueRequired")]
        public Data<DateTime?> SellDate { get; set; } = new Data<DateTime?>();
    }
}
