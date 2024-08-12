using System;
using SIUE.ControllerGames.Player;
using SIUE.ControllerGames.PoolSystem;
using UnityEngine;
using UnityEngine.Assertions;
using static UnityEngine.ParticleSystem;

namespace SIUE.ControllerGames.Throwables
{
    public class ThrowableItems : MonoBehaviour
    {
        [SerializeField] private TrailRenderer trailRenderer;
        [SerializeField] private ParticleSystem particleSystem1;
        [SerializeField] private ParticleSystem particleSystem2;
        public bool shoot { get; private set; } = false;
        public EPlayer shootingPlayer { get; private set; }
        private bool collectItem = false;
        public float distance { get; private set; }
        public Vector3 Direction { get; private set; }
        private float speed;
        public static event Action OnReturn;
        private IPool<ThrowableItems> throwableItemsPool { set; get; }
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
                Remove();
        }

        public void Remove()
        {
            throwableItemsPool.Return(this);
            OnReturn?.Invoke();
        }


        void Update()
        {
            if (shoot)
                transform.position += transform.forward * speed * Time.deltaTime;
        }
        public void OnThrow(Vector3 direction, Vector3 position, EPlayer ePlayer)
        {
            collectItem = true;
            this.shootingPlayer = ePlayer;
            this.transform.position = position;
            this.Direction = direction;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
            transform.SetParent(null);
            trailRenderer.enabled = true;
            trailRenderer.Clear();
            shoot = true;
        }

        public void SetDistanceAndTime(float distance, float speed)
        {
            this.distance = distance;
            this.speed = speed;
        }

        public void GotPicked(Transform parent)
        {
            if (collectItem) return;
            collectItem = true;
            transform.SetParent(parent);
            transform.localPosition = Vector3.up;
        }

        public void ResetItem(Vector3 position, IPool<ThrowableItems> throwableItemPool, Color rarityColor)
        {
            Gradient gradient = CreateGradient(rarityColor);
            trailRenderer.colorGradient = gradient;
            ColorOverLifetimeModule colorOverLifetimeModule = particleSystem1.colorOverLifetime;
            colorOverLifetimeModule.color = new MinMaxGradient(gradient);
            MainModule mainModule = particleSystem1.main;
            mainModule.startColor = rarityColor;
            MainModule mainModule2 = particleSystem2.main;
            mainModule2.startColor = rarityColor;
            // mainModule1.colo
            ColorOverLifetimeModule colorOverLifetimeModule2 = particleSystem2.colorOverLifetime;
            colorOverLifetimeModule2.color = new MinMaxGradient(gradient);
            this.transform.position = position;
            this.throwableItemsPool = throwableItemPool;
            trailRenderer.enabled = false;
            shootingPlayer = EPlayer.none;
            collectItem = false;
            shoot = false;
        }

        Gradient CreateGradient(Color color)
        {
            var gradient = new Gradient();

            // Blend color from red at 0% to blue at 100%
            var colors = new GradientColorKey[2];
            colors[0] = new GradientColorKey(color, 1.0f);
            colors[1] = new GradientColorKey(color, 1.0f);

            // Blend alpha from opaque at 0% to transparent at 100%
            var alphas = new GradientAlphaKey[2];
            alphas[0] = new GradientAlphaKey(1.0f, 1.0f);
            alphas[1] = new GradientAlphaKey(1.0f, 1.0f);

            gradient.SetKeys(colors, alphas);
            return gradient;
        }
        void OnValidate()
        {
            Assert.IsNotNull(trailRenderer);
        }
    }
}
