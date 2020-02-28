
namespace ChainOfResponsibility
{
    public interface IDiscountCalculator
    {
        IDiscountCalculator SetNextCalculator(IDiscountCalculator discountCalculator);
        decimal Calculate(Product product);
    }
}