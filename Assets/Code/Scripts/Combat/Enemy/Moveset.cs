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
		private List<CardBase> actions;

		[NonSerialized]
		private List<CardBase> actionsCopy;

		public void InitCopy()
		{
			actionsCopy = new List<CardBase>(actions);

			for (int i = 0; i < actionsCopy.Count; i++)
			{
				actionsCopy[i] = Instantiate(actions[i]);
			}
		}
		public CardBase[] Actions => actionsCopy.ToArray();
	}
	
	[SerializeField]
	private MovesGroup[] moves;

	public void InitCopy()
	{
		foreach (MovesGroup movesGroup in moves)
		{
			movesGroup.InitCopy();
		}
	}
	public MovesGroup[] Moves => moves;
}