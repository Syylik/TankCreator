using UnityEngine;

public class Builder : MonoBehaviour
{
    [HideInInspector] public Block blockToPlace;  //что ставим
    [SerializeField] private GameObject blockCursorPrefab;   //курсор для спавна
    [HideInInspector] public GameObject blockCursor;  //заспавненный курсор (блок следует за курсором)
    private PlayerInput input;
    public Transform car;  //куда создавать блоки (машина которую мы строим)
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
        if(Input.GetMouseButton(1)) //убираем выбранный блок на ПКМ (пока его не поставили)
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
        if(mouseHit.collider != null && Input.GetMouseButtonDown(1)) //удаляем блоки (уже поставленные)
        {
            if(mouseHit.collider.TryGetComponent<Block>(out Block block) && Input.GetMouseButtonDown(0))
            {
                if(block.canBeDeleted) Destroy(block.gameObject);
            }
        }
    }
    public void PickupBlock(Block block) //выбираем блок
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
            blockCursor.GetComponent<SpriteRenderer>().sprite = block.GetComponent<SpriteRenderer>().sprite; //делаем что-бы курсор выглядел, как блок
        }
    }
    public void SetTileVisibility(bool isVisible, Block.blockSizeEnum blockType) //делает все тайлы видимыми: (вкл/выкл, какой вид блока)
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
