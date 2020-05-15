using System;

namespace visitor_lib
{
    public abstract class TextElement
	{
		public abstract void Accept(IVisitor visitor);
		public bool Active { get; set; } = false;
        public string Message { get; set; } = string.Empty;
	}
}