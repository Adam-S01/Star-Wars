using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{

    [SerializeField] float backgroundScrollSpeed = 0.1f; 
    Material myMateial;// variable of tye Material
    Vector2 offset;



    // Start is called before the first frame update
    void Start()
    {
        myMateial = GetComponent<MeshRenderer>().material;// we assign the material in the quad to this variable
        offset = new Vector2(0f, backgroundScrollSpeed) ;// we are scrolling in the y direction that's why x = 0 
    }

    // Update is called once per frame
    void Update()
    {
        myMateial.mainTextureOffset += offset * Time.deltaTime;
        // this is the way to say to the material to move by a certain ammount of value which is a Vector2
        // we multiply by Time.deltatime to stay independant of frame/sec
        
               
    }
}
