//-----------------------------------------------------------------------
// <copyright file="IFileRepository.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.File
{
	using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Threading.Tasks;

    public interface IFileRepository
    {
        bool Exists(string path);
        byte[] Get(string path);
        Task<byte[]> GetAsync(string path);
    }
}
