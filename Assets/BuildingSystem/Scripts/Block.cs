using UnityEngine;

public class Block : MonoBehaviour
{
    public bool canBeDeleted = true;
    public enum blockSizeEnum { big, doubleMini, mini}  //big - 4 мини-блока, dounbleMini - 2 мини-блока //mini - 1 мини-блок
    public blockSizeEnum blockSize;
}
