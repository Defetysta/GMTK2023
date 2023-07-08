using UnityEngine;

[CreateAssetMenu(menuName="Enemy/Empty", fileName = "Assets/ScriptableObjects/Enemies/Empty")]
public class Enemy : ScriptableObject
{
	[SerializeField]
	private Moveset moveset;

	[SerializeField]
	private FighterStats stats;

	private Moveset movesetCopy;
	private FighterStats statsCopy;

	public void InitCopy()
	{
		movesetCopy = Instantiate(moveset);
		movesetCopy.InitCopy();
		statsCopy = Instantiate(stats);
		statsCopy.InitCopy();
	}
	public Moveset Moveset => movesetCopy;

	public FighterStats Stats => statsCopy;
}