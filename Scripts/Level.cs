using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // we to use this to change from scene to scene

public class Level : MonoBehaviour
{

    [SerializeField] float delayWhenDying = 2.5f;


    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);// start menu normaly will be scene 0 
        // FindObjectOfType<GameSession>().ResetGame();
        // this will call a methode that we use to reset the game after losing or game over
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // this return an int of the active scene in play 
        SceneManager.LoadScene(currentSceneIndex + 1);
        // then we load the scene after it 
    }

    public void LoadGame()
    {
       
        SceneManager.LoadScene("Game");
        // this will load the scene with the name "Game", it's better to not load scene 
        // by their names coz we could change the name in unity and it will not update here 

       if( FindObjectOfType<GameSession>()) // this is a check to see if GameSession is existed or not
        {
            FindObjectOfType<GameSession>().ResetGame(); 
            // this to reset the game and the score before the game start, by destroying GameSession class 
            // the first time the game launch GameSession is not yet created so that's why we put the if 
            // it's so that we don't get an error 
        }
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitThenLoad());

    }

    public IEnumerator WaitThenLoad()
    {
        
        yield return new WaitForSeconds(delayWhenDying);//the yield instruction or condition(here it's waiting for some seconds) 
        SceneManager.LoadScene("Game Over");
        // this will load the scene with the name "Game Over", it's better to not load scene 
        // by their names coz we could change the name in unity and it will not update here

    }

    public void QuitGame()
    {

        Application.Quit();

    }
}
