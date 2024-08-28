using UnityEngine;
public class Table : MonoBehaviour
{
    [SerializeField] private Sprite plateSprite;
    [SerializeField] private Sprite[] foodSprites;
    private System.Random rand;
    private int plateCount = 0;
    private float width = 20;
    private float height = 10;

    public int PlateCount => plateCount;

    // the table position changed to the bottom left conrner, don't call before awake
    private Vector2 TablePos => new(
            transform.position.x - (width / 2),
            transform.position.y - (height / 2));

    public void AddPlate()
    {
        GameObject plateObj = new($"Plate {plateCount}");
        plateObj.transform.position = Vector3.zero;

        // add sprite plate renderer
        var plateRemderer = plateObj.AddComponent<SpriteRenderer>();
        plateRemderer.sprite = plateSprite;

        // add plate
        var plate = plateObj.AddComponent<Plate>();
        plate.table = this;

        // add food
        plate.food = new("Food");
        plate.food.transform.SetParent(plateObj.transform);

        // add food sprite renderer
        var foodRenderer = plate.food.AddComponent<SpriteRenderer>();
        int spriteIndex = rand.Next(foodSprites.Length);
        foodRenderer.sprite = foodSprites[spriteIndex];


        plateCount++;
    }

    public void RemovePlate(Plate plate)
    {
        Destroy(plate.gameObject);
        plateCount--;
    }

    // checks whether the position falls on the table
    public bool IsOnTable(Vector2 pos)
    {
        return
            TablePos.x > pos.x &&
            TablePos.y > pos.y &&
            TablePos.x + width < pos.x &&
            TablePos.y + height < pos.y;
    }

    private void Awake()
    {
        if (Debug.isDebugBuild)
            rand = new System.Random(0);
        else
            rand = new System.Random();
    }

    // draws the table outline in the editor when the object is selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    // DEBUG: remove in final
    private void Start()
    {
        AddPlate();
    }
}