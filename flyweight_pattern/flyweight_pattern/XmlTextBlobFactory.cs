using System;

namespace FlyweightPattern
{
    public class TextBlobFactory
    {
        private Dictionary<int, TextBlob> textBlobDictionary = new Dictionary<int, TextBlob>();
        private TextDownloader textDownloader;
        public TextBlobFactory(TextDownloader textDownloader)
        {
            this.textDownloader = textDownloader;
        }
        public TextBlob GetTextBlob(int id)
        {
            var textBlob = textBlobDictionary[id];

            if (textBlob == null)
            {
                textBlob = new XmlTextBlob(textDownloader);
                textBlobDictionary[id] = textBlob;
            }

            return textBlob;
        }
    }
}
