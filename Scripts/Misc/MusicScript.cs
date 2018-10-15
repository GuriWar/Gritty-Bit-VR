using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour {
	[SerializeField]
	AudioClip[] music;
	// Use this for initialization
	void Start () 
	{
		GetComponent<AudioSource>().clip = (music[Random.Range(0,music.Length)]);
		GetComponent<AudioSource>().Play();
	}
}
