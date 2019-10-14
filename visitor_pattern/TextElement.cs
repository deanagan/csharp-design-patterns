using System;

namespace visitor_pattern
{
    abstract class TextElement
	{	
		public abstract void Accept(IVisitor visitor);
		public bool Active { get; set; } = false;
        public string Message { get; set; } = string.Empty;
	}
}