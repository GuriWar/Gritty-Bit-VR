using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_LightIntensity : MonoBehaviour
{
    [SerializeField]
    float defaultIntensity;
    [SerializeField]
    float maxIntensity;
    float currentIntensity;
    [SerializeField]
    float speed;
    void Start()
    {
        currentIntensity = defaultIntensity;
    }


    // Update is called once per frame
    void Update()
    {
        if (currentIntensity != defaultIntensity)
        {
            currentIntensity = Mathf.Lerp(currentIntensity, defaultIntensity, Time.deltaTime * speed);
        }
        GetComponent<Light>().intensity = currentIntensity;
    }

    public void ChangeToHighIntensity()
    {
        currentIntensity = maxIntensity;
    }

}
