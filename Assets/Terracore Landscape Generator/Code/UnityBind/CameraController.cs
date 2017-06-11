using UnityEngine;

namespace Code.UnityBind
{
    public class CameraController : MonoBehaviour
    {
        private Transform _cachedTransform;
        public float Coeff, Speed;
        private Rigidbody _physx;

        void Start()
        {
            _physx = gameObject.GetComponent<Rigidbody>();
            _cachedTransform = gameObject.GetComponent<Transform>();
        }

        void FixedUpdate()
        {
            var xRot = Input.GetAxis("Mouse X") * Coeff;
            var yRot = -Input.GetAxis("Mouse Y") * Coeff;
            _cachedTransform.rotation = Quaternion.Euler(_cachedTransform.rotation.eulerAngles + new Vector3(yRot, xRot,0));
            var horiz = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            _physx.MovePosition(_cachedTransform.localPosition + _cachedTransform.rotation * new Vector3(horiz, 0, vertical) * Speed);

            Speed = Input.GetKey(KeyCode.LeftShift) ? 3f : 1;
        }
    }
}