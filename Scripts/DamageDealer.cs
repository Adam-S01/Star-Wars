using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    // this class will be used for each gameObject that can deal damage 
    // we attach this class to it and give him a damage variable 
    // and when we need to apply damage following to a collision, we call for this 
    // class and get the specified value
     

    [SerializeField] int damage = 100;

    public int GetDamage()
    {
        return damage;     

    }

    public void Hit()
    {
        Destroy(gameObject);

    }
      
}

 
