//-----------------------------------------------------------------------
// <copyright file="CustomerUpdateModel.cs" company="SEE Software">
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
	

    public class CustomerUpdateModel
    {
        public Data<Guid> Id { get; set; } = new Data<Guid>();

        [Required(ErrorMessageResourceType = typeof(Localization.Resource), ErrorMessageResourceName = "Validation_ValueRequired")]
        public Data<string> Name { get; set; } = new Data<string>();

        [Required(ErrorMessageResourceType = typeof(Localization.Resource), ErrorMessageResourceName = "Validation_ValueRequired")]
        [NipValidation(ErrorMessageResourceType = typeof(Localization.Resource), ErrorMessageResourceName = "Validation_NipBadFormat")]
        public Data<string> Nip { get; set; } = new Data<string>();

        [Required(ErrorMessageResourceType = typeof(Localization.Resource), ErrorMessageResourceName = "Validation_ValueRequired")]
        public Data<string> StreetNumber { get; set; } = new Data<string>();

        [Required(ErrorMessageResourceType = typeof(Localization.Resource), ErrorMessageResourceName = "Validation_ValueRequired")]
        public Data<string> ZipCode { get; set; } = new Data<string>();

        [Required(ErrorMessageResourceType = typeof(Localization.Resource), ErrorMessageResourceName = "Validation_ValueRequired")]
        public Data<string> City { get; set; } = new Data<string>();

        [EmailValidation(ErrorMessageResourceType = typeof(Localization.Resource), ErrorMessageResourceName = "Validation_EmailBadFormat")]
        [Required(ErrorMessageResourceType = typeof(Localization.Resource), ErrorMessageResourceName = "Validation_ValueRequired")]
        public Data<string> Email { get; set; } = new Data<string>();

        [Required(ErrorMessageResourceType = typeof(Localization.Resource), ErrorMessageResourceName = "Validation_ValueRequired")]
        public Data<string> FirstName { get; set; } = new Data<string>();

        [Required(ErrorMessageResourceType = typeof(Localization.Resource), ErrorMessageResourceName = "Validation_ValueRequired")]
        public Data<string> LastName { get; set; } = new Data<string>();

        public Data<string> Phone { get; set; } = new Data<string>();
    }
}
