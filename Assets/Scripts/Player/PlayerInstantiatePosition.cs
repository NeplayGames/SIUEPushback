using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SIUE.ControllerGames.Player
{
    public class PlayerInstantiatePosition : MonoBehaviour
    {
        [field:SerializeField] public List<Transform> playerPosition  { get; private set; }

        void OnValidate(){
            Assert.IsTrue(playerPosition.Count == 4, "The number of player temporarily for now should be four");
        }
    }

}

