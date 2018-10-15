using UnityEngine;
using System.Collections;

public class headShotMultiplier : MonoBehaviour 
{
	[SerializeField]
	int m_multiplier;
	[SerializeField]
	scoreWorth scoreScript;

	public void MultiplyScore()
	{
		scoreScript.points *= m_multiplier;
	}

    public void HeadShot()
    {
        GameObject.Find("Player").GetComponent<scoreTracker>().AddHeadShot();
    }
}
