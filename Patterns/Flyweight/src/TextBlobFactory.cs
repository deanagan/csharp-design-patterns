using System;

namespace Flyweight
{
    public interface TextBlobFactory
    {
        TextBlob GetTextBlob(int id);
    }
}
