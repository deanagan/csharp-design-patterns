using System;

namespace FlyweightPattern
{
    public class XmlTextBlob : TextBlob
    {
        private TextDownloader textDownloader;

        public XmlTextBlob(TextDownloader textDownloader)
        {
            this.textDownloader = textDownloader;
        }
        public void Download(int id, string blobContent)
        {
            textDownloader.DownloadFile($"https://localhost:5001/api/getitem/{id}", blobContent);
        }
    }
}
