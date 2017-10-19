//-----------------------------------------------------------------------
// <copyright file="FileRepository.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.File
{
	using System;
	using System.Collections.Generic;
	using System.Text;
    using System.IO;
    using System.Threading.Tasks;

    public class FileRepository : IFileRepository
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public byte[] Get(string path)
        {
            return File.ReadAllBytes(path);
        }

        public async Task<byte[]> GetAsync(string path)
        {
            byte[] buffer;
            using (var fileStream = File.OpenRead(path))
            {
                buffer = new byte[fileStream.Length];
                await fileStream.ReadAsync(buffer, 0, (int)fileStream.Length);
                return buffer;
            }
        }
    }
}
