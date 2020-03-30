using System;

namespace flyweight_pattern
{
    public interface Blob
    {
        void Download(int id, string blobContent);
    }
}
