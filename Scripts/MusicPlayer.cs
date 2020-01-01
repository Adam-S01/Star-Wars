using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
     
    void Awake()//the awake method is compiled before the start method, we need this for the singleton
    {
        SetUpSingleton(); // this is a method we create to use the singleton concept
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            // the singleton concept is that if we have 2 or more gameObject, that are the same  
            // in two different scenes, we destroy the new one before it get compiled and we keep 
            // the one from the previous scene.
            // FindObjectsOfType() find all the objects that are of a cretain name 
            // here we use GetType() which is a method that return the name of the current class 
            
            Destroy(gameObject);// so when we find the new gameObject with the same name we destroy it
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
