using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingManager : MonoBehaviour {
	public Toggle fullscreenToggle;
	public Dropdown resolutionDropdown;	
	public Dropdown textureQualityDropdown;
	public Dropdown antiAliasingDropdown;
	public Dropdown vSyncDropdown;
	public Slider musicVolumeSlider;
	public Button applyButton;

	public AudioSource volumeSource;
	public Resolution[] resolutions;
	public GameSettings gameSettings;

	void OnEnable() {
		gameSettings = new GameSettings();
		
		fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
		resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
		textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
		antiAliasingDropdown.onValueChanged.AddListener(delegate { OnAntiAliasingChange(); });
		vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
		musicVolumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });
		applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });

		resolutions = Screen.resolutions;
		foreach (Resolution resolution in resolutions)
		{
			resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
		}

		LoadSettings();
	}

	public void OnFullscreenToggle() {
		gameSettings.fullScreen = Screen.fullScreen = fullscreenToggle.isOn;
		Debug.Log("Fullscreen?");
	}

	public void OnResolutionChange() {
		Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
		gameSettings.resolutionIndex = resolutionDropdown.value;
	}
	public void OnTextureQualityChange() {
	QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDropdown.value;
		
	}
	
	public void OnAntiAliasingChange() {
		QualitySettings.antiAliasing = (int)Mathf.Pow(2, antiAliasingDropdown.value);
	
	}

	public void OnVSyncChange() {
		QualitySettings.vSyncCount = gameSettings.vSync = vSyncDropdown.value;
	}

	public void OnVolumeChange() {
		volumeSource.volume = gameSettings.musicVolume = musicVolumeSlider.value;
	}

	public void OnApplyButtonClick() {
		SaveSettings();
	}


	public void SaveSettings(){
		string jsonData = JsonUtility.ToJson(gameSettings, true);
		File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
	}

	public void LoadSettings(){
         gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json")); 
			musicVolumeSlider.value = gameSettings.musicVolume;
			antiAliasingDropdown.value = gameSettings.antiAliasing;
			vSyncDropdown.value = gameSettings.vSync;
			textureQualityDropdown.value = gameSettings.textureQuality;
			resolutionDropdown.value = gameSettings.resolutionIndex;
			fullscreenToggle.isOn = gameSettings.fullScreen;
			Screen.fullScreen = gameSettings.fullScreen;

		resolutionDropdown.RefreshShownValue();
	}

}
