using UnityEngine;

namespace SIUE.ControllerGames.PoolSystem
{
    public class PoolFabric
    {
        public IPool<T> CreatePool<T>(T item) where T : Component
        {
            IPool<T> pool = new Pool<T>(item);
            return pool;
        }
    }
}
