using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;// we're working in UI so we need to add unityEngine.UI
using TMPro;


public class ScoreDisplay : MonoBehaviour
{

    TextMeshProUGUI scoreText;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>(); // caching GetComponent<> , it's heavy if you use it in update()
                                                    //this return the textmeshpro in unity coz it's a component hooked to this class
        gameSession = FindObjectOfType<GameSession>();// caching findOjectOfType<> , it's heavy if you use it in update()
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = gameSession.GetScore().ToString();
        // we are assigning the score value in the game
        // we could write it like this : 
        // GetComponent<TextMeshProUGUI>().text = gameSession.GetScore().ToString();
        // but it would be heavy
    }
}
