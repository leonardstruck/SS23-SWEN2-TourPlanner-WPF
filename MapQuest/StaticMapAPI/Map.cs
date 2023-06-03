using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapQuest.StaticMapAPI
{
    public class Map
    {
        private readonly byte[] _bytes;
        public async Task WriteToFile(string path)
        {
            await using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            await fileStream.WriteAsync(_bytes);
        }

        public Map(byte[] bytes)
        {
            this._bytes = bytes;
        }
    }
}
