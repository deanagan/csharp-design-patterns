using System;

namespace flyweight_pattern
{
    public interface TextDownloader
    {
        void DownloadFile(string url, string blobContent);
    }
}
