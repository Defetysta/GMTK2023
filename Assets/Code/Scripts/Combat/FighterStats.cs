using System;
using UnityEngine;

[CreateAssetMenu(menuName="Fighters/Default", fileName = "Assets/ScriptableObjects/Fighters/Default")]
public class FighterStats : ScriptableObject
{
	[SerializeField]
	private string fighterName;
	[SerializeField]
	private FloatValue myHP;
	[SerializeField]
	private FloatValue myArmor;
	[SerializeField]
	private FloatValue myStrength;
	[SerializeField]
	private FloatValue myPosture;

	private float maxHP;

	public string FighterName => fighterName;
	public FloatValue HP => myHP;
	public FloatValue Armor => myArmor;
	public FloatValue Strength => myStrength;
	public FloatValue Posture => myPosture;

	public float MaxHP => maxHP;

	private void OnEnable()
	{
		maxHP = HP.Value;
	}
}