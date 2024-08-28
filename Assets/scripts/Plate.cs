using System;
using UnityEngine;
public class Plate : MonoBehaviour
{
    public Table table;
    public GameObject food;
    private bool isHeld = false;
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

        if (mouseDown && IsOnPlate(mousePos))
        {
            if (isEmpty)
            {
                // if the plate is empty, set the plate to be held
                isHeld = true;
                return;
            }
            // make the sprite empty if the plate is clicked
            isEmpty = true;
            Destroy(food);
        }
        else if (isEmpty == true && isHeld == true && mousePressed)
        {
            // move the plate to the mouse's position while it is being held and empty
            transform.position = mousePos;
        }
        else if (isHeld == true && mouseUp)
        {
            // make the mouse no longer held when the mouse is released
            isHeld = false;
        }
        else if (table.IsOnTable(transform.position))
        {
            // remove the plate if
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