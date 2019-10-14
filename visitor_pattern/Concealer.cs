
namespace visitor_pattern
{
    class Concealer : IVisitor
	{
		public void Visit(GameElement gameObject)
		{
			gameObject.Active = false;
		}

		public void Visit(TextElement textElement)
		{
			textElement.Active = false;
		}
	}
}