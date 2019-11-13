using System;


namespace visitor_lib
{
    public interface IVisitor
	{
		void Visit(GameElement gameObject);
        void Visit(TextElement textObject);
	}
}