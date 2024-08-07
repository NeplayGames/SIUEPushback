using System;
using System.Collections;
using System.Collections.Generic;
using SIUE.ControllerGames.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace SIUE.ControllerGames.Environment
{
    public class OutOfBoundTriggerInstances : MonoBehaviour
    {
        public event Action<EPlayer> OnPlayerOutOfBoundEvent;

        void OnCollisionEnter(Collision collider)
        {
            if (collider.gameObject.TryGetComponent(out PlayerController playerController))
            {
                OnPlayerOutOfBoundEvent?.Invoke(playerController.ePlayer);
            }
        }
    }

}
