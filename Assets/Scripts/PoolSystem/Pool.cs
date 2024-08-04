using System;
using System.Collections.Generic;
using UnityEngine;

namespace SIUE.ControllerGames.PoolSystem
{
    public class Pool<T> : IPool<T> where T : Component
    {
        public Stack<T> poolStack = new Stack<T>();
        private T item;

        public Pool(T item)
        {
            this.item = item;
        }

        public T Request()
        {
            T item = poolStack.Count == 0 ? GameObject.Instantiate(this.item) : poolStack.Pop();
            item.gameObject.SetActive(true);
            return item;

        }

        public void Return(T item)
        {
            item.gameObject.SetActive(false);
            poolStack.Push(item);
        }
    }

}
