using UnityEngine;
public class Table : MonoBehaviour
{
    [SerializeField] private Sprite plateSprite;
    [SerializeField] private Sprite[] foodSprites;
    private System.Random rand;
    private int plateCount = 0;

    public int PlateCount { get => plateCount; }

    public void AddPlate()
    {
        // TODO: select a random X and Y position on the surface of the table (add padding)
        GameObject plateObj = new($"Plate {plateCount}");
        plateObj.transform.position = Vector3.zero;

        // add sprite renderer
        var plateRemderer = plateObj.AddComponent<SpriteRenderer>();
        plateRemderer.sprite = plateSprite;

        // add plate
        var plate = plateObj.AddComponent<Plate>();
        plate.table = this;

        GameObject foodObj = new("Food");
        foodObj.transform.SetParent(plateObj.transform);
        var foodRenderer = foodObj.AddComponent<SpriteRenderer>();
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
        // TODO: currently just assume false. implementing this later
        return false;
    }

    private void Awake()
    {
        if (Debug.isDebugBuild)
            rand = new System.Random(0);
        else
            rand = new System.Random();
    }

    private void Start()
    {
        AddPlate();
    }
}