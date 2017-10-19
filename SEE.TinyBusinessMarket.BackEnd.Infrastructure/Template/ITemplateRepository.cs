//-----------------------------------------------------------------------
// <copyright file="ITemplateRepository.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.Template
{
	using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Threading.Tasks;

    public interface ITemplateRepository
    {
        string Get(string relativePath);
        Task<string> RenderTemplateAsync<TViewModel>(string filename, TViewModel viewModel);
    }
}
