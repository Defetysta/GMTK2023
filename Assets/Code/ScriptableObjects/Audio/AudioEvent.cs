using UnityEngine;

public abstract class AudioEvent : ScriptableObject
{
	public const string GLOGAL_SFX_SOURCE = "SfxAudioSource";
	public abstract void Play(AudioSource source);
}