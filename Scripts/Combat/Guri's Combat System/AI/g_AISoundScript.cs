using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class g_AISoundScript : MonoBehaviour {
	AudioSource audioSource;
	[SerializeField]
	List<AudioClip> deathSound = new List<AudioClip>();
	[SerializeField]
	AudioClip painSound;
	[SerializeField]
	AudioClip attackSound;
	// Use this for initialization
	void Start () 
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void PlayDeathSound()
	{
		if (deathSound.Count > 0)
		{
            int soundIndex = Random.Range(0, deathSound.Count);
            audioSource.clip = deathSound[soundIndex];
		    audioSource.Play();
		}
	}

	public void PlayPainSound()
	{
	//	audioSource.clip = painSound;
	//	audioSource.Play();
	}

	public void PlayAttackSound()
	{
		if (attackSound)
		{
		audioSource.clip = attackSound;
		audioSource.Play ();
		}
	}
}
