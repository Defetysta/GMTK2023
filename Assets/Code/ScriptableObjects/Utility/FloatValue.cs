using System;
using UnityEngine;

[CreateAssetMenu(menuName="Variables/Float", fileName = "Assets/ScriptableObjects/Variables/FloatValue")]
public class FloatValue : ScriptableObject
{
	[SerializeField]
	private float defaultValue;

	[SerializeField]
	private bool resetToDefault;

	private float changingValue;

	private void OnEnable()
	{
		changingValue = defaultValue;
	}

	public float Value
	{
		get
		{
			return resetToDefault ? changingValue : defaultValue;
		}
		
		set
		{
			if (resetToDefault)
			{
				changingValue = value;
			}
			else
			{
				defaultValue = value;
			}
		}
	}
}