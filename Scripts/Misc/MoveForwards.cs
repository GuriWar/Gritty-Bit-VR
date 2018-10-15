using UnityEngine;
using System.Collections;

public class MoveForwards : MonoBehaviour 
{
	public float speed;
    [SerializeField]
    bool randomSpeed;
    [SerializeField]
    Vector2 speedRange;

	
	// Update is called once per frame
	void Update () 
	{
        if (randomSpeed)
            speed = Random.Range(speedRange.x, speedRange.y);
		transform.localPosition += transform.forward * speed *Time.deltaTime;
        
	}
}
