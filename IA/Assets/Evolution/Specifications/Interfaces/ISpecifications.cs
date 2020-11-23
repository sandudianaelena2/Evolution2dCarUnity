using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Evolution.Specifications.Interfaces
{
    public interface ISpecifications
    {
        void ChangeGameObject(GameObject car);
        void RegenerateValues();

        Dictionary<string,Tuple< float,float>> GetSpecifications();

        void RemoveObject(GameObject car);
    }
}