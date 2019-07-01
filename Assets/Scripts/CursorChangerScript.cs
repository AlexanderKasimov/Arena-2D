using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChangerScript : MonoBehaviour
{
    //texture of cursor
    public Texture2D cursorImage;


    // Start is called before the first frame update
    void Start()
    {
        //setting cursor
        Cursor.SetCursor(cursorImage, new Vector2(cursorImage.width / 2, cursorImage.width / 2), CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
