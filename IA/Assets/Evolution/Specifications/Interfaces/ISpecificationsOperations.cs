namespace Evolution.Specifications.Interfaces
{
    public interface ISpecificationsOperations
    {
        ISpecifications AddSpecifications(ISpecifications specifications1, ISpecifications specifications2);
        ISpecifications ScalarMultiplySpecifications(ISpecifications specifications, float scalar);
    }
}
