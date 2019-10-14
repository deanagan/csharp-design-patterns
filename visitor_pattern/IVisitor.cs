using System;


namespace visitor_pattern
{
    interface IVisitor
	{	
		void Visit(GameElement gameObject);
        void Visit(TextElement textObject);
	}
}