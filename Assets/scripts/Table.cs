using UnityEngine;
public class Table : MonoBehaviour
{
    [SerializeField] private Sprite plateSprite;
    [SerializeField] private Sprite[] foodSprites;
    private System.Random rand;
    private int plateCount = 0;
    private float plateDiameter;
    private float width;
    private float height;

    public int PlateCount => plateCount;

    // the table position changed to the bottom left conrner, don't call before awake
    private Vector2 TablePos => new(
            transform.position.x - (width / 2),
            transform.position.y - (height / 2));

    public void AddPlate()
    {
        // spawn a plate at a random position on the table, padding * 2  ecause the plate's origin is (0.5, 0.5)
        GameObject plateObj = new($"Plate {plateCount}");
        plateObj.transform.position = new Vector2(
            Random.Range(TablePos.x + (plateDiameter * 2), TablePos.x + width - (plateDiameter * 2)),
            Random.Range(TablePos.y + (plateDiameter * 2), TablePos.y + height - (plateDiameter * 2)));

        // add sprite plate renderer
        var plateRenderer = plateObj.AddComponent<SpriteRenderer>();
        plateRenderer.sprite = plateSprite;

        // add plate
        var plate = plateObj.AddComponent<Plate>();
        plate.table = this;

        // add food
        plate.food = new("Food");
        plate.food.transform.SetParent(plateObj.transform);
        plate.food.transform.position = new(
            plate.transform.position.x,
            plate.transform.position.y,
            plate.transform.position.z - 1);

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
            TablePos.x < pos.x &&
            TablePos.y < pos.y &&
            TablePos.x + width > pos.x &&
            TablePos.y + height > pos.y;
    }

    private void Awake()
    {
        // set a random seed on debug builds, otherwise use the system time as a seed.
        if (Debug.isDebugBuild)
            rand = new System.Random(0);
        else
            rand = new System.Random();

        // get the table's width and height
        var render = GetComponent<SpriteRenderer>();
        width = render.sprite.bounds.size.x;
        height = render.sprite.bounds.size.y;

        // get the plate's diameter for plate spawning
        plateDiameter = plateSprite.bounds.size.y / 2.0F;
    }

    // draws the table outline in the editor when the object is selected
    private void OnDrawGizmosSelected()
    {
        var render = GetComponent<SpriteRenderer>();
        float w = render.size.x;
        float h = render.size.y;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(w, h, 0));
    }

    // DEBUG: remove in final
    private void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            AddPlate();
        }
    }
}