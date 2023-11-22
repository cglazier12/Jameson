using UnityEngine;

namespace Controllers.Camera
{
    public class CameraController : MonoBehaviour
    {
        public Transform target; // Assign your player's transform
        public float smoothSpeed = 0.125f;
        public Vector3 offset;

        void LateUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position =
                new Vector3(smoothedPosition.x, transform.position.y,
                    transform.position.z);
        }

    }
}