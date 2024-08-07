using System;
using System.Collections.Generic;
using SIUE.ControllerGames.DataBase;
using SIUE.ControllerGames.Input;
using SIUE.ControllerGames.Player;
using SIUE.ControllerGames.PoolSystem;
using SIUE.ControllerGames.Throwables;
using SIUE.ControllerGames.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace SIUE.ControllerGames.System
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObjectsDB gameObjectsDB;
        [SerializeField] private PlayerInstantiatePosition playerInstantiatePosition;
        [SerializeField] private UIManager uIManager;
        [SerializeField] private OutOfBoundManager outOfBoundManager;
        [SerializeField] private Transform flyingObjectTransform;
        private List<PlayerController> playerControllers = new List<PlayerController>();
        private ThrowableManager throwableManager;
        private ConfigsDB configsDB;
        private PoolFabric poolFabric;
        private int playerInstantiate;
        // Start is called before the first frame update
        void Start()
        {
            poolFabric = new PoolFabric();
            configsDB = gameObjectsDB.configsDB;
            throwableManager = new ThrowableManager(gameObjectsDB.throwableDB, flyingObjectTransform, this.poolFabric);
            PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
            uIManager.StartGameAction += StartGame;
            outOfBoundManager.PlayerLostGame += OnPlayerLostGame;
        }

        private void OnPlayerLostGame(EPlayer player)
        {
            PlayerController lostPlayer = null;
            foreach (PlayerController playerController in playerControllers)
            {
                if (playerController.ePlayer == player)
                {
                    if (playerController.IsHit)
                    {
                        lostPlayer = playerController;
                    }
                }
            }
            if (lostPlayer == null) return;
            uIManager.GameInfoMessage(player.ToString());
            lostPlayer.isControllable = false;
            playerControllers.Remove(lostPlayer);
            if (playerControllers.Count == 1)
                uIManager.EndGame($"{playerControllers[0].ePlayer} won the game");
        }

        private void InstantiateThrowable() =>
            throwableManager.InstantiateThrowable();


        private void OnPlayerJoined(PlayerInput input) =>
            InstantiatePlayer(input);

        private void InstantiatePlayer(PlayerInput input)
        {
            if (playerInstantiate == configsDB.playerConfig.totalPlayer) return;
            PlayerController playerController = Instantiate(gameObjectsDB.players[playerInstantiate],
                playerInstantiatePosition.playerPosition[playerInstantiate].position,
                Quaternion.identity)
                .GetComponent<PlayerController>();
            playerControllers.Add(playerController);
            playerController.SetInputReader(new InputReader(input), configsDB.playerConfig, GetEPlayer(playerInstantiate));
            playerInstantiate++;
            uIManager.TotalPlayer(playerInstantiate);
        }

        private EPlayer GetEPlayer(int playerInstantiate)
        {
            return playerInstantiate switch
            {
                0 => EPlayer.EPlayer1,
                1 => EPlayer.EPlayer2,
                2 => EPlayer.EPlayer3,
                3 => EPlayer.EPlayer4,
                _ => 0
            };
        }

        public void StartGame()
        {
            foreach (var playerController in playerControllers)
            {
                playerController.isControllable = true;
            }
            PlayerInputManager.instance.DisableJoining();
            uIManager.StartGameAction -= StartGame;
            SceneManager.LoadSceneAsync("EnvironmentDesign", LoadSceneMode.Additive);
            InvokeRepeating(nameof(InstantiateThrowable), 1f, 1);
        }

        void OnDestroy()
        {
            PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
            outOfBoundManager.PlayerLostGame -= OnPlayerLostGame;
        }
    }
}
