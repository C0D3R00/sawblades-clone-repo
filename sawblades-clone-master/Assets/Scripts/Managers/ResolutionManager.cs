using UnityEngine;

public class ResolutionManager : Singleton<ResolutionManager>
{
    public float CameraWidth { get; private set; }
    public float CameraHeight { get; private set; }

    protected ResolutionManager() { }

    [SerializeField]
    private float
        DesiredWidth = 0f;

    private void Start()
    {
        CameraWidth = DesiredWidth / 2f;
        CameraHeight = CameraWidth / Camera.main.aspect;

        Camera.main.orthographicSize = CameraHeight;

        Debug.Log("Camera Height: " + CameraHeight + " Camera Width: " + CameraWidth);
    }
}