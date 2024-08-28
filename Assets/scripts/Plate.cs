using System;
using UnityEngine;
public class Plate : MonoBehaviour
{
    public Table table;             // the table that the plate is on
    public GameObject food;         // food gameObject child
    private bool isEmpty = false;   // whether the plate is empty
    private float radius;           // radius of the plate to check whether a point falls on it, is set by the texture's height
    private int depth;              // render depth of the plate. Is set to the Z value.

    public bool IsEmpty => isEmpty;
    public int Depth => depth;

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

    // for setting the rendering depth of the plate
    public void SetDepth(int depth)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, depth);
        this.depth = depth;
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