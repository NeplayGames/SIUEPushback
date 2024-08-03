using System;
using SIUE.ControllerGames.Player;
using UnityEngine;

namespace SIUE.ControllerGames.Throwables
{
    public class ThrowableItems : MonoBehaviour
    {
        [SerializeField] private Rigidbody itemRigidBody;
        public bool shoot { get; private set; } = false;
        private bool collectItem = false;
        public float distance{ get; private set; }
        public Vector3 Direction { get; private set; }
        private float speed;

        void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                //Temp
                Destroy(this.gameObject);
            }
        }

        void Update()
        {
            if (shoot)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        }
        public void OnThrow(Vector3 direction, Vector3 position)
        {
            collectItem = true;
            this.transform.position = position;
            this.Direction = direction;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
            transform.SetParent(null);
            shoot = true;
        }

        internal void SetDistanceAndTime(float distance, float speed)
        {
            this.distance = distance;
            this.speed = speed;
        }

        internal void GotPicked(Transform parent)
        {
            if(collectItem) return;
            collectItem = true;
            transform.SetParent(parent);
            transform.localPosition = Vector3.up;
            itemRigidBody.isKinematic = true;
        }
    }
}
