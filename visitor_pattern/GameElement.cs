using System;

namespace visitor_pattern
{
    public abstract class GameElement
	{	
		public bool Active { get; set; } = false;
		public abstract void Accept(IVisitor visitor);
		
	}
}