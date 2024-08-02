using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIUE.ControllerGames.Throwables
{
    public class ThrowableManager
    {
        private GameObject throwableGameObject;
        public ThrowableManager(GameObject throwable)
        {
            this.throwableGameObject = throwable;
        }

        internal void InstantiateThrowable()
        {
            GameObject.Instantiate(throwableGameObject);
        }
    }
}


