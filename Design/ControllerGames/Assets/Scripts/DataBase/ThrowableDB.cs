using System.Collections;
using System.Collections.Generic;
using SIUE.ControllerGames.Throwables;
using UnityEngine;

namespace SIUE.ControllerGames.DataBase
{
    [CreateAssetMenu(fileName = "Throwable DB", menuName = "DB/ThrowableDB")]
    public class ThrowableDB : ScriptableObject
    {
        
      [field:SerializeField] public List<ThrowableConfig> throwableConfigs {get;private set;}
    }
}
