using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
	private const string GAME_SCENE_NAME = "GameScene";
	private const string MENU_SCENE_NAME = "MainMenu";
	
	public void ReloadGameScene()
	{
		SceneManager.LoadScene(GAME_SCENE_NAME);
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene(MENU_SCENE_NAME);
	}
}