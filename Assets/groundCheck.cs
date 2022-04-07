using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    public bool onGround;
    public bool onGroundStatic;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "TilemapStatic")
            onGroundStatic = false;

        if (collision.name == "TilemapGround")
            onGround = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "TilemapStatic")
            onGroundStatic = true;

        if (collision.name == "TilemapGround")
            onGround = true;
    }
}
