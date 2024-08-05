using SIUE.ControllerGames.DataBase;
using SIUE.ControllerGames.Input;
using SIUE.ControllerGames.Player;
using SIUE.ControllerGames.PoolSystem;
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
        private ConfigsDB configsDB;
        private PoolFabric poolFabric;
        private int playerInstantiate;
        // Start is called before the first frame update
        void Start()
        {
            poolFabric = new PoolFabric();
            configsDB = gameObjectsDB.configsDB;
            throwableManager = new ThrowableManager(gameObjectsDB.throwableDB, flyingObjectTransform,this.poolFabric);
            PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
            InvokeRepeating(nameof(InstantiateThrowable), 1f, 5);
        }

        private void InstantiateThrowable() =>
            throwableManager.InstantiateThrowable();


        private void OnPlayerJoined(PlayerInput input) =>
            InstantiatePlayer(input);

        private void InstantiatePlayer(PlayerInput input)
        {
            if(playerInstantiate == configsDB.playerConfig.totalPlayer) return;
            Instantiate(gameObjectsDB.players[playerInstantiate],
                playerInstantiatePosition.playerPosition[playerInstantiate].position,
                Quaternion.identity)
                .GetComponent<PlayerController>().
                SetInputReader(new InputReader(input), configsDB.playerConfig);
            playerInstantiate++;
        }
    }
}
