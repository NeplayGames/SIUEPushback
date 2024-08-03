using System;
using System.Collections;
using System.Collections.Generic;
using SIUE.ControllerGames.DataBase;
using TMPro;
using UnityEngine;

namespace SIUE.ControllerGames.Throwables
{
    public class ThrowableManager
    {
        private ThrowableDB throwableDB;
        private Transform flyingObjectTransform;
        private int length;
        public ThrowableManager(ThrowableDB throwableDB, Transform flyingObjectTransform)
        {
            this.throwableDB = throwableDB;
            this.flyingObjectTransform = flyingObjectTransform;
            length = this.throwableDB.throwableConfigs.Count;
        }

        public void InstantiateThrowable()
        {
            int index = UnityEngine.Random.Range(0, length);
            ThrowableConfig throwableItemsConfig = this.throwableDB.throwableConfigs[index];
            GameObject spawnedThrowableItem = GameObject.Instantiate(throwableItemsConfig.throwable,
            flyingObjectTransform.position,
            Quaternion.identity);
            ThrowableItems throwableItems = spawnedThrowableItem.GetComponent<ThrowableItems>();
            throwableItems.SetDistanceAndTime(GetDistance(throwableItemsConfig.eThrowablesRarity), GetSpeed(throwableItemsConfig.eThrowablesRarity));
        }

        private float GetSpeed(EThrowablesRarity eThrowablesRarity)
        {
             return eThrowablesRarity switch 
            {
                EThrowablesRarity.Common => 50,
                EThrowablesRarity.Rare => 60,
                EThrowablesRarity.Epic => 70,
                EThrowablesRarity.Legendary => 80,
                _ => 0,
            };
        }

        private float GetDistance(EThrowablesRarity eThrowablesRarity)
        {
            return eThrowablesRarity switch 
            {
                EThrowablesRarity.Common => 4,
                EThrowablesRarity.Rare => 6,
                EThrowablesRarity.Epic => 8,
                EThrowablesRarity.Legendary => 10,
                _ => 0,
            };
        }
    }
}


