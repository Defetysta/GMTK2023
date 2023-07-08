using TMPro;
using UnityEngine;

public class StatsDisplayer : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI hpText;
	[SerializeField]
	private TextMeshProUGUI strText;
	[SerializeField]
	private TextMeshProUGUI nameText;
	[SerializeField]
	private TextMeshProUGUI postText;
	[SerializeField]
	private TextMeshProUGUI armorText;

	public void Display(FighterStats stats)
	{
		hpText.text = stats.HP.Value.ToString();
		strText.text = stats.Strength.Value.ToString();
		nameText.text = stats.FighterName;
		postText.text = stats.Posture.Value.ToString();
		armorText.text = stats.Armor.Value.ToString();
	}
}