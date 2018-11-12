using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IndiePixel.Cameras {
    public class TopCamara : MonoBehaviour
    {
        public Transform m_target;
        [SerializeField]
        private float m_height = 10f;
        [SerializeField]
        private float m_Distance = 20f;
        [SerializeField]
        private float m_Angle = 45f;

        public float m_SmoothSpeed = 0.5f;
        private Vector3 RefVelocity;

        public float screen_limit;
        public float cam_offset;

        private CameraController cam;
        // Use this for initialization

        private void Awake()
        {

            cam = GetComponent<CameraController>();
        }

        void Start()
        {

            HandleCamera();

        }

        // Update is called once per frame
        void Update()
        {
            HandleCamera();
        }

        protected virtual void HandleCamera()
        {
            if(!m_target)
            {
                return;
            }

            //Build worldposition vector
            //Vector3 worldPosition = (Vector3.forward * -m_Distance) + (Vector3.up * m_height);
            Vector3 worldPosition = (Vector3.up * m_height);
            //Debug.DrawLine(m_target.position, worldPosition, Color.red);

            // Build rotated vector
            Vector3 rotatedVector = Quaternion.AngleAxis(m_Angle, Vector3.up) * worldPosition;
            //Debug.DrawLine(m_target.position, rotatedVector, Color.green);

            Vector3 flatTargetPosition = m_target.position;
            flatTargetPosition = cam.CalculatePointInCircleFromCenter(m_target.position, cam.GetMousePosInPlane(m_target.position),
                cam.CalculateOffset(m_target.position, cam.GetMousePosInPlane(m_target.position), screen_limit, cam_offset));
            
            Debug.DrawLine(m_target.position, flatTargetPosition, Color.red);
            flatTargetPosition.y = 0f;

            Vector3 finalPosition = flatTargetPosition + rotatedVector;
            
            transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref RefVelocity , m_SmoothSpeed);
            //transform.LookAt(flatTargetPosition);

        }
    }
}
