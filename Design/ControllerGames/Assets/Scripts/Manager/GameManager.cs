using System;
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
        [SerializeField] private PlayerInputManager playerInputManager;

        private ThrowableManager throwableManager;
        // Start is called before the first frame update
        void Start()
        {
            throwableManager = new ThrowableManager(gameObjectsDB.throwable);
            playerInputManager.onPlayerJoined += OnPlayerJoined;
            InvokeRepeating(nameof(InstantiateThrowable), 3f, 1);
            //Temp
        }

        private void InstantiateThrowable()
        {
            throwableManager.InstantiateThrowable();
        }

        private void OnPlayerJoined(PlayerInput input)
        {
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

        // Update is called once per frame
        void Update()
        {

        }
    }
}
