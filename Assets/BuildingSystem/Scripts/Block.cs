using UnityEngine;

public class Block : MonoBehaviour
{
    public bool canBeDeleted = true;
    public enum blockSizeEnum { big, doubleMini, mini}  //big - 4 ����-�����, dounbleMini - 2 ����-����� //mini - 1 ����-����
    public blockSizeEnum blockSize;
}
