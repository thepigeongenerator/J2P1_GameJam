using System;
using UnityEngine;
public class Plate : MonoBehaviour
{
    public Table table;
    public GameObject food;
    private bool isEmpty = false;
    private float radius;

    public bool IsEmpty => isEmpty;

    // checks whether a position falls on the plate
    public bool IsOnPlate(Vector2 pos)
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

    // empties the plate (can only be called once, otherwise causes unpredictable behaviour)
    public void EmptyPlate()
    {
        isEmpty = true;

        // destroy the food object and remove the reference to it.
        Destroy(food);
        food = null;
    }

    // when the object is loaded
    private void Awake()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();

        // update the renderer and calculate the radius (assuming all sprites are the same size)
        radius = render.size.y / 2.0F;
    }

    // draws the hitbox outline in the editor when the object is selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}