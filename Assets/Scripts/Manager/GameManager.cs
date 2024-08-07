using System;
using System.Collections.Generic;
using SIUE.ControllerGames.Configs;
using SIUE.ControllerGames.DataBase;
using SIUE.ControllerGames.Input;
using SIUE.ControllerGames.Player;
using SIUE.ControllerGames.PoolSystem;
using SIUE.ControllerGames.Throwables;
using SIUE.ControllerGames.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SIUE.ControllerGames.System
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObjectsDB gameObjectsDB;
        [SerializeField] private PlayerInstantiatePosition playerInstantiatePosition;
        [SerializeField] private UIManager uIManager;
        [SerializeField] private OutOfBoundManager outOfBoundManager;
        private List<PlayerController> playerControllers = new List<PlayerController>();
        private ThrowableManager throwableManager;
        private PoolFabric poolFabric;
        private ConfigManager configManager;
        private int playerInstantiate;

        private const string player1Name = "Player 1";
        private const string player2Name = "Player 2";
        private const string player3Name = "Player 3";
        private const string player4Name = "Player 4";
        // Start is called before the first frame update
        void Start()
        {
            poolFabric = new PoolFabric();
            configManager = new ConfigManager(gameObjectsDB.configsDB);
            throwableManager = new ThrowableManager(configManager, gameObjectsDB.throwableDB.throwable,
            this.poolFabric);
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
            uIManager.GameInfoMessage($"{GetPlayerName(player)} is out of arena");
            lostPlayer.isControllable = false;
            playerControllers.Remove(lostPlayer);
            if (playerControllers.Count == 1)
            {
                uIManager.EndGame($"{GetPlayerName( playerControllers[0].ePlayer)} won the game");
                
            }
        }

        private object GetPlayerName(EPlayer ePlayer)
        {
            return ePlayer switch{
                EPlayer.EPlayer1 => player1Name,
                EPlayer.EPlayer2 => player2Name,
                EPlayer.EPlayer3 => player3Name,
                EPlayer.EPlayer4 => player4Name,
                _ => ""
            };
        }

        private void InstantiateThrowable() =>
            throwableManager.InstantiateThrowable();

        private void OnPlayerJoined(PlayerInput input) =>
            InstantiatePlayer(input);

        private void InstantiatePlayer(PlayerInput input)
        {
            if (playerInstantiate == configManager.playerConfig.totalPlayer) return;
            Transform playerPositionTransfrom = playerInstantiatePosition.playerPosition[playerInstantiate];
            PlayerController playerController = Instantiate(gameObjectsDB.players[playerInstantiate],
                playerPositionTransfrom.position,
                playerPositionTransfrom.rotation)
                .GetComponent<PlayerController>();
            playerControllers.Add(playerController);
            playerController.SetInputReader(new InputReader(input), configManager.playerConfig, GetEPlayer(playerInstantiate));
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
           // SceneManager.LoadSceneAsync("EnvironmentDesign", LoadSceneMode.Additive);
            InvokeRepeating(nameof(InstantiateThrowable), 1f, configManager.throwableItemConfig.throwableRate);

        }

        void OnDestroy()
        {
            outOfBoundManager.PlayerLostGame -= OnPlayerLostGame;
        }
    }
}
