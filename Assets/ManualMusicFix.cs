using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualMusicFix : MonoBehaviour {
public AudioClip[] musics;

void Start()
{
	GetComponent<AudioSource>().clip = musics[UnityEngine.Random.Range(0, musics.Length)];
}
void Update()
{
	if(!GetComponent<AudioSource>().isPlaying)
	GetComponent<AudioSource>().Play();
}
}
