using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SIUE.ControllerGames.DataBase
{
    [CreateAssetMenu(fileName = "Game Object DataBase", menuName = "DB/GameObjectDB")]
    public class GameObjectsDB : ScriptableObject
    {
        [field:SerializeField] public GameObject player { get; private set; }
        void OnValidate(){
            Assert.IsNotNull(player, $"The player is null in {nameof(GameObjectsDB)}");
        }
    }
}
