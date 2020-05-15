
namespace Visitor
{
    public class Revealer : IVisitor
	{
		public void Visit(GameElement gameObject)
		{
			gameObject.Active = true;
		}

		public void Visit(TextElement textElement)
		{
			textElement.Active = true;
		}
	}
}
