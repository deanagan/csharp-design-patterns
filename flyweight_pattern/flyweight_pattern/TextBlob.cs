using System;

namespace FlyweightPattern
{
    public interface Blob
    {
        void Download(int id, string blobContent);
    }
}
