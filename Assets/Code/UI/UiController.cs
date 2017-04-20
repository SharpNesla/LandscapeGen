using Code.UnityBind;
using UnityEngine;

namespace Code.UI
{
    public class UiController : MonoBehaviour
    {
        private CameraController _cameraController;
        public GameObject DebugScreen;
        public GameObject UI;
        private void Start()
        {
            Application.targetFrameRate = 60;
            _cameraController = FindObjectOfType<CameraController>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _cameraController.ToggleCameraController();
                DebugScreen.SetActive(UI.activeSelf);
                UI.SetActive(!UI.activeSelf);
            }
            if (Input.GetKeyDown(KeyCode.F1) && !UI.activeSelf)
            {
                DebugScreen.SetActive(!DebugScreen.activeSelf);
            }
        }

    }
}