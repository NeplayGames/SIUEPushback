using System;
using System.Collections;
using System.Collections.Generic;
using SIUE.ControllerGames.Environment;
using SIUE.ControllerGames.Player;
using UnityEngine;

namespace SIUE.ControllerGames
{
    public class OutOfBoundManager : MonoBehaviour
    {
        [SerializeField] private List<OutOfBoundTriggerInstances> ofBoundTriggerInstances;

        void Start() =>
         RegisterMethods();

        public event Action<EPlayer> PlayerLostGame;
        private void RegisterMethods()
        {
            foreach (OutOfBoundTriggerInstances instance in ofBoundTriggerInstances)
            {
                instance.OnPlayerOutOfBoundEvent += OnPlayerOutOfBound;
            }
        }

        private void OnPlayerOutOfBound(EPlayer ePlayer)
        {
            PlayerLostGame?.Invoke(ePlayer);
        }

        void OnDestroy()
        {
            foreach (OutOfBoundTriggerInstances instance in ofBoundTriggerInstances)
            {
                instance.OnPlayerOutOfBoundEvent -= OnPlayerOutOfBound;
            }
        }
    }

}
