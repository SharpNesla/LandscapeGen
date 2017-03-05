namespace Code.UI
{
    public static class Extensions
    {
        public static void ToggleCameraController(this CameraController controll)
        {
            controll.enabled = !controll.enabled;
        }
    }
}