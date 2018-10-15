using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
[System.Serializable]
public struct AIAction
{
	public string name;
	public enum ActionStates {attackVolley, crouch, runTo, crawlTo, chargePlayer, hoverAndShoot, boss};
	public ActionStates action;
	public Vector2 actionTimeRange;
	[HideInInspector]
	public float actionTime;
	public string nextActionName;
	public void Setup()
	{
		if (action ==AIAction.ActionStates.runTo || action ==AIAction.ActionStates.crawlTo)
		{
			actionTimeRange =Vector2.zero;
			actionTime =0;
		} 

	}
	public void setActionTime()
	{
		actionTime = Random.Range (actionTimeRange.x, actionTimeRange.y);
	}

}	

public class g_AIBehaviourScript : MonoBehaviour
{
	public List<AIAction> Actions = new List<AIAction>();
    [SerializeField]
	AIAction currentAction;
	int actionIndex;
	NavMeshAgent navMesh;
	g_AIAnimationScript animationScript;
	g_AIGunScript gunScript;
	g_AISoundScript soundScript;
	float m_currentActionTime;
	//[HideInInspector]
	public GameObject m_coverTarget;
	//[HideInInspector]
	public GameObject m_attackTarget;
    public LayerMask lineOfSightMask;

    GameObject player;
	// Use this for initialization
	void Start () 
	{
		navMesh = GetComponent<NavMeshAgent> ();
		animationScript = GetComponent<g_AIAnimationScript> ();
		gunScript = GetComponent<g_AIGunScript> ();
		soundScript = GetComponent<g_AISoundScript>();
        player = GameObject.Find("Player");
		for (int i =0; i <Actions.Count; i++)
		{
			Actions[i].Setup();
            if (Actions[i].action == AIAction.ActionStates.crouch)
            {
                GetComponent<g_AISetCoverNode>().SetUpCoverNodes();
                GetComponent<g_AISetCoverNode>().FindCloseCover();
            }
		}
        
    }
	
	// Update is called once per frame
	void Update () 
	{
		currentAction = Actions[actionIndex];
		currentAction.setActionTime ();
	
		if (currentAction.action == AIAction.ActionStates.attackVolley) 
		{
			if (m_currentActionTime < currentAction.actionTime) 
			{
				m_currentActionTime += Time.deltaTime;
                if (hasLineOfSight())
				FireVolley();
			}
			else
			{
				m_currentActionTime = 0;
				for (int i =0; i <Actions.Count; i++)
				{
					if (currentAction.nextActionName == Actions[i].name)
						actionIndex = i;
				}

			}
		}
		else if (currentAction.action == AIAction.ActionStates.crouch)
		{
			if (m_currentActionTime < currentAction.actionTime) 
			{
				m_currentActionTime += Time.deltaTime;
				Crouch();

			}
			else
			{
				m_currentActionTime = 0;

				for (int i =0; i <Actions.Count; i++)
				{
					if (currentAction.nextActionName == Actions[i].name)
						actionIndex = i;
				}			
			}
		}
		else if (currentAction.action ==AIAction.ActionStates.runTo || currentAction.action ==AIAction.ActionStates.crawlTo)
		{
            if (m_coverTarget == null)
            {
                GetComponent<g_AISetCoverNode>().FindCloseCover();
            }
            SetMoveTarget (m_coverTarget.transform.position);
			NavMeshLocomotion();
		} 
		else if (currentAction.action == AIAction.ActionStates.chargePlayer)
		{
			SetMoveTarget(m_attackTarget.transform.position);
			NavMeshLocomotion();
		}
        else if (currentAction.action == AIAction.ActionStates.hoverAndShoot)
        { 
            FireVolley();
        }
	}

	void FireVolley()
	{
		SetAttackTarget ();
        if (animationScript != null)
		animationScript.PlayAttackAnimation ();
		gunScript.Fire ();
	}

    void SetAttackTarget()
    {
        if (GetComponent<g_AIRotateToTarget>() != null)
        GetComponent<g_AIRotateToTarget>().SetTargetTransform(m_attackTarget.transform);
    }

    void Crouch()
	{
        if (animationScript)
		animationScript.PlayCrouchAnimation ();
		SetAttackTarget ();

	}

	void SetMoveTarget(Vector3 movePosition)
	{
		navMesh.SetDestination (movePosition);
	}

	void NavMeshLocomotion()
	{
        if (navMesh.remainingDistance == 0)
        {
            if (animationScript)
                animationScript.PlayRunAnimation();
        }
        else if (navMesh.remainingDistance <= navMesh.stoppingDistance)
        { //need to make sure this doesnt happen until run
            if (animationScript)
            {
                animationScript.StopRunAnimation();
                animationScript.PlayCrouchAnimation();
            }
            for (int i = 0; i < Actions.Count; i++)
            {
                if (currentAction.nextActionName == Actions[i].name)
                {
                    actionIndex = i;
                }
            }
        }
        else
        {
            if (animationScript)
                animationScript.PlayRunAnimation();
        }
	}

    bool hasLineOfSight()
    {
        RaycastHit hit;
        if (Physics.Linecast(GetComponent<g_AIGunScript>().FireTransform.position, player.transform.position, out hit, lineOfSightMask))
        {
            Debug.DrawLine(GetComponent<g_AIGunScript>().FireTransform.position, player.transform.position);
            if (hit.transform.root.name == "Player")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
 
        return false;
    }
}
