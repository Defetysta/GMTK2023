using UnityEngine;

[CreateAssetMenu(menuName="Enemy/Empty", fileName = "Assets/ScriptableObjects/Enemies/Empty")]
public class Enemy : ScriptableObject
{
	[SerializeField]
	private Moveset moveset;

	[SerializeField]
	private FighterStats stats;

	public Moveset Moveset => moveset;

	public FighterStats Stats => stats;
}