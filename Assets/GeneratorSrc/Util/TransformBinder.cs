using UnityEngine;

namespace Assets.GeneratorSrc.Util
{
    public class TransformBinder : MonoBehaviour
    {
        public Transform ToBind;
        private Transform _self;

        public bool XPos;
        public bool YPos;
        public bool ZPos;
        public bool XRot;
        public bool YRot;
        public bool ZRot;



        public void Start()
        {
            _self = gameObject.GetComponent<Transform>();
        }

        public void Update()
        {
            _self.position = ToBind.position;
            _self.rotation = Quaternion.Euler(
                XRot ? -ToBind.rotation.eulerAngles.x : _self.rotation.eulerAngles.x,
                YRot ? ToBind.rotation.eulerAngles.y : _self.rotation.eulerAngles.y,
                ZRot ? ToBind.rotation.eulerAngles.z : _self.rotation.eulerAngles.z
                );
        }
    }
}