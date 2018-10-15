using UnityEngine;
using System.Collections;
using System.Collections.Generic;

struct Wave
{
    public Wave(int amount, float rate)
    {
        spawnRate = rate;
        enemyAmount = amount;
    }
	//need an animation to be represented by some value
	public float spawnRate;
	public int enemyAmount; // keep 0 if moving 
}

public class EnemySpawner : MonoBehaviour 
{
	bool m_canSpawn;
	[SerializeField]
	List<GameObject> m_EnemiesToSpawn = new List<GameObject>();
	Wave m_currentWave;
    //preset all enemies to spawn within a certain navmesh area
    [SerializeField]
    GameObject spawnObjectRanged;
    [SerializeField]
    GameObject spawnObjectMelee;
	float currentTime;
    List<Transform> SpawnTransformsRanged = new List<Transform>();
    List<Transform> SpawnTransformsMelee = new List<Transform>();
	[SerializeField]
	List<GameObject> m_currentlySpawnedEnemies = new List<GameObject>();
    [SerializeField]
    float m_minSpawnRate;
    [SerializeField]
    GameObject miniMap;
    GameObject player;
    [SerializeField]
    GameObject waveText;
    gameState gameState;
    public int m_currentWaveNumber = 1;
    float m_currentSpawnRate = 3.3f;
    int m_currentNumberOfEnemiesToSpawn = 0;
    int m_numberToAddToNextWave;
    int m_maxEnemies;
    int templarHealth = 300;
    int chargerHealth = 300;

    void Start()
    {
        foreach (Transform child in spawnObjectRanged.transform)
        {
            SpawnTransformsRanged.Add(child);
        }

        foreach (Transform child in spawnObjectMelee.transform)
        {
            SpawnTransformsMelee.Add(child);
        }

        m_maxEnemies = GameObject.Find("CoverSpots").GetComponent<CoverManager>().m_coverSpots.Count;
        player = GameObject.Find("Player");
        gameState = GameObject.Find("Level Scripts").GetComponent<gameState>();
        PrepareNextWave();
        StartCoroutine(StartNextWave());
    }

	// Update is called once per frame
	void Update () 
	{
		currentTime += Time.deltaTime;

		if (m_currentWave.enemyAmount >0)
		{
		    if(m_currentWave.spawnRate <= currentTime)
		    {
		    	currentTime = 0;
                SpawnEnemy();
		    }	
		}
        
		if (m_currentWave.enemyAmount == 0 && m_currentlySpawnedEnemies.Count == 0) // all enemies cleared go to next wave
		{

            PrepareNextWave();
            gameState.StartUpgrading();
		}
	}

    void SpawnEnemy()
    {
        GameObject tempEnemy;
        int enemyIndex = Random.Range(0, m_EnemiesToSpawn.Count);
        tempEnemy = (GameObject)Instantiate(m_EnemiesToSpawn[enemyIndex]);
        tempEnemy.SetActive(false);
        //get run action and set it a random cover position
        if (tempEnemy.GetComponent<g_AIID>().ID == 0)
        {
            int spawnIndex = Random.Range(0, SpawnTransformsRanged.Count);
            tempEnemy.transform.position = SpawnTransformsRanged[spawnIndex].position;
            tempEnemy.transform.rotation = SpawnTransformsRanged[spawnIndex].rotation = SpawnTransformsRanged[spawnIndex].rotation;
            tempEnemy.GetComponent<g_AIHealthScript>().health = templarHealth;

        }
        else if (tempEnemy.GetComponent<g_AIID>().ID == 1)
        {
            int spawnIndex = Random.Range(0, SpawnTransformsMelee.Count);
            tempEnemy.transform.position = SpawnTransformsMelee[spawnIndex].position;
            tempEnemy.transform.rotation = SpawnTransformsMelee[spawnIndex].rotation = SpawnTransformsMelee[spawnIndex].rotation;
            tempEnemy.GetComponent<g_AIHealthScript>().health = chargerHealth;
        }
        tempEnemy.SetActive(true);
        m_currentWave.enemyAmount--;
        m_currentlySpawnedEnemies.Add(tempEnemy);       
            
    }

    public void KillEnemy(GameObject enemy)
    {
        m_currentlySpawnedEnemies.Remove(enemy);
    }

    void PrepareNextWave()
    {
        //make a new wave, everytime decrease spawnrate and increase enemy number
        m_currentWaveNumber++;
        m_numberToAddToNextWave = m_currentWaveNumber;
        m_currentNumberOfEnemiesToSpawn += m_numberToAddToNextWave/2;
        if (m_currentNumberOfEnemiesToSpawn < 1)
            m_currentNumberOfEnemiesToSpawn = 1;

        if (m_currentNumberOfEnemiesToSpawn > m_maxEnemies)
        {
            m_currentNumberOfEnemiesToSpawn = m_maxEnemies;
        }
        m_currentSpawnRate = 3 - (m_currentWaveNumber - 1) *.2f;
        if (m_currentSpawnRate <= m_minSpawnRate)
            m_currentSpawnRate = m_minSpawnRate;
        
        m_currentWave.enemyAmount = -1;

        //Find a better way to do this using gamestate after buttons are set up (line switch)
    }

    public IEnumerator StartNextWave()
    {
        waveText.SetActive(true);
        waveText.GetComponent<WaveDisplay>().SetWaveNumber(m_currentWaveNumber);
        yield return new WaitForSeconds(2);
        waveText.SetActive(false);
        yield return new WaitForSeconds(1);
        m_currentWave = new Wave(m_currentNumberOfEnemiesToSpawn, 0);
    }

    public void DeleteAllEnemies()
    {
        for (int i = 0; i < m_currentlySpawnedEnemies.Count; i++)
        {
            Destroy(m_currentlySpawnedEnemies[i]);
        }
        if (gameObject != null)
        Destroy(gameObject);
    }
}
