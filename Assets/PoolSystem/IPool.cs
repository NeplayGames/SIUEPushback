using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIUE.ControllerGames.PoolSystem
{
    public interface IPool<T>
    {
       T Request();
       void Return(T item);
    }
}
