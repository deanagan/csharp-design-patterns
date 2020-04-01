using System;

namespace FlyweightPattern
{
    public interface TextBlob
    {
        void Download(int id, string blobContent);
    }
}
