using SIUE.ControllerGames.Player;
using UnityEngine;

namespace SIUE.ControllerGames.Throwables
{
    public class ThrowableItems : MonoBehaviour
    {
        private PlayerController playerController;
        private bool shoot = false;

        public Vector3 Direction {get; private set;}
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out playerController))
            {
                playerController.ThrowItemEvent += OnThrow;
                transform.SetParent(playerController.transform);
            }
        }

        void Update(){
            if (shoot){
                transform.position += transform.forward * 40 * Time.deltaTime;
            }
        }
        private void OnThrow(Vector3 direction)
        {
            this.Direction = direction;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
            transform.SetParent(null);
            shoot = true;
        }

    }
}
