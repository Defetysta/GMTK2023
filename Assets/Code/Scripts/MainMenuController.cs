using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	private const string TUTORIAL_SCENE_NAME = "TutorialScene";
	
	[SerializeField]
	private Button startGameButton;
	[SerializeField]
	private Button settingsButton;
	[SerializeField]
	private Button creditsView;
	[SerializeField]
	private Button quitGameButton;
	
	[SerializeField]
	private SettingsView settingsView;
	[SerializeField]
	private GameObject creditsViewObject;

	private void Awake()
	{
		startGameButton.onClick.AddListener(StarGame);
		quitGameButton.onClick.AddListener(QuitGame);
		settingsButton.onClick.AddListener(OpenSettings);
		creditsView.onClick.AddListener( () => creditsViewObject.SetActive(true));
	}

	private void OnDestroy()
	{
		startGameButton.onClick.RemoveAllListeners();
		quitGameButton.onClick.RemoveAllListeners();
		settingsButton.onClick.RemoveAllListeners();
		creditsView.onClick.RemoveAllListeners();
	}

	private void StarGame()
	{
		SceneManager.LoadScene(TUTORIAL_SCENE_NAME);
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