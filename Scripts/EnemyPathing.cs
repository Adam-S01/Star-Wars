using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
        // this line is to put the enemy (the attached game object to this script) , in the same position 
        // as the waypoints[0] 
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();




    }

    public void SetWaveConfig ( WaveConfig waveConfig)
    {
        // note that this is a local variable waveConfig different that the gloabla variable waveConfig 
        this.waveConfig = waveConfig; // this.waveConfig is the global variable in this class 
                                      // we assigned to it the waveConfig the local parameter in this method 
                                      
    }



    private void MoveEnemy()
    {
        if (waypointIndex <= waypoints.Count - 1) // for the lists we use .count for the array we use .length coz it's fixed
        {
            var targetPosition = waypoints[waypointIndex].transform.position;// this variable is assigned with the value of the position
                                                                             // of the waypoints[] ( each point we put in unity ) 
                                                                             
            var mouvementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime; 
            // this var is used to specify the speed of the enemy we multiply it by Time.deltaTime to make the speed independant
            // from the frame/second 

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, mouvementThisFrame);
            // the .MoveToWards method make an object move from point a to point b with a certain speed 
            // the .MoveToWards method take 3 parameter ( current position as a vector, targeted position as a vector, 
            // and the mouvment speed ) 


            if (transform.position == targetPosition)
            {
                waypointIndex++; // when the gameObject ( enemy here ) reach the targeted position wich is the waypoint 
                                 // we increment the index so that the enemy start move towards the next waypoint 
            }



        }
        else
        {
            Destroy(gameObject); // when there is no more waypoints in the path we destroy the gameObject 
        }
    }
}
