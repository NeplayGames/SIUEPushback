using System;
using SIUE.ControllerGames.DataBase;
using SIUE.ControllerGames.Input;
using SIUE.ControllerGames.Player;
using UnityEngine;

namespace SIUE.ControllerGames.System{

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObjectsDB gameObjectsDB;
    [SerializeField] private PlayerInstantiatePosition playerInstantiatePosition;
    private InputReader inputReader;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        inputReader = new InputReader();
        //Temp
        InstantiatePlayer();
    }

        private void InstantiatePlayer()
        {
            playerController = Instantiate(gameObjectsDB.player,playerInstantiatePosition.playerPosition[0].position, Quaternion.identity ).GetComponent<PlayerController>();
            playerController.SetInputReader(inputReader);   
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
