using System;
using System.Collections;
using System.Collections.Generic;
using SIUE.ControllerGames.Player;
using UnityEngine;

namespace SIUE.ControllerGames.Environment
{
    public class OutOfBoundTriggerInstances : MonoBehaviour
    {
        public event Action<PlayerController> OnPlayerOutOfBound;

        void OnCollisionEnter(Collision collider)
        {
            if (collider.gameObject.TryGetComponent(out PlayerController playerController))            
                OnPlayerOutOfBound?.Invoke(playerController);   
        }
    }

}
