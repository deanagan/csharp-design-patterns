using System;

namespace FlyweightPattern
{
    public interface TextBlobFactory
    {
        TextBlob GetTextBlob(int id);
    }
}
