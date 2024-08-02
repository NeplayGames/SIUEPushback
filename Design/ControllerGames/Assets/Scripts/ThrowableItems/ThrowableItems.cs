using SIUE.ControllerGames.Player;
using UnityEngine;

namespace SIUE.ControllerGames.Throwables
{
    public class ThrowableItems : MonoBehaviour
    {
        private PlayerController playerController;
        private bool shoot = false;

        private Vector3 Direction { get; set; }
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out playerController))
            {
                if (shoot)
                {
                    playerController.HitPlayer(Direction, 3);
                    Destroy(this.gameObject);
                    return;
                }
                playerController.ThrowItemEvent += OnThrow;
                transform.SetParent(playerController.transform);
                return;
            }
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
                transform.position += transform.forward * 40 * Time.deltaTime;
            }
        }
        private void OnThrow(Vector3 direction)
        {
            this.Direction = direction;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerController.ThrowItemEvent -= OnThrow;
            transform.rotation = targetRotation;
            transform.SetParent(null);
            shoot = true;
        }

    }
}
