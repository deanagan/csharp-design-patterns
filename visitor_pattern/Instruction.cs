
namespace visitor_pattern
{
class Instruction : TextElement
{
    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

}