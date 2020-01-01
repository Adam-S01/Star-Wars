using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player ")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float laserFiringPeriod = 0.1f;

    [SerializeField] GameObject playerPrtclExploPrefab;
    [SerializeField] float explosionDuration = 1f;

    [SerializeField] AudioClip deathSoundClip;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;

    [SerializeField] AudioClip firingSoundClip;
    [SerializeField] [Range(0, 1)] float firingSoundVolume = 0.25f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    Coroutine firingCouroutine;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();


    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();


    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCouroutine = StartCoroutine(FireContinuously());
            // coroutine is a methode that can suspend it's excution until the yield instruction is met 
            // the method here is FireContinuously() , we put it later as a IEnumerator type 
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCouroutine);

        }
    }
    IEnumerator FireContinuously()// the method used in the coroutine above 
    {
        while (true)// while (true) is what make the shooting continues when we press the hold button
        {

            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;// instantiate the laser 
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);// giving the instantiated laser speed
            AudioSource.PlayClipAtPoint(firingSoundClip, Camera.main.transform.position, firingSoundVolume);// giving the laser sounds

            yield return new WaitForSeconds(laserFiringPeriod);//the yield instruction or condition(here it's waiting for some seconds) 



        }

    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        // Input.GetAxis(""Horizontal") give back a float value between -1 and 1, if the user press right on the keyboard it
        // will return a positive value and if he press left it'll return a negative value, 
        // this method is automatically implemented by the left right up and down or the a s d w buttons that return values 
        // since we are using this method in the updat , then it's called each frame , and we don't want to move the player 
        // based on the frame/sec coz it varies from a computer to another , so we need to move independently from the frames 
        // that why we use Time.deltaTime
        // Time.deltaTime is a value calculated by unity , this value return the duration of frame 
        // we multiply it by get axis value, so if the frame per seconds = 10 , then the duration of frame = 1/10 sec 
        // it the f/s =  100 the the duration of frame = 1/100 , so by multiplying we got the same result as a movement 
        // moveSpeed is a variable to control the speed of the mouvment 
        // Debug.Log(deltaX);

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);


    }


    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;



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
        damageDealer.Hit();// this line is to destroy the enemy projectile when colliding
        if (health <= 0)
        {
            Die();

        }

    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject particleExplosion = Instantiate(playerPrtclExploPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(particleExplosion, explosionDuration);
        PlayDeathSound();
        FindObjectOfType<Level>().LoadGameOver();
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
    public int GetHealth()
    {
        // we'll use this method to return the score to the text that display the score (in ScoreDisplay class)
        return health;
    }


}
