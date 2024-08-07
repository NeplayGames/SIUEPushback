using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIUE.ControllerGames.Throwables
{
    public class GineeController : MonoBehaviour
    {
        [SerializeField] private float fieldWidth = 10;
        private Vector3 currentPosition;
        private Vector3 newPosition;
        private float time;
        private float tempTime = 0;
        private float height;
        // Start is called before the first frame update
        void Start()
        {
            height = transform.position.y;
            SetTimeAndPosition();
        }

        private void SetTimeAndPosition()
        {
            currentPosition = transform.position;
            newPosition = GetRandomPositionInsideArena();
            float distance = Vector3.Distance(currentPosition, newPosition);
            time = distance / 8;
            tempTime = 0;
        }

        private Vector3 GetRandomPositionInsideArena()
        {
            return new Vector3(Random.Range(-fieldWidth, fieldWidth),
                height,
                Random.Range(-fieldWidth, fieldWidth));
        }

        // Update is called once per frame
        void Update()
        {
            if (time > tempTime)
            {
                tempTime += Time.deltaTime;
                transform.position = Vector3.Lerp(currentPosition, newPosition, tempTime / time);
            }
            else
            {
                SetTimeAndPosition();
            }
        }
    }

}
