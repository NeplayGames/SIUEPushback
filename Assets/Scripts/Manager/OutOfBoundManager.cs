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

        private void RegisterMethods()
        {
            foreach (OutOfBoundTriggerInstances instance in ofBoundTriggerInstances)
            {
                instance.OnPlayerOutOfBound += OnPlayerOutOfBound;
            }
        }

        private void OnPlayerOutOfBound(PlayerController controller)
        {
            if(controller.IsHit)
                controller.isControllable = false;
            print($"{nameof(controller)} lost the match");
        }

        void OnDestroy()
        {
            foreach (OutOfBoundTriggerInstances instance in ofBoundTriggerInstances)
            {
                instance.OnPlayerOutOfBound -= OnPlayerOutOfBound;
            }
        }
    }

}
