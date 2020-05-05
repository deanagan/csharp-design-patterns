using System;

namespace FlyweightPattern
{
    public interface TextDownloader
    {
        void DownloadFile(string url, string blobContent);
    }
}
