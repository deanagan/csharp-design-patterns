
namespace visitor_pattern
{
class Sprite : GameElement
{
    public string Name {get; }
    public Sprite(string name) { Name = name; }
    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

}