using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName="Audio Events/Simple")]
public class SimpleAudioEvent : AudioEvent
{
	public AudioClip[] clips;

	public RangedFloat volume;

	[MinMaxRange(0, 2)]
	public RangedFloat pitch;

	private AudioSource cachedGlobalSource;

	public void Play()
	{
		if (cachedGlobalSource == null)
		{
			cachedGlobalSource = GameObject.FindGameObjectWithTag(GLOGAL_SFX_SOURCE).GetComponent<AudioSource>();
		}
		
		Play(cachedGlobalSource);
	}

	public override void Play(AudioSource source)
	{
		if (clips.Length == 0) return;

		source.clip = clips[Random.Range(0, clips.Length)];
		source.volume = Random.Range(volume.MinValue, volume.MaxValue);
		source.pitch = Random.Range(pitch.MinValue, pitch.MaxValue);
		source.Play();
	}
}