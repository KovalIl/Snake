using UnityEngine;

public class CameraScale : MonoBehaviour
{
    Camera camera;
    [SerializeField] private Renderer _backGround;

    void Start()
    {
        camera = GetComponent<Camera>();     
        SetupCamera();
    }

    private void SetupCamera()
    {
        camera.transform.position = new Vector3(_backGround.bounds.center.x - 0.1f, camera.transform.position.y, _backGround.bounds.center.z - 0.1f);
        var aspectRatio = Screen.width / (float)Screen.height;
        var verticalSize = _backGround.bounds.size.x / 2f - 0.2f;        
        var horizontalSize = (_backGround.bounds.size.z / 2f - 0.1f) / aspectRatio;
        camera.orthographicSize = verticalSize > horizontalSize ? verticalSize : horizontalSize;
    }
}
