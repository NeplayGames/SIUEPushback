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
        private PoolFabric poolFabric;
        private int playerInstantiate;
        // Start is called before the first frame update
        void Start()
        {
            poolFabric = new PoolFabric();
            throwableManager = new ThrowableManager(gameObjectsDB.throwableDB, flyingObjectTransform,this.poolFabric);
            PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
            InvokeRepeating(nameof(InstantiateThrowable), 1f, 1);
            //Temp
        }

        private void InstantiateThrowable() =>
            throwableManager.InstantiateThrowable();


        private void OnPlayerJoined(PlayerInput input) =>
            InstantiatePlayer(input);

        private void InstantiatePlayer(PlayerInput input)
        {
            if(playerInstantiate == 4) return;
            Instantiate(gameObjectsDB.players[playerInstantiate],
                playerInstantiatePosition.playerPosition[playerInstantiate].position,
                Quaternion.identity)
                .GetComponent<PlayerController>().
                SetInputReader(new InputReader(input));
            playerInstantiate++;
        }
    }
}
