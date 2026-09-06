using UnityEngine;

namespace ProjectLOOP
{
    public sealed class TopDownCameraFollow : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] Vector3 offset = new Vector3(0f, 14f, -10f);
        [SerializeField] float pitchDegrees = 50f;
        [SerializeField] float followSpeed = 10f;

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            var desired = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desired, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(pitchDegrees, 0f, 0f);
        }
    }
}
