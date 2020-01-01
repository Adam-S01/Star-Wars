using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shredder : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) // this method is called when a collider collide with the 
                                                        // collider of this object ( shredder ) , the collider2d of the 
                                                        // the object that collide with the shredder is passed as 
                                                        // parameter with the name collision
    {
       Destroy(collision.gameObject);// the collision.gameObject is the game object that collid with the shredder 
    }

    

}
