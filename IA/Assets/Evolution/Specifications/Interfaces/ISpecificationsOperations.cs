using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Evolution.Specifications.Interfaces
{
    public interface ISpecificationsOperations
    {
        ISpecifications AddSpecifications(ISpecifications specifications1, ISpecifications specifications2);
        ISpecifications ScalarMultiplySpecifications(ISpecifications specifications, float scalar);
    }
}
