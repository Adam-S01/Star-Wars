using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour // this enemySpawner.cs is attached to an empty object, we will use the code here 
                                          // to instantiating enemies using the information in the scriptable Object called 
                                          // WaveConfig 
{

    [SerializeField] List<WaveConfig> waveConfigs; // since we will use the scriptable object WaveConfig to get the information 
                                                   // then we need to hook the waves we create to this class as a list 
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;


    // Start is called before the first frame update
    IEnumerator Start()
        // we transform the Start() methode to an IEnumerator method 
        // we do this to apply the do while loop and to respawn all the waves
        // infinetly, so it's a coroutine inside a coroutine inside a coroutine
        // each one will be done when the condition is met 
    
    {

        do
        {


            yield return StartCoroutine(SpawnAllWaves());// this coroutine method will be used to spawn all the waves that are serialized
                                            // in the inspector, we use coroutine coz it give us the power to yield and wait 
                                            // so we spawn a wave and wait for the condition  

        } while (looping);


    }


    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex =startingWave; waveIndex < waveConfigs.Count;waveIndex++) // we are looping through all the waveConfigs
                                                                                     // that are attached to this gameObject in 
                                                                                     // the inspector 
        {

            var currentWave = waveConfigs[waveIndex];// creating a variable (currentWave) and assign to it the waveConfigs[waveIndex]
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave)); // this coroutine method will be used to instantiating
                                                                             // the enemies, for each wave in the waveConfigs we start 
                                                                             // this coroutine, which is also the yield condition

        }
        

    }


        private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig) 
    {
         // the parameter that we get the information from (waveConfig) is the serialized wave in the inspector

        int enemyCount = waveConfig.GetNumberOfEnemies();

        for (int i = 0; i < enemyCount; i++)//we are looping through the number of enemie inside the wave, to instantiate enemy objects
        {
           var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), 
               waveConfig.GetWaypoints()[0].transform.position, 
               Quaternion.identity);
            // the instantiate need 3 parameter ( who (object) , where (position) , and rotation ) 

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            // in this line, we pass the serialized waveConfig in this class to the EnemyPathing class, so that we don't 
            // need to got there and pass it manualy or serialized it, we only serialized it here in this class and attach it
            // to the other classes, 
            // note that the instantiation here is creating a game object which is the enemy 
            // and this enemy have the EnemyPathing script attached to it 
            // so we can put the instantiation inside a variable and entering it's component and pass waveConfig 

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());// this line is used to wait for the condition to 
                                                                               // reread the code in the method 


        }
       


    }



    // Update is called once per frame
    void Update()
    {

    }





}
