using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;
    public GameObject playerCharacter;
    private float searchCountdown = 1f;

    public Transform[] spawnPoints;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        waveCountDown = timeBetweenWaves;

    }



     void Update()
    {

        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                // begin new round
            } 
            else
            {
                return;
            }

        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }
    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning Wave: " + wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f /wave.rate);

        }

        state = (SpawnState.WAITING);
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();

            }
            else
            {
                yield break;
            }



            yield break;
        }


    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");
        }



        nextWave++;

    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)

        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }


        }
        return true;

    }

    void SpawnEnemy(Transform _enemy)

    {
        Debug.Log("Spawning Enemy: " + _enemy.name);
        Instantiate(_enemy, transform.position, transform.rotation).GetComponent<Enemy>().AssignTarget(playerCharacter.transform);




    }
}








