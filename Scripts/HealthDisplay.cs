using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;// we're working in UI so we need to add unityEngine.UI
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    // if we have many levels we might wants to make this a singleton 

    TextMeshProUGUI healthText;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>(); // caching GetComponent<> , it's heavy if you use it in update()
                                                      //this return the textmeshpro in unity coz it's a component hooked to this class
        player = FindObjectOfType<Player>();// caching findOjectOfType<> , it's heavy if you use it in update()
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = player.GetHealth().ToString();
        // we are assigning the health value in the game
        // we could write it like this : 
        // GetComponent<TextMeshProUGUI>().text = Player.GetHealth().ToString();
        // but it would be heavy
    }
}

 