using System;
using UnityEngine;
public class Plate : MonoBehaviour
{
    public Table table;
    public GameObject food;
    private bool released = false;
    private bool isEmpty = false;
    private float radius;


    // checks whether a position falls on the plate
    private bool IsOnPlate(Vector2 pos)
    {
        // get the relative position as a non-negative
        Vector2 rpos = new(transform.position.x - pos.x, transform.position.y - pos.y);
        rpos.x = Math.Abs(rpos.x);
        rpos.y = Math.Abs(rpos.y);

        // normalize a copy which will be a point on a circle
        Vector2 platePos = rpos;
        platePos.Normalize();
        platePos *= radius;

        return rpos.x < platePos.x && rpos.y < platePos.y;
    }

    // when the object is loaded
    private void Awake()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();

        // update the renderer and calculate the radius (assuming all sprites are the same size)
        radius = render.size.y / 2.0F;
    }

    // called on every frame upate
    private void Update()
    {
        bool mousePressed = Input.GetMouseButton(0);
        bool mouseDown = Input.GetMouseButtonDown(0);
        bool mouseUp = Input.GetMouseButtonUp(0);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (isEmpty == false && mouseDown)
        {
            // make the sprite empty if the plate is clicked
            isEmpty = true;
            Destroy(food);
        }
        else if (isEmpty == true && released == true && mousePressed)
        {
            // move the plate to the mouse's position when it is being held (after initially being released)
            transform.position = mousePos;
        }
        else if (isEmpty == true && mouseUp)
        {
            // set the mouse to be released
            released = true;
        }
        else if (table.IsOnTable(transform.position))
        {
            table.RemovePlate(this);
        }
    }

    // draws the hitbox outline in the editor when the object is selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}