using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_Trigger : MonoBehaviour
{
    [SerializeField]
    List<string> activeTags = new List<string>();
    [SerializeField]
    GameObject triggerObject;
    [SerializeField]
    string triggerFunction;
    [SerializeField]
    AudioClip clip;
    public void Damage(float damage)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
        triggerObject.SendMessage(triggerFunction, gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        for (int i= 0; i < activeTags.Count; i++)
        {
            if (other.tag == activeTags[i])
            {
                break;
            }
            if (i == activeTags.Count)
            {
                return;
            }
        }
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
        triggerObject.SendMessage(triggerFunction, gameObject);
    }
}
