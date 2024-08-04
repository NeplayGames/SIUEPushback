using UnityEngine;

namespace SIUE.ControllerGames.PoolSystem
{
    public class PoolFabric
    {
        public IPool<T> CreatePool<T>(T item) where T : Component
        {
         }
    }
}
