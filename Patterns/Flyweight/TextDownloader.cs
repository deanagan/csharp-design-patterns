using System;

namespace Flyweight
{
    public interface TextDownloader
    {
        void DownloadFile(string url, string blobContent);
    }
}
