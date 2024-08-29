using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] Texture2D newCursor;//store the texture we wand for the curser
    void Start()
    {
        Cursor.SetCursor(newCursor,Vector2.zero,CursorMode.ForceSoftware);//here the cursor gets a new texture
    }

}
