using SIUE.ControllerGames.DataBase;
using UnityEngine;

namespace SIUE.ControllerGames.Throwables
{
    public class ThrowableManager
    {
        private ThrowableDB throwableDB;
        private Transform flyingObjectTransform;
        public ThrowableManager(ThrowableDB throwableDB, Transform flyingObjectTransform)
        {
            this.throwableDB = throwableDB;
            this.flyingObjectTransform = flyingObjectTransform;
        }

        public void InstantiateThrowable()
        {
            GameObject spawnedThrowableItem = GameObject.Instantiate(this.throwableDB.throwable,
            flyingObjectTransform.position,
            Quaternion.identity);
            ThrowableItems throwableItems = spawnedThrowableItem.GetComponent<ThrowableItems>();
            EThrowablesRarity rarity = GetEThrowablesRarity();
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


