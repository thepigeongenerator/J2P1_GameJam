using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    void Start()
    {
        // make the mouse cursor invisable
        Cursor.visible = false;
    }
    void Update()
    {
        // get the mouseposition 
        Vector2 mousePosition = Input.mousePosition;
        // the object gets the same x and y relative to the screen/camera. and the x is beïng calculated to be always higher than the camera so the camera always see the object
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Mathf.Abs(Camera.main.transform.position.z - transform.position.z)));
    }
}
