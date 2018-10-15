using UnityEngine;
using System.Collections;

public class interpolateUpandDown : MonoBehaviour 
{
	[SerializeField]
	Vector2 speedRange;
	float speed;
	[SerializeField]
	Vector2 movementOffsetRange;
	float movementOffset;
	Vector3 movePosition;
	Vector3 defaultPosition;
	bool calculateNewPosition = false;
	bool negative;
	void Start()
	{
		defaultPosition = transform.localPosition;
		speed = Random.Range(speedRange.x, speedRange.y);
		movementOffset = Random.Range(movementOffsetRange.x, movementOffsetRange.y);
	}

	// Update is called once per frame
	void Update () 
	{
		transform.localPosition = Vector3.Lerp(transform.localPosition, movePosition, speed*Time.deltaTime);
		if ((movePosition - transform.localPosition).magnitude < .2f)
		{
			CalculateNewPosition();
		}
	}

	void CalculateNewPosition()
	{
		int yeah;
		negative = !negative;
		if (negative)
			yeah = 1;
		else
			yeah = -1;
		movePosition = defaultPosition + new Vector3(0f, movementOffset * yeah, 0f);

	}
}
