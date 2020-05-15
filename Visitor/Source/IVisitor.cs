namespace Visitor
{
    public interface IVisitor
	{
		void Visit(GameElement gameObject);
        void Visit(TextElement textObject);
	}
}
