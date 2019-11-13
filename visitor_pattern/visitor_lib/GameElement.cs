using System;

namespace visitor_lib
{
    public abstract class GameElement
	{
		public bool Active { get; set; } = false;
		public abstract void Accept(IVisitor visitor);

	}
}