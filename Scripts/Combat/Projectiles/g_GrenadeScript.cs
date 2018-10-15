using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
 * This script aims the grenade and launches it.
 *  It will also warn other agents of itself when it lands, usually prompting them to move out of the way.
 * */

    public class g_GrenadeScript : MonoBehaviour
    {
        //Timer starts after the grenade hits the ground
        public float timeTilExplode = 3;
        public GameObject explosion;

        private Vector3 target;

        //This was to give the grenade consistant force instead of adjusting the force and throwing at a 45 degree angle
        //var alterAngle : boolean = false;	
        public float warningRadius = 7;
        public float timeUntilWarningCanBeGiven = 1;

        private bool hasTarget = false;


        //public bool makeSureWeDontGoThroughObjects = true;
        //private Vector3 lastPos;
        public LayerMask layerMask;

        private bool warned = false;
        private bool canBeWarnedYet = false;

        private Rigidbody myRigidBody;

        public float runAwayBuffer = 3;

		void Start()

		{
			Go();
		}

        void Go()
        {
            float throwForce = 0;
            //Aims grenade at Target
            if (hasTarget)
            {
                float xDist = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z));
                float yDist = -(transform.position.y - target.y);

                //Calculate force required
                throwForce = xDist / (Mathf.Sqrt(Mathf.Abs((yDist - xDist) / (0.5f * (-Physics.gravity.y)))));
                throwForce = 1.414f * throwForce / Time.fixedDeltaTime * GetComponent<Rigidbody>().mass;

                //Always fire on a 45 degree angle, this makes it easier to calculate the force required.
                transform.Rotate(-45, 0, 0);
            }
            myRigidBody = GetComponent<Rigidbody>();
            myRigidBody.AddRelativeForce(Vector3.forward * throwForce);

            //Start the time on the grenade
            StartCoroutine(StartDetonationTimer());
            //Because the grenade may skim the colliders of the agent before detonating, we want to wait a moment or two before being able to "warn" agents of the grenade
            //Warning will cause surrounding agents to attempt to escape from the grenade.
            StartCoroutine(SetTimeUntilWarning()); 	
        }

		void CustomGo()
		{
			float throwForce = 0;
			//Aims grenade at Target
			if (hasTarget)
			{
				float xDist = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z));
				float yDist = -(transform.position.y - target.y);

				//Calculate force required
				throwForce = xDist / (Mathf.Sqrt(Mathf.Abs((yDist - xDist) / (0.5f * (-Physics.gravity.y)))));
				throwForce = .7f * throwForce / Time.fixedDeltaTime * GetComponent<Rigidbody>().mass;
				//find direction to target
				Vector3 dir2Tar = (target - transform.position);

				//these both should be direction vectors
				float angle = Vector3.Angle ((transform.forward), new Vector3 (dir2Tar.x, 0, dir2Tar.z));
				//print (angle);
				//need to do cross product with vector.up
				//Always fire on a 45 degree angle, this makes it easier to calculate the force required.
				transform.Rotate(45, angle, 0); // just rotate the grenades in an angle towards the player, in the y
			}
			myRigidBody = GetComponent<Rigidbody>();
			myRigidBody.AddRelativeForce(Vector3.forward * throwForce);

			//Start the time on the grenade
			StartCoroutine(StartDetonationTimer());
			//Because the grenade may skim the colliders of the agent before detonating, we want to wait a moment or two before being able to "warn" agents of the grenade
			//Warning will cause surrounding agents to attempt to escape from the grenade.
			StartCoroutine(SetTimeUntilWarning()); 	
		}
		

        void DetonateGrenade()
        {
            if (explosion)
                Instantiate(explosion, transform.position, transform.rotation);
            else
                Debug.LogWarning("No explosion object assigned to grenade!");

            Destroy(gameObject);
        }

        IEnumerator StartDetonationTimer()
        {
            yield return new WaitForSeconds(timeTilExplode);
            DetonateGrenade();
        }

        IEnumerator SetTimeUntilWarning()
        {
            yield return new WaitForSeconds(timeUntilWarningCanBeGiven);
            canBeWarnedYet = true;
        }

		public void SetTarget(Vector3 pos, bool custom)
        {
            target = pos;
            hasTarget = true;

			if (custom) {
				CustomGo ();
			} 
			else 
			{
				Go();
			}
        }
    }
