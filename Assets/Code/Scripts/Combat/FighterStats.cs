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

	public string FighterName => fighterNameCopy;
	public FloatValue HP => myHpCopy;
	public FloatValue Armor => myArmorCopy;
	public FloatValue Strength => myStrengthCopy;
	public FloatValue Posture => myPostureCopy;

	private float maxHP;
	public float MaxHP => maxHP;
	private string fighterNameCopy;
	
	[NonSerialized]
	public FloatValue myHpCopy;
	[NonSerialized]
	public FloatValue myArmorCopy;
	[NonSerialized]
	public FloatValue myStrengthCopy;
	[NonSerialized]
	public FloatValue myPostureCopy;
	
	public int ParalysisDuration { get; set; }


	public void InitCopy()
	{
		if (HP != null)
		{
			return;
		}
		
		maxHP = Instantiate(myHP).Value;
		fighterNameCopy = fighterName;
		myHpCopy = Instantiate(myHP);
		myArmorCopy = Instantiate(myArmor);
		myStrengthCopy = Instantiate(myStrength);
		myPostureCopy = Instantiate(myPosture);
	}
}