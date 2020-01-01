using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{

     int score = 0;


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
           // this will keep this gameObject on the awake of any scene EVEN IF THIS CLASS WAS NOT IN THE SCENE
        }
    }

    public int GetScore()
    {
        // we'll use this method to return the score to the text that display the score (in ScoreDisplay class)
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        // we'll use this method to add a value to the score, we'll call it when an enemy die and we want 
        // to add a score to the score 
        score += scoreValue;
    }

    public void ResetGame()
    {
        // we'll use this method when we load the game scene, so that we restart the score to zero before the game 
        // start 
        Destroy(gameObject); // we could set only the score to zero instead of destroying but rick it's better to destroy it

    }
    
}
