using System;

namespace flyweight_pattern
{
    public interface TextBlobFactory
    {
        TextBlob GetTextBlob(int id);
    }
}
