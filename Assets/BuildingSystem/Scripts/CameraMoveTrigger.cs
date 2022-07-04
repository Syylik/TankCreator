using UnityEngine;

public class CameraMoveTrigger : MonoBehaviour //����� �� ��������( ������� ��
{
    [SerializeField] private Vector2 moveDir = Vector2.zero;
    private void OnMouseEnter() => GarageCameraMovement.instance.ChangeMoveDir(moveDir);
    private void OnMouseExit() => GarageCameraMovement.instance.ZeroMoveDir();
}
