using UnityEngine;
public class Table : MonoBehaviour
{
    [SerializeField] private Sprite plateSprite;
    [SerializeField] private Sprite[] foodSprites;
    private System.Random rand;
    private int plateCount = 0;
    private float padding;
    private float width = 20;
    private float height = 10;

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
            Random.Range(TablePos.x + (padding * 2), TablePos.x + width - (padding * 2)),
            Random.Range(TablePos.y + (padding * 2), TablePos.y + height - (padding * 2)));

        // add sprite plate renderer
        var plateRenderer = plateObj.AddComponent<SpriteRenderer>();
        plateRenderer.sprite = plateSprite;

        // add plate
        var plate = plateObj.AddComponent<Plate>();
        plate.table = this;

        // add food
        plate.food = new("Food");
        plate.food.transform.SetParent(plateObj.transform);
        plate.food.transform.position = plate.transform.position;

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
            TablePos.x + padding > pos.x - padding &&
            TablePos.y + padding > pos.y - padding &&
            TablePos.x + padding + width < pos.x - padding &&
            TablePos.y + padding + height < pos.y - padding;
    }

    private void Awake()
    {
        // set a random seed on debug builds, otherwise use the system time as a seed.
        if (Debug.isDebugBuild)
            rand = new System.Random(0);
        else
            rand = new System.Random();

        // calculate set the plate range as the padding
        padding = plateSprite.bounds.size.y / 2.0F;
    }

    // draws the table outline in the editor when the object is selected
    private void OnDrawGizmosSelected()
    {
        float plateSize = plateSprite.bounds.size.y;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(width - plateSize, height - plateSize, 0));
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