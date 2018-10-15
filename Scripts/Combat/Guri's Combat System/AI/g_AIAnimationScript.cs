using UnityEngine;
using System.Collections;

public class g_AIAnimationScript : MonoBehaviour 
{
	Animator animator;
	int attackingHash;
	int crouchingHash;
	int runningHash;
	int shotHash;
	int dieHash;
	bool isRunning;
	bool doShotAnimation;
	float shotAnimationTime = 0.5f;
	float currentShotAnimationTime;
	void SetHashes()
	{
		attackingHash = Animator.StringToHash("Attack");
		crouchingHash = Animator.StringToHash("Crouching");
		runningHash = Animator.StringToHash("Running");
		shotHash = Animator.StringToHash("GotShot");
		dieHash = Animator.StringToHash("Die");
	}
	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator> ();
		SetHashes ();
	}

	void Update()
	{
		if (doShotAnimation)
		{
			currentShotAnimationTime += Time.deltaTime;
			if (currentShotAnimationTime >= shotAnimationTime)
			{
				doShotAnimation = false;
				currentShotAnimationTime = 0;
				StopGotShotAnimation();

			}
			//check if animation is done playing and then set the variable to false so we can switchback to
			//shooting animation

		}
	}


	public void PlayRunAnimation()
	{
		animator.SetBool (runningHash, true);
	}

	public void PlayAttackAnimation()
	{
		animator.SetBool (crouchingHash, false);
		animator.SetTrigger (attackingHash);
	}

	public void PlayCrouchAnimation()
	{
		animator.SetBool (crouchingHash, true);
	}

	public void PlayGotShotAnimation()
	{
		doShotAnimation = true;
		animator.SetBool(shotHash, doShotAnimation);
	}

	public void PlayDieAnimation()
	{
		animator.SetTrigger(dieHash);
	}

	public void StopRunAnimation()
	{
		animator.SetBool (runningHash, false);
	}
		
	public void StopCrouchAnimation()
	{
		animator.SetBool (crouchingHash, false);
	}

	public void StopGotShotAnimation()
	{
		animator.SetBool(shotHash, doShotAnimation);
	}

}
