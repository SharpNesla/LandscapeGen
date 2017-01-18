using UnityEngine;
using UnityEngine.EventSystems;

class CameraController : MonoBehaviour
{
    public Transform a;
    public float coeff, Speed;
    public Rigidbody r;
    void Start()
    {
        r = gameObject.GetComponent<Rigidbody>();
        a = gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        var xRot = Input.GetAxis("Mouse X") * coeff;
        var yRot = -Input.GetAxis("Mouse Y") * coeff;
        a.rotation = Quaternion.Euler(a.rotation.eulerAngles + new Vector3(yRot, xRot,0));
        var horiz = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        r.MovePosition(a.localPosition + a.rotation * new Vector3(horiz, 0, vertical) * Speed);

        Speed = Input.GetKey(KeyCode.LeftShift) ? 3f : 1;
    }
}