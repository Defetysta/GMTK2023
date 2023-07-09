using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TutorialSceneHandler : MonoBehaviour
{
	private const string GAME_SCENE_NAME = "GameScene";

	[SerializeField]
	private GameObject proceedButtonObject;

	private void Awake()
	{
		StartCoroutine(DelayEnablingButton());
	}

	private IEnumerator DelayEnablingButton()
	{
		yield return new WaitForSeconds(3f);
		
		proceedButtonObject.SetActive(true);
	}

	public void ProceedToGame()
	{
		SceneManager.LoadScene(GAME_SCENE_NAME);
	}
}