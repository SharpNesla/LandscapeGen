using UnityEngine;

class TransformBinder : MonoBehaviour
{
    public Transform ToBind;
    private Transform _self;
    [Header("Bind position on axes:")]
    public bool XPos;
    public bool YPos;
    public bool ZPos;
    [Header("Bind rotation on axes:")]
    public bool XRot;
    public bool YRot;
    public bool ZRot;

    void Start()
    {
        _self = gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        _self.position = new Vector3(
            XPos ? ToBind.position.x : _self.position.x,
            YPos ? ToBind.position.y : _self.position.y,
            ZPos ? ToBind.position.z : _self.position.z
            );
        _self.rotation = Quaternion.Euler(new Vector3(
            XRot ? ToBind.rotation.eulerAngles.x : _self.rotation.eulerAngles.x,
            YRot ? ToBind.rotation.eulerAngles.y : _self.rotation.eulerAngles.y,
            ZRot ? ToBind.rotation.eulerAngles.z : _self.rotation.eulerAngles.z
            ));
    }
}