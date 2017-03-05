using UnityEngine;

namespace Code.UI
{
    public class UiController : MonoBehaviour
    {
        private CameraController _cameraController;

        private void Start()
        {
            _cameraController = FindObjectOfType<CameraController>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _cameraController.ToggleCameraController();
            }
        }

    }
}