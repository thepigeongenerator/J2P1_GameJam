using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] Texture2D newCursor;
    void Start()
    {
        // make the mouse cursor invisable
        Cursor.SetCursor(newCursor,Vector2.zero,CursorMode.ForceSoftware);
    }

}
