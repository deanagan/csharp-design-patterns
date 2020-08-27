using System;
using System.Collections.Generic;

namespace Flyweight
{
    public class XmlTextBlobFactory : TextBlobFactory
    {
        private Dictionary<int, TextBlob> textBlobDictionary = new Dictionary<int, TextBlob>();
        private TextDownloader textDownloader;
        public XmlTextBlobFactory(TextDownloader textDownloader)
        {
            this.textDownloader = textDownloader;
        }

        public TextBlob GetTextBlob(int id)
        {
            if (textBlobDictionary.ContainsKey(id))
            {
                return textBlobDictionary[id];
            }

            var textBlob = new XmlTextBlob(textDownloader);
            textBlobDictionary[id] = textBlob;
            return textBlob;
        }
    }
}
