using System.Collections;
using System.Collections.Generic;
using SIUE.ControllerGames.Player;
using UnityEngine;
using UnityEngine.Assertions;

namespace SIUE.ControllerGames.DataBase
{
    [CreateAssetMenu(fileName = "Game Object DataBase", menuName = "DB/GameObjectDB")]
    public class GameObjectsDB : ScriptableObject
    {
        [field:SerializeField] public GameObject player { get; private set; }
        [field:SerializeField] public GameObject throwable { get; private set; }
        void OnValidate(){
            Assert.IsNotNull(player, $"The player is null in {nameof(GameObjectsDB)}");
            Assert.IsNotNull(throwable, $"The {nameof(throwable)} is null in {nameof(GameObjectsDB)}");
            Assert.IsTrue(player.GetComponent<PlayerController>(), "The player needs to have the player Controller");
        }
    }
}
