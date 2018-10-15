using UnityEngine;
using System.Collections;

public class scoreWorth : MonoBehaviour {
    [SerializeField]
	int Basepoints;
    [HideInInspector]
    public int points;
    scoreTracker scoreTracker;
    EnemySpawner enemyManager;

    void Start()
    {
        scoreTracker = GameObject.Find("Player").GetComponent<scoreTracker>();
        if (GameObject.Find("Enemy Manager") != null)
        enemyManager = GameObject.Find("Enemy Manager").GetComponent<EnemySpawner>();
    }

    void Update()
    {
        if (enemyManager != null)
            points = Basepoints * scoreTracker.comboCount * enemyManager.m_currentWaveNumber;
        else
            points = 0;
        //print("Total Points: " +points);
       // print("Score Tracker: " + scoreTracker.comboCount);
       // print("Wave number " + enemyManager.m_currentWaveNumber);
    }
}
