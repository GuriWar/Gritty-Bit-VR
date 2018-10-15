using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_RandomColorChange : MonoBehaviour {
    Color randomColor;
    Color currentColor;

    [SerializeField]
    float speed;
	// Update is called once per frame
	void Update ()
    {
        currentColor = Color.Lerp(currentColor, randomColor, speed * Time.deltaTime);
		if (currentColor == randomColor)
        {
            NewColor();
        }

        GetComponent<HoloController>().HologramColor = currentColor;
	}

    void NewColor()
    {
        randomColor = new Color(Random.value, Random.value, Random.value, 1);
    }
}
