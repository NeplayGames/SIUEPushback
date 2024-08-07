using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIUE.ControllerGames.Configs
{
    public class ConfigManager 
    {
       public PlayerConfig playerConfig {get;}
       public ThrowableItemConfig throwableItemConfig {get;}

        //ToDo:
        //Create a generic config and add this value to generic config
        public float surfaceHeight {get;}
       public ConfigManager(DataBase.ConfigsDB configsDB)
        {
            this.playerConfig = configsDB.playerConfig;
            this.throwableItemConfig = configsDB.throwableItemConfig;
            surfaceHeight = configsDB.surfaceHeight;
       }
    }

}
