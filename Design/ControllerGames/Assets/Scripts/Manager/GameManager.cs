using System;
using SIUE.ControllerGames.DataBase;
using SIUE.ControllerGames.Input;
using SIUE.ControllerGames.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SIUE.ControllerGames.System{

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObjectsDB gameObjectsDB;
    [SerializeField] private PlayerInstantiatePosition playerInstantiatePosition;
    [SerializeField] private PlayerInputManager playerInputManager;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerInputManager.onPlayerJoined += OnPlayerJoined;
        //Temp
    }

        private void OnPlayerJoined(PlayerInput input)
        {
            InstantiatePlayer(input);
        }

        private void InstantiatePlayer(PlayerInput input)
        {
            playerController = Instantiate(gameObjectsDB.player,playerInstantiatePosition.playerPosition[0].position, Quaternion.identity ).GetComponent<PlayerController>();
            playerController.SetInputReader(new InputReader(input));   
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
