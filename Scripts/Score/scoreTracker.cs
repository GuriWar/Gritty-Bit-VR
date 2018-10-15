using UnityEngine;
using System.Collections;

public class scoreTracker : MonoBehaviour 
{
	public int score;
    [HideInInspector]
	public int kills;
    [HideInInspector]
    public int headShots;
    [SerializeField]
    float comboTime;
    float currentComboTime;
    [HideInInspector]
    public int comboCount;
    void Update()
    {
        currentComboTime += Time.deltaTime;
        if (currentComboTime >= comboTime)
        {
            comboCount = 1;
            currentComboTime = 0;
        }


    }

    public void AddKill(int points)
    {
        kills++;
        IncreaseScore(points);
        AddToCombo();
    }
	public void IncreaseScore(int points)
	{
		score += points;
	}

    void AddToCombo()
    {
        //if timer runs out combo is 0
        currentComboTime = 0;
        comboCount++;
        //while this is true, all enemies scores get a bonus depending on combocount
    }
    public void AddHeadShot()
    {
        headShots++;
    }
    //when you get a combo, pop up at the position of the last kill
}
