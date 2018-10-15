using UnityEngine;
using System.Collections;

public class ObjectState : MonoBehaviour 
{
    public int ID;
	public enum ObjectStates
    {
		Holstered,
		Held,
        Free,
        Decaying
	};
	public ObjectStates currentState;
    g_ObjectManager ObjectManager;
    [SerializeField]
    CustomDissolve dissolveScript;
    [SerializeField]
    bool lifeLine;
    [SerializeField]
    float freeTimeUntilDelete;
    [SerializeField]
    Collider collider;
    bool startCountingFreeTime;
    float currentFreeTime;

    void Start()
    {
        ObjectManager = GameObject.Find("Object Manager").GetComponent<g_ObjectManager>();
        Physics.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider>(), collider);

    }
    void Update()
    {
        if (currentState == ObjectStates.Decaying)
            StartDissolve();
        else if (currentState == ObjectStates.Held)
            currentFreeTime = 0;
        if (startCountingFreeTime)
        {
            currentFreeTime += Time.deltaTime;
            if (currentFreeTime >= freeTimeUntilDelete)
            {
                StartDissolve();
                currentFreeTime = 0;
            }
        }
    }

    public void BeginDeleteCountdown()
    {
        startCountingFreeTime = true;
    }
    void StartDissolve()
    {
        if (lifeLine)
        {
            currentState = ObjectStates.Decaying;
            if (dissolveScript)
            {
                dissolveScript.Dissolve();
                ObjectManager.m_Objects.Remove(gameObject);
                Destroy(gameObject, dissolveScript.dissolveDuration);
            }
            else
            {
                ObjectManager.m_Objects.Remove(gameObject);
                Destroy(gameObject, 1);
            }
            Destroy(this);
        }
    }

    public void PickupObject()
    {
        currentState = ObjectStates.Held;
        if (GetComponent<AudioSource>())
            GetComponent<AudioSource>().enabled = true;
    }
}
