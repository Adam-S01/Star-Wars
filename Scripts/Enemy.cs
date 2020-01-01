
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 25;

    [Header("Shooting")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float enemyLaserSpeed = -2f;// negatif coz it must go down

    [Header("Sound Effects")]
    [SerializeField] GameObject enemyPrtclExploPrefab;
    [SerializeField] float explosionDuration = 1f;
    [SerializeField] AudioClip deathSoundClip;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip firingSoundClip;
    [SerializeField] [Range(0, 1)] float firingSoundVolume = 0.25f;


    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);


    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();


    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;// this methode make the shooting frame independant
        if (shotCounter <= 0f)
        {
            Fire();// we shoot when the shotCounter is less or equal to zero
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);// after shooting we reset the counter 

        }



    }

    private void Fire()
    {

        GameObject laser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        // instantiate ( who , where, rotation ) 
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, enemyLaserSpeed);
        AudioSource.PlayClipAtPoint(firingSoundClip, Camera.main.transform.position, firingSoundVolume);// giving the laser sounds


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        // here, when a collision take place with the gameObject that this class attached to 
        // the other gameObject that is colliding with this class (should be is trigger in unity) 
        // is represented by the var "other", so we get the component DamageDealer from this gameObject "other"
        // and if he has one than we got the damage from him and reduce this class health 

        if (!damageDealer) { return; }
        // if damageDealer == null 
        // this line is to say that if something got collide with this class and doesn't have a DamageDealer component
        // then return nothing and we got out of this method ( OnTriggerEnter2D ) without getting an error

        ProcessHit(damageDealer);// this method got the damage from the damageDealer

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit(); // this line is to destroy the enemy projectile when colliding
        if (health <= 0)
        {
            Die();

        }
    }

    private void Die()
    {

        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        // this to add score to when the enemy dies 

        Destroy(gameObject);
        GameObject particleExplosion = Instantiate(enemyPrtclExploPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(particleExplosion, explosionDuration);
        PlayDeathSound();


    }

    private void PlayDeathSound()
    {
        AudioSource.PlayClipAtPoint(deathSoundClip, Camera.main.transform.position, deathSoundVolume);
        // PlayClipAtPoint() will play the sound even if the gameobject is destroyed 
        // and will take 3 parameter, the audio clip , the position and the volume 
        // the audio clip must be serialized and hooked , the position here we choose the camera position 
        // because when a game object is far from the camera the sound will be lower 
        // the volume is a float 
    }
}
