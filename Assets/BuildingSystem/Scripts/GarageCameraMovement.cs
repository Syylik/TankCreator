using UnityEngine;

public class GarageCameraMovement : MonoBehaviour
{
    [SerializeField] [Range(0, 500f)] private float moveSpeed = 0.3f;
    [SerializeField] [Range(0, 250f)] private float scrollSpeed = 50f;
    private Vector2 moveTo;
    private PlayerInput input;
    public static GarageCameraMovement instance { get; private set; }
    private void Awake()
    {
        instance = this;
        input = new PlayerInput();
    }
    private void OnEnable() => input.Enable();
    private void OnDestroy() => input.Disable();
    private void LateUpdate()  //лютейший говнокод, но я с камерой мало работал, сорри :(
    {
        transform.position += (Vector3)moveTo * moveSpeed * Time.deltaTime;
        if(Camera.main.orthographicSize<1.5f) Camera.main.orthographicSize = 1.5f;
        if(Camera.main.orthographicSize>20f) Camera.main.orthographicSize = 20f;
        if(Camera.main.orthographicSize>1.5f && input.Player.GarageZoom.ReadValue<Vector2>().y>0) Camera.main.orthographicSize += (-input.Player.GarageZoom.ReadValue<Vector2>().y / 100) * scrollSpeed * Time.deltaTime;
        if(Camera.main.orthographicSize<20f && input.Player.GarageZoom.ReadValue<Vector2>().y<0) Camera.main.orthographicSize += (-input.Player.GarageZoom.ReadValue<Vector2>().y / 100) * scrollSpeed * Time.deltaTime;
    }
    public void ChangeMoveDir(Vector2 dir) => moveTo = dir;
    public void ZeroMoveDir() => moveTo = Vector2.zero;
}