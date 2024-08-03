using System;
using System.Collections.Generic;
using SIUE.ControllerGames.DataBase;
using SIUE.ControllerGames.Input;
using SIUE.ControllerGames.Player;
using SIUE.ControllerGames.Throwables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SIUE.ControllerGames.System
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObjectsDB gameObjectsDB;
        [SerializeField] private PlayerInstantiatePosition playerInstantiatePosition;
        [SerializeField] private Transform flyingObjectTransform;
        private ThrowableManager throwableManager;
        // Start is called before the first frame update
        void Start()
        {
            throwableManager = new ThrowableManager(gameObjectsDB.throwableDB, flyingObjectTransform);
            PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
            InvokeRepeating(nameof(InstantiateThrowable), 10f, 1);
            //Temp
        }

        private void InstantiateThrowable()
        {
            throwableManager.InstantiateThrowable();
        }

        private void OnPlayerJoined(PlayerInput input)
        {
            print("Player is spawned");
            InstantiatePlayer(input);
        }

        private void InstantiatePlayer(PlayerInput input)
        {
            Instantiate(gameObjectsDB.player,
                playerInstantiatePosition.playerPosition[0].position,
                Quaternion.identity)
                .GetComponent<PlayerController>().
                SetInputReader(new InputReader(input));
        }

        void OnDestroy()
        {
            PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
        }
    }
}
