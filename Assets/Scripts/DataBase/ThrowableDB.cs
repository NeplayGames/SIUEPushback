using System.Collections;
using System.Collections.Generic;
using SIUE.ControllerGames.Throwables;
using UnityEngine;
using UnityEngine.Assertions;

namespace SIUE.ControllerGames.DataBase
{
  [CreateAssetMenu(fileName = "Throwable DB", menuName = "DB/ThrowableDB")]
  public class ThrowableDB : ScriptableObject
  {

    [field: SerializeField] public ThrowableItems throwable { get; private set; }
   
    void OnValidate()
    {
       Assert.IsNotNull(throwable);
    }
  }
}
