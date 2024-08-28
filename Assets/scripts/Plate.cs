using System;
using UnityEngine;
public class Plate : MonoBehaviour
{
    private bool isEmpty = false;
    private float radius;
    public GameObject food;
    public Table table;


    // called when the mouse is held or clicked
    private void OnMouse(bool isHeld)
    {
        // if the plate is empty, move the plate to the mouse's position
        if (isEmpty == true)
        {
            transform.position = Input.mousePosition;
            return;
        }

        // return if the mouse is held at this point
        if (isHeld) return;

        //otherwise decrease the fullness and update the sprite
        isEmpty = true;
        Destroy(food);
    }

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
        rpos *= radius;

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
        if (Input.GetMouseButton(0) && IsOnPlate(Input.mousePosition))
        {
            bool isHeld = Input.GetMouseButtonDown(0) == false;
            OnMouse(isHeld);
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