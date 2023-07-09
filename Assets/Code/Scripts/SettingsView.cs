using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
	[SerializeField]
	private TMP_Dropdown resolutionsDropdown;

	[SerializeField]
	private Toggle fullscreenToggle;
	
	
	private List<(int, int)> resolutions = new() { (1024, 768), (1280, 720), (1366, 768), (1920, 1080), (2560, 1440) };

	private void Awake()
	{
		resolutionsDropdown.onValueChanged.AddListener(SetScreenSize);
		fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
	}

	private void OnDestroy()
	{
		resolutionsDropdown.onValueChanged.RemoveAllListeners();
		fullscreenToggle.onValueChanged.RemoveAllListeners();
	}

	private void SetScreenSize(int index)
	{
		bool isFullscreen = Screen.fullScreen;

		var desiredResolution = resolutions[index];
		Screen.SetResolution(desiredResolution.Item1, desiredResolution.Item2, isFullscreen);
	}

	private void SetFullscreen(bool desiredValue)
	{
		Screen.fullScreen = desiredValue;
	}
}