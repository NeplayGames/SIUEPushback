using System;
using SIUE.ControllerGames.DataBase;
using SIUE.ControllerGames.PoolSystem;
using UnityEngine;

namespace SIUE.ControllerGames.Throwables
{
    public class ThrowableManager : IDisposable
    {
        private ThrowableDB throwableDB;
        private Transform flyingObjectTransform;
        private IPool<ThrowableItems> throwableItemsPool {get;}
        private int totalCount = 0;
        public ThrowableManager(ThrowableDB throwableDB, Transform flyingObjectTransform, PoolFabric poolFabric)
        {
            this.throwableDB = throwableDB;
            this.flyingObjectTransform = flyingObjectTransform;
            ThrowableItems.OnReturn += () => totalCount--;
            this.throwableItemsPool = poolFabric.CreatePool(throwableDB.throwable);
        }

      
        

        public void InstantiateThrowable()
        {
            if(totalCount >= 5) return;
            totalCount++;
            ThrowableItems throwableItems = throwableItemsPool.Request();
            EThrowablesRarity rarity = GetEThrowablesRarity();
            throwableItems.ResetItem(flyingObjectTransform.position,throwableItemsPool, GetColor(rarity));
            throwableItems.SetDistanceAndTime(GetDistance(rarity), GetSpeed(rarity));
        }

        public EThrowablesRarity GetEThrowablesRarity()
        {
            float rarityFloatRandom = UnityEngine.Random.Range(0f, 1f);
            if (rarityFloatRandom < 0.5f)
                return EThrowablesRarity.Common;
            if (rarityFloatRandom < .75f)
                return EThrowablesRarity.Rare;
            if (rarityFloatRandom < .90f)
                return EThrowablesRarity.Legendary;
            return EThrowablesRarity.Epic;
        }

        private float GetSpeed(EThrowablesRarity eThrowablesRarity)
        {
            return eThrowablesRarity switch
            {
                EThrowablesRarity.Common => throwableDB.commonItemSpeed,
                EThrowablesRarity.Rare => throwableDB.rareItemSpeed,
                EThrowablesRarity.Epic => throwableDB.epicItemSpeed,
                EThrowablesRarity.Legendary => throwableDB.legendaryItemSpeed,
                _ => 0,
            };
        }

         private Color GetColor(EThrowablesRarity eThrowablesRarity)
        {
            return eThrowablesRarity switch
            {
                EThrowablesRarity.Common => throwableDB.commonColor,
                EThrowablesRarity.Rare => throwableDB.rareColor,
                EThrowablesRarity.Epic => throwableDB.epicColor,
                EThrowablesRarity.Legendary => throwableDB.legendaryColor,
                _ => Color.white,
            };
        }

        private float GetDistance(EThrowablesRarity eThrowablesRarity)
        {
            return eThrowablesRarity switch
            {
                EThrowablesRarity.Common => throwableDB.commonDistancePushed,
                EThrowablesRarity.Rare => throwableDB.rareDistancePushed,
                EThrowablesRarity.Epic => throwableDB.epicDistancePushed,
                EThrowablesRarity.Legendary => throwableDB.legendaryDistancePushed,
                _ => 0,
            };
        }

        public void Dispose()
        {
            ThrowableItems.OnReturn -= () => totalCount--;
        }
    }
}


