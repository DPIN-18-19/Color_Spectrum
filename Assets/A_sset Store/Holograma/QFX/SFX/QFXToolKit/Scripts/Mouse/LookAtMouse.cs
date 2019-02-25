using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class LookAtMouse : MonoBehaviour
    {
        public float RotationSpeed;

        private void FixedUpdate()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit))
                return;

            var targetRotation = Quaternion.LookRotation(hit.point - transform.position);
            transform.rotation =
                Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }
    }
}