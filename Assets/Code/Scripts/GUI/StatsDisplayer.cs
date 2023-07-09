using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplayer : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI hpText;
	[SerializeField]
	private TextMeshProUGUI maxHpText;

	[SerializeField]
	private Slider hpSlider;
	
	// [SerializeField]
	// private TextMeshProUGUI strText;
	[SerializeField]
	private TextMeshProUGUI nameText;
	// [SerializeField]
	// private TextMeshProUGUI postText;
	[SerializeField]
	private TextMeshProUGUI armorText;

	public void Display(FighterStats stats)
	{
		if (stats.HP == null)
		{
			stats.InitCopy();
		}

		hpSlider.value = stats.HP.Value / stats.MaxHP;
		
		hpText.text = $"{stats.HP.Value} / {stats.MaxHP}";
		// maxHpText.text = stats.MaxHP.ToString();
		// strText.text = stats.Strength.Value.ToString();
		// nameText.text = stats.FighterName;
		// postText.text = stats.Posture.Value.ToString();
		armorText.text = stats.Armor.Value.ToString();
	}
}