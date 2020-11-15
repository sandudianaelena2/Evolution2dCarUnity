using UnityEngine;

namespace Assets.Evolution.Specifications.Interfaces
{
    public interface ISpecifications
    {
        void ChangeGameObject(GameObject car);
        void RegenerateValues();

        void RemoveObject(GameObject car);
    }
}