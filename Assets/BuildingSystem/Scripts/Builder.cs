using UnityEngine;

public class Builder : MonoBehaviour
{
    [HideInInspector] public Block blockToPlace;  //��� ������
    [SerializeField] private GameObject blockCursorPrefab;   //������ ��� ������
    [HideInInspector] public GameObject blockCursor;  //������������ ������ (���� ������� �� ��������)
    private PlayerInput input;
    public Transform car;  //���� ��������� ����� (������ ������� �� ������)
    public static Builder instance { get; private set; }

    private void Awake()
    {
        instance = this;
        input = new PlayerInput();
    }
    private void Start()
    {
        SetTileVisibility(false, Block.blockSizeEnum.big);
        SetTileVisibility(false, Block.blockSizeEnum.mini);
    }
    private void Update()
    {
        if(Input.GetMouseButton(1)) //������� ��������� ���� �� ��� (���� ��� �� ���������)
        {
            if(blockToPlace != null)
            {
                SetTileVisibility(false, Block.blockSizeEnum.big);
                SetTileVisibility(false, Block.blockSizeEnum.mini);
                blockToPlace = null;
                Destroy(blockCursor);
            }
        }
        RaycastHit2D mouseHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
        if(mouseHit.collider != null && Input.GetMouseButtonDown(1)) //������� ����� (��� ������������)
        {
            if(mouseHit.collider.TryGetComponent<Block>(out Block block) && Input.GetMouseButtonDown(0))
            {
                if(block.canBeDeleted) Destroy(block.gameObject);
            }
        }
    }
    public void PickupBlock(Block block) //�������� ����
    {
        if(blockToPlace == null)
        {
            blockToPlace = block;
            if(blockToPlace.blockSize == Block.blockSizeEnum.big)
            {
                SetTileVisibility(true, Block.blockSizeEnum.big);
                SetTileVisibility(false, Block.blockSizeEnum.mini);
            }
            if(blockToPlace.blockSize == Block.blockSizeEnum.mini)
            {
                SetTileVisibility(true, Block.blockSizeEnum.mini);
                SetTileVisibility(false, Block.blockSizeEnum.big);
            }
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            blockCursor = Instantiate(blockCursorPrefab, mousePos, Quaternion.identity);
            blockCursor.GetComponent<SpriteRenderer>().sprite = block.GetComponent<SpriteRenderer>().sprite; //������ ���-�� ������ ��������, ��� ����
        }
    }
    public void SetTileVisibility(bool isVisible, Block.blockSizeEnum blockType) //������ ��� ����� ��������: (���/����, ����� ��� �����)
    {
        var tiles = GameObject.FindObjectsOfType<Tile>();
        if(blockType == Block.blockSizeEnum.big)
        {
            foreach(var tile in tiles)
            {
                if(isVisible && !tile.isMini) tile.ShowTile();
                if(!isVisible && !tile.isMini) tile.HideTile();
            }
        }
        if(blockType == Block.blockSizeEnum.mini)
        {
            foreach(var tile in tiles)
            {
                if(isVisible && tile.isMini) tile.ShowTile();
                if(!isVisible && tile.isMini) tile.HideTile();
            }
        }
    }
}
