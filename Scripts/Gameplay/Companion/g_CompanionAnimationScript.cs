using UnityEngine;
using System.Collections;

public class g_CompanionAnimationScript : MonoBehaviour 
{
	Animator animator;
	int attackingHash;
	int battleIdleHash;
	int relaxTakeDamageHash;
	int battleTakeDamageHash;
	int relaxDieHash;
	int battleDieHash;
	bool doShotAnimation;
	float shotAnimationTime = 0.5f;
	float currentShotAnimationTime;
	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator> ();
		SetHashes();
	}

	void SetHashes()
	{
		battleIdleHash = Animator.StringToHash("Battle Idle");
		attackingHash = Animator.StringToHash("Battle Shoot Attack");
		relaxTakeDamageHash = Animator.StringToHash("Relax Take Damage");
		battleTakeDamageHash = Animator.StringToHash("Battle Take Damage");
		relaxDieHash = Animator.StringToHash("Relax Die");
		battleDieHash = Animator.StringToHash("Battle Die");

	}
		

	public void PlayAttackAnimation()
	{
		animator.SetBool (attackingHash, true);
	}

	public void PlayBattleIdleAnimation()
	{
		animator.SetBool (battleIdleHash, true);
	}

	public void PlayRelaxTakeDamageAnimation()
	{
		animator.SetTrigger (relaxTakeDamageHash);
	}

	public void PlayBattleTakeDamageAnimation()
	{
		animator.SetTrigger(battleTakeDamageHash);
	}

	public void PlayRelaxDie()
	{
		animator.SetTrigger(relaxDieHash);
	}

	public void PlayBattleDie()
	{
		animator.SetTrigger(battleDieHash);
	}

	public void StopAttackAnimation()
	{
		animator.SetBool (attackingHash, false);
	}

	public void StopBattleIdleAnimation()
	{
		animator.SetBool (battleIdleHash, false);
	}

}
