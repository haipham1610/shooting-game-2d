using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioSource effectAudioSource;
	[SerializeField] private AudioSource defaultMusic;
	[SerializeField] private AudioSource bossMusic;
	[SerializeField] private AudioClip shootClip;

	public void PlayShootSound()
	{
		effectAudioSource.PlayOneShot(shootClip);
	}
	
	public void PlayBossAudio()
	{
		bossMusic.Play();
		defaultMusic.Stop();
	}

	public void StopAudio()
	{
		bossMusic.Stop();
		defaultMusic.Stop();
		effectAudioSource.Stop();
	}

	public void PlayDefaultAudio()
	{
		bossMusic.Stop();
		defaultMusic.Play();
	}
}
