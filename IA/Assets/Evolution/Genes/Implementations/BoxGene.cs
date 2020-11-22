using Assets.Evolution.Genes.Interfaces;
using Assets.Evolution.Specifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Evolution.Genes.Implementations
{
    class BoxGene:IGene
    {
        private bool _active;

        public BoxGene(ISpecifications specifications)
        {
            var rand = new Random();
            var number = rand.NextDouble();
            _active = true;
            if (number >= 0.5)
                _active = false;
            Specifications = specifications;
        }

        public ISpecifications Specifications { get; }

        public void Mutate(double probability)
        {
            _active = !_active;
            Specifications.RegenerateValues();
        }

        public bool IsActive()
        {
            return _active;
        }

        public ISpecifications GetSpecifications()
        {
            return Specifications;
        }
    }
}
