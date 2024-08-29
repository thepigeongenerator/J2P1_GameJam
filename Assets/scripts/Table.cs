using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Table : MonoBehaviour
{
    [SerializeField] private Sprite plateSprite;    // contains the sprite containing the plate which'll be underneath the food
    [SerializeField] private Sprite[] foodSprites;  // contains the sprites of the different foods
    private LinkedList<Plate> plates;   // contains all plates, initialized in Awake() (using linked list for ease of moving nodes around)
    private System.Random rand;         // used for integer randomisation, initialized in Awake()
    private Score score;                // used for updating the score module
    private Plate heldPlate = null;     // reference to the plate that is held
    private float plateDiameter;        // the plate sprite's size to define padding for spawning plates
    private float width;                // the table's width defined by it's texture
    private float height;               // the table's height defined by it's texture

    // public interface for the plates list count
    public int PlateCount => plates.Count;

    // the table position changed to the bottom left conrner, don't call before awake
    private Vector2 TablePos => new(
            transform.position.x - (width / 2),
            transform.position.y - (height / 2));

    // adds a plate
    [ContextMenu("add plate")]
    public void AddPlate()
    {
        // spawn a plate at a random position on the table, padding * 2  ecause the plate's origin is (0.5, 0.5)
        GameObject plateObj = new($"Plate");
        {
            float minX = TablePos.x + (plateDiameter * 2);
            float minY = TablePos.y + (plateDiameter * 2);
            float maxX = TablePos.x + width - (plateDiameter * 2);
            float maxY = TablePos.y + height - (plateDiameter * 2);
            plateObj.transform.position = new Vector2(
                (float)rand.NextDouble() * (maxX - minX) + minX,
                (float)rand.NextDouble() * (maxY - minY) + minY);
        }
        // add sprite plate renderer
        var plateRenderer = plateObj.AddComponent<SpriteRenderer>();
        plateRenderer.sprite = plateSprite;

        // add plate
        var plate = plateObj.AddComponent<Plate>();
        plate.table = this;
        plate.SetDepth(-1); // if set to 0 plate depths won't update correctly

        // add food
        plate.food = new("Food");
        plate.food.transform.SetParent(plateObj.transform);
        plate.food.transform.position = new(
            plate.transform.position.x,
            plate.transform.position.y,
            plate.transform.position.z - 0.5F); // subtract 0.5 to have the food render above the plate

        // add food sprite renderer
        var foodRenderer = plate.food.AddComponent<SpriteRenderer>();
        int spriteIndex = rand.Next(foodSprites.Length);
        foodRenderer.sprite = foodSprites[spriteIndex];

        // add the plate at the end of the list.
        plates.AddFirst(plate);
        UpdatePlateDepth();
        transform.position = new Vector3(transform.position.x, transform.position.y, PlateCount); // make sure the table stays behind the plates. :3
    }

    // removes a plate
    public void RemovePlate(Plate plate)
    {
        plates.Remove(plate);
        Destroy(plate.gameObject);
        score.ScorePlus();
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

    // update plate depth (wow, the inefficiency, but unity does unity, I guess)
    private void UpdatePlateDepth()
    {
        int i = 0;
        foreach (Plate p in plates)
        {
            if (p.Depth == i) break; // assuming the rest is sorted when depths lines up.
            p.SetDepth(i);
            i++;
        }
    }

    // called when the script is being loaded
    private void Awake()
    {
        // set a random seed on debug builds, otherwise use the system time as a seed.
        if (Debug.isDebugBuild)
            rand = new System.Random(1);
        else
            rand = new System.Random();

        score = FindObjectOfType<Score>();
        plateDiameter = plateSprite.bounds.size.y / 2.0F; // get the plate's diameter for plate spawning
        plates = new LinkedList<Plate>();

        // get the table's width and height
        var render = GetComponent<SpriteRenderer>();
        width = render.sprite.bounds.size.x;
        height = render.sprite.bounds.size.y;
    }

    // called on every frame
    private void Update()
    {
        // get mouse data
        bool mousePressed = Input.GetMouseButton((int)MouseButton.Left);    // wether the mouse button is being pressed
        bool mouseDown = Input.GetMouseButtonDown((int)MouseButton.Left);   // whether the mouse is starting to be pressed
        bool mouseUp = Input.GetMouseButtonUp((int)MouseButton.Left);       // whether the mouse is released
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // don't do anything if there's nothing to do
        if (mousePressed == false && mouseUp == false)
        {
            return;
        }

        // manage the held plate, if the mouse is pressed move it, otheriwse set it back to 'null'
        if (heldPlate != null)
        {
            if (mousePressed == true)
            {
                // just move the plate to the mouse's position
                heldPlate.transform.position = mousePos;
            }
            else if (mouseUp == true)
            {
                // remove the plate if it is no longer on the table when released
                Vector3 closestToTable = transform.position - heldPlate.transform.position;
                closestToTable.Normalize();
                if (IsOnTable(heldPlate.transform.position + closestToTable) == false)
                    RemovePlate(heldPlate);

                // reset held plate to "null"
                heldPlate = null;
            }
            else
            {
                Debug.LogWarning("held plate isn't null, but nothing is done with it");
            }

            return;
        }

        // at this point we have no use if the mouse is being held
        if (mouseDown == false) return;

        // loop through the plates, the first one that shows up will be emptied or moved up in the list and the held plate if already empty
        foreach (Plate plate in plates)
        {
            // check if the mouse position falls on the plate
            if (plate.IsOnPlate(mousePos))
            {
                if (plate.IsEmpty == false)
                {
                    // just empty the plate if it isn't empty
                    plate.EmptyPlate();
                }
                else
                {
                    // make this plate the new held plate and update the position
                    heldPlate = plate;
                    plate.transform.position = mousePos;
                }

                // put this plate above the other ones
                plates.Remove(plate);
                plates.AddFirst(plate);
                UpdatePlateDepth();

                // no need to check further
                break;
            }
        }
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
}