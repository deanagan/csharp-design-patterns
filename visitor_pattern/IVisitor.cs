using System;


namespace visitor_pattern
{
    public interface IVisitor
	{	
		void Visit(GameElement gameObject);
        void Visit(TextElement textObject);
	}
}