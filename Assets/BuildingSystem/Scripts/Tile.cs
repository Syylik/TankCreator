using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isMini;
    public bool isOcupped; //занято ли место
    [SerializeField] private Color freeSlotColor; //цвет, свободного места
    [SerializeField] private Color ocuppedSlotColor; //цвет, занятого места
    private SpriteRenderer spriteRenderer;
    private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();
    private void Update()
    {
        if(!isOcupped) spriteRenderer.color = freeSlotColor;
        else spriteRenderer.color = ocuppedSlotColor;
    }
    private void OnMouseDown()
    {
        if(Builder.instance.blockToPlace != null && !isOcupped)
        {
            var selectedBlock = Builder.instance.blockToPlace;
            Instantiate(selectedBlock, transform.position, selectedBlock.transform.rotation, Builder.instance.car);
            Builder.instance.blockToPlace = null;
            Builder.instance.SetTileVisibility(false, Block.blockSizeEnum.big);
            Builder.instance.SetTileVisibility(false, Block.blockSizeEnum.mini);
            isOcupped = true;
            Destroy(Builder.instance.blockCursor);
        }
    }
    public void HideTile() => spriteRenderer.enabled = false;
    public void ShowTile()
    {
        if(!isOcupped) spriteRenderer.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Block>(out Block block) && block != GetComponentInParent<Block>())
        {
            isOcupped = true;
            spriteRenderer.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Block>(out Block block) && block != GetComponentInParent<Block>())
        {
            isOcupped = false;
            spriteRenderer.enabled = true;
        }
    }
}
