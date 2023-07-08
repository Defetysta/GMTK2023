using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Moveset/Empty", fileName = "Assets/ScriptableObjects/Movesets/Empty")]
public class Moveset : ScriptableObject
{
	[Serializable]
	public class MovesGroup
	{
		[SerializeField]
		private CardBase[] actions;

		public CardBase[] Actions => actions;
	}
	
	[SerializeField]
	private MovesGroup[] moves;

	public MovesGroup[] Moves => moves;
}