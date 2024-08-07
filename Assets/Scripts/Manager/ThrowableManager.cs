using System;
using SIUE.ControllerGames.Configs;
using SIUE.ControllerGames.PoolSystem;
using UnityEngine;

namespace SIUE.ControllerGames.Throwables
{
    public class ThrowableManager : IDisposable
    {
        private ThrowableItemConfig throwableItemConfig;
        private IPool<ThrowableItems> throwableItemsPool {get;}
        private int totalCount = 0;
        private float surfaceHeight {get;}
        public ThrowableManager(ConfigManager configManager, ThrowableItems throwableItems,
         PoolFabric poolFabric)
        {
            this.throwableItemConfig = configManager.throwableItemConfig;
            ThrowableItems.OnReturn += () => totalCount--;
            this.throwableItemsPool = poolFabric.CreatePool(throwableItems);
            surfaceHeight = configManager.surfaceHeight;
        }

        public void InstantiateThrowable()
        {
            if(totalCount >= throwableItemConfig.totalThrowableItems) return;
            totalCount++;
            ThrowableItems throwableItems = throwableItemsPool.Request();
            EThrowablesRarity rarity = GetEThrowablesRarity();
            Vector3 throwableItemPosition = GetRandomPositionInsideArena();
            throwableItems.ResetItem(throwableItemPosition,throwableItemsPool, GetColor(rarity));
            throwableItems.SetDistanceAndTime(GetDistance(rarity), GetSpeed(rarity));
        }

        private Vector3 GetRandomPositionInsideArena()
        {
            float ArenaSemiLength = throwableItemConfig.ArenaLength/2;
            float x = UnityEngine.Random.Range(-ArenaSemiLength, ArenaSemiLength);
            float z = UnityEngine.Random.Range(-ArenaSemiLength, ArenaSemiLength);
            return new Vector3(x, surfaceHeight, z);
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
                EThrowablesRarity.Common => throwableItemConfig.commonItemSpeed,
                EThrowablesRarity.Rare => throwableItemConfig.rareItemSpeed,
                EThrowablesRarity.Epic => throwableItemConfig.epicItemSpeed,
                EThrowablesRarity.Legendary => throwableItemConfig.legendaryItemSpeed,
                _ => 0,
            };
        }

         private Color GetColor(EThrowablesRarity eThrowablesRarity)
        {
            return eThrowablesRarity switch
            {
                EThrowablesRarity.Common => throwableItemConfig.commonColor,
                EThrowablesRarity.Rare => throwableItemConfig.rareColor,
                EThrowablesRarity.Epic => throwableItemConfig.epicColor,
                EThrowablesRarity.Legendary => throwableItemConfig.legendaryColor,
                _ => Color.white,
            };
        }

        private float GetDistance(EThrowablesRarity eThrowablesRarity)
        {
            return eThrowablesRarity switch
            {
                EThrowablesRarity.Common => throwableItemConfig.commonDistancePushed,
                EThrowablesRarity.Rare => throwableItemConfig.rareDistancePushed,
                EThrowablesRarity.Epic => throwableItemConfig.epicDistancePushed,
                EThrowablesRarity.Legendary => throwableItemConfig.legendaryDistancePushed,
                _ => 0,
            };
        }

        public void Dispose()
        {
            ThrowableItems.OnReturn -= () => totalCount--;
        }
    }
}


