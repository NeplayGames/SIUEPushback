using System;
using SIUE.ControllerGames.Player;
using SIUE.ControllerGames.PoolSystem;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Assertions;
using static UnityEngine.ParticleSystem;

namespace SIUE.ControllerGames.Throwables
{
    public class ThrowableItems : MonoBehaviour
    {
        [SerializeField] private Rigidbody itemRigidBody;
        [SerializeField] private TrailRenderer trailRenderer;
        [SerializeField] private ParticleSystem particleSystem1;
        [SerializeField] private ParticleSystem particleSystem2;
        public bool shoot { get; private set; } = false;
        private bool collectItem = false;
        public float distance { get; private set; }
        public Vector3 Direction { get; private set; }
        private float speed;
        public static event Action OnReturn;
        private IPool<ThrowableItems> throwableItemsPool { set; get; }
        void OnTriggerEnter(Collider collision)
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
        public void OnThrow(Vector3 direction, Vector3 position)
        {
            collectItem = true;
            trailRenderer.enabled = true;
            this.transform.position = position;
            this.Direction = direction;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
            transform.SetParent(null);
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
            IsKinematic(true);
        }

        private void IsKinematic(bool isKinematic) =>
            itemRigidBody.isKinematic = isKinematic;

        public void ResetItem(Vector3 position, IPool<ThrowableItems> throwableItemPool, Color rarityColor)
        {
            trailRenderer.colorGradient = CreateGradient(rarityColor);
            MainModule mainModule1 = particleSystem1.main;
            mainModule1.startColor = rarityColor;
            MainModule mainModule2 = particleSystem2.main;
            mainModule2.startColor = rarityColor;
            this.transform.position = position;
            this.throwableItemsPool = throwableItemPool;
            IsKinematic(false);
            trailRenderer.enabled = false;
            collectItem = false;
            shoot = false;
        }

        Gradient CreateGradient(Color color)
        {
            var gradient = new Gradient();

            // Blend color from red at 0% to blue at 100%
            var colors = new GradientColorKey[2];
            colors[0] = new GradientColorKey(color, 0.0f);
            colors[1] = new GradientColorKey(color, 1.0f);

            // Blend alpha from opaque at 0% to transparent at 100%
            var alphas = new GradientAlphaKey[2];
            alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
            alphas[1] = new GradientAlphaKey(0.0f, 1.0f);

            gradient.SetKeys(colors, alphas);
            return gradient;
        }
        void OnValidate()
        {
            Assert.IsNotNull(itemRigidBody);
            Assert.IsNotNull(trailRenderer);
        }
    }
}
