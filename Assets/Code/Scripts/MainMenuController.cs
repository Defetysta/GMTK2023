using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	private const string GAME_SCENE_NAME = "GameScene";
	
	[SerializeField]
	private Button startGameButton;
	[SerializeField]
	private Button settingsButton;
	[SerializeField]
	private Button quitGameButton;
	
	[SerializeField]
	private SettingsView settingsView;

	private void Awake()
	{
		startGameButton.onClick.AddListener(StarGame);
		quitGameButton.onClick.AddListener(QuitGame);
		settingsButton.onClick.AddListener(OpenSettings);
	}

	private void OnDestroy()
	{
		startGameButton.onClick.RemoveAllListeners();
		quitGameButton.onClick.RemoveAllListeners();
	}

	private void StarGame()
	{
		SceneManager.LoadScene(GAME_SCENE_NAME);
	}

	private void QuitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	private void OpenSettings()
	{
		settingsView.gameObject.SetActive(true);
	}
}