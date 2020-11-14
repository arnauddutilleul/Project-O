using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManagerTech : MonoBehaviour
{
	[Header("What Menu Is Active?")]
	public bool simpleMenu = false;
	public bool advancedMenu = true;

	[Header("Simple Panels")]
	[Tooltip("The UI Panel holding the Home Screen elements")]
	public GameObject homeScreen;
	[Tooltip("The UI Panel holding the credits")]
	public GameObject creditsScreen;
	[Tooltip("The UI Panel holding the settings")]
	public GameObject systemScreen;
	[Tooltip("The UI Panel holding the CANCEL or ACCEPT Options for New Game")]
	public GameObject newGameScreen;
	[Tooltip("The UI Panel holding the YES or NO Options for Load Game")]
	public GameObject loadGameScreen;
	[Tooltip("The Loading Screen holding loading bar")]
	public GameObject loadingScreen;

	[Header("COLORS - Tint")]
	public Image[] panelGraphics;
	public Image[] blurs;
	public Color tint;

	[Header("ADVANCED - Panels")]
	[Tooltip("The UI Panel holding the New Account Screen elements")]
	public GameObject newAccountScreen;
	[Tooltip("The UI Panel holding the Delete Account Screen elements")]
	public GameObject deleteAccountScreen;
	[Tooltip("The UI Panel holding Log-In Buttons")]
	public GameObject loginScreen;
	[Tooltip("The UI Panel holding account and load menu")]
	public GameObject databaseScreen;
	[Tooltip("The UI Menu Bar at the edge of the screen")]
	public GameObject menuBar;

	[Header("ADVANCED - UI Elements & User Data")]
	[Tooltip("The Main Canvas Gameobject")]
	public CanvasScaler mainCanvas;
	[Tooltip("The dropdown menu containing all the resolutions that your game can adapt to")]
	public TMP_Dropdown ResolutionDropDown;
	private Resolution[] resolutions;
	[Tooltip("The text object in the Settings Panel displaying the current quality setting enabled")]
	public TMP_Text qualityText; // text displaying current selected quality
	[Tooltip("The icon showing the current quality selected in the Settings Panels")]
	public Animator qualityDisplay;
	private string[] qualityNames;
	private int tempQualityLevel;// store it for start up text update
	[Tooltip("The volume slider UI element in the Settings Screen")]
	public Slider audioSlider;

	[Tooltip("If a message is displaying indiciating FAILURE, this is the color of that error text")]
	public Color errorColor;
	[Tooltip("If a message is displaying indiciating SUCCESS, this is the color of that success text")]
	public Color successColor;
	public float messageDisplayLength = 2.0f;
	public Slider uiScaleSlider;
	float xScale = 0f;
	float yScale = 0f;

	[Header("Menu Bar")]
	public bool showMenuBar = true;
	[Tooltip("The Arrow at the corner of the screen activating and de-activating the menu bar")]
	public GameObject menuBarButton;
	[Tooltip("The date and time display text at the bottom of the screen")]
	public TMP_Text dateDisplay;
	public TMP_Text timeDisplay;
	public bool showDate = true;
	public bool showTime = true;

	[Header("Loading Screen Elements")]
	[Tooltip("The name of the scene loaded when a 'NEW GAME' is started")]
	public string newSceneName;
	[Tooltip("The loading bar Slider UI element in the Loading Screen")]
	public Slider loadingBar;
	private string loadSceneName; // scene name is defined when the load game data is retrieved

	[Header("Settings Screen")]
	public TMP_Text textSpeakers;
	public TMP_Text textSubtitleLanguage;
	public List<string> speakers = new List<string>();
	public List<string> subtitleLanguage = new List<string>();

	[Header("Starting Options Values")]
	public int speakersDefault = 0;
	public int subtitleLanguageDefault = 0;

	[Header("List Indexing")]
	int speakersIndex = 0;
	int subtitleLanguageIndex = 0;

	[Header("Debug")]
	[Tooltip("If this is true, pressing 'R' will reload the scene.")]
	public bool reloadSceneButton = true;
	Transform tempParent;

	public void MoveToFront(GameObject currentObj){
		//tempParent = currentObj.transform.parent;
		tempParent = currentObj.transform;
		tempParent.SetAsLastSibling();
	}

	void Start(){
		// By default, starts on the home screen, disables others
		homeScreen.SetActive(true);
		if(newAccountScreen != null)
		newAccountScreen.SetActive(false);
		if(deleteAccountScreen != null)
		deleteAccountScreen.SetActive(false);
		if(loginScreen != null)
		loginScreen.SetActive(false);
		if(databaseScreen != null)
		databaseScreen.SetActive(false);
		if(creditsScreen != null)
		creditsScreen.SetActive(false);
		if(systemScreen != null)
		systemScreen.SetActive(false);
		if(loadingScreen != null)
		loadingScreen.SetActive(false);
		if(loadGameScreen != null)
		loadGameScreen.SetActive(false);
		if(newGameScreen != null)
		newGameScreen.SetActive(false);

		string m_Path;

		if(advancedMenu)
		{
			// Set Save Path to local
			m_Path = Application.dataPath;
		}

		if(menuBar != null){
			if(!showMenuBar){
				menuBar.gameObject.SetActive(false);
				menuBarButton.gameObject.SetActive(false);
			}
		}

		// Set Colors if the user didn't before play
		for(int i = 0; i < panelGraphics.Length; i++)
        {
           panelGraphics[i].color = tint;
        }
		for(int i = 0; i < blurs.Length; i++)
        {
           blurs[i].material.SetColor("_Color",tint);
        }

		// Get quality settings names
		qualityNames = QualitySettings.names;

		// Get screens possible resolutions
		resolutions = Screen.resolutions;

		// Set Drop Down resolution options according to possible screen resolutions of your monitor
		if(ResolutionDropDown != null){
		for (int i = 0; i < resolutions.Length; i++){
				ResolutionDropDown.options.Add (new TMP_Dropdown.OptionData (ResToString (resolutions [i])));
	
				ResolutionDropDown.value = i;
	
				ResolutionDropDown.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions
				[ResolutionDropDown.value].width, resolutions[ResolutionDropDown.value].height, true);});
			}
		}
		 
		 // Check if first time so the volume can be set to MAX
		 if(PlayerPrefs.GetInt("firsttime")==0){
			 // it's the player's first time. Set to false now...
			 PlayerPrefs.SetInt("firsttime",1);
			 PlayerPrefs.SetFloat("volume",1);
		 }

		 // Check volume that was saved from last play
		 if(audioSlider != null)
		 audioSlider.value = PlayerPrefs.GetFloat("volume");

		// Settings screen
		speakersIndex = speakersDefault;
		subtitleLanguageIndex = subtitleLanguageDefault;

		textSpeakers.text = speakers[speakersDefault];
		textSubtitleLanguage.text = subtitleLanguage[subtitleLanguageDefault];
	}

	public void IncreaseIndex(int i){
		switch (i){
			case 0:
				if(speakersIndex != speakers.Count -1){speakersIndex++;}else{speakersIndex = 0;}
				textSpeakers.text = speakers[speakersIndex];
				break;
			case 1:
				if(subtitleLanguageIndex != subtitleLanguage.Count -1){subtitleLanguageIndex++;}else{subtitleLanguageIndex = 0;}
				textSubtitleLanguage.text = subtitleLanguage[subtitleLanguageIndex];
				break;
			default:
				break;
		}
	}

	public void DecreaseIndex(int i){
		switch (i){
			case 0:
				if(speakersIndex == 0){speakersIndex = speakers.Count;}speakersIndex--;
				textSpeakers.text = speakers[speakersIndex];
				break;
			case 1:
				if(subtitleLanguageIndex == 0){subtitleLanguageIndex = subtitleLanguage.Count;}subtitleLanguageIndex--;
				textSubtitleLanguage.text = subtitleLanguage[subtitleLanguageIndex];
				break;
			default:
				break;
		}
	}

	public void UIScaler(){
		xScale = 1920 * uiScaleSlider.value;
		yScale = 1080 * uiScaleSlider.value;
		mainCanvas.referenceResolution = new Vector2 (xScale,yScale);
	}

	// Converts the resolution into a string form that is then used in the dropdown list as the options
	string ResToString(Resolution res)
	{
		return res.width + " x " + res.height;
	}

	// Whenever a value on the audio slider in the settings panel is changed, this 
	// function is called and updated the overall game volume
	public void AudioSlider(){
		AudioListener.volume = audioSlider.value;
		PlayerPrefs.SetFloat("volume",audioSlider.value);
	}

	// When accepting the QUIT question, the application will close 
	// (Only works in Executable. Disabled in Editor)
	public void Quit(){
		Application.Quit();
	}

	// Called when loading new game scene
	public void LoadNewLevel (){
		if(newSceneName != ""){
			StartCoroutine(LoadAsynchronously(newSceneName));
		}
	}

	// Called when loading saved scene
	// Add the save code in this function!
	public void LoadSavedLevel (){
		if(loadSceneName != ""){
			StartCoroutine(LoadAsynchronously(newSceneName)); // temporarily uses New Scene Name. Change this to 'loadSceneName' when you program the save data
		}
	}

	// Load Bar synching animation
	IEnumerator LoadAsynchronously (string sceneName){ // scene name is just the name of the current scene being loaded
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

		while (!operation.isDone){
			float progress = Mathf.Clamp01(operation.progress / .9f);
			
			loadingBar.value = progress;

			yield return null;
		}
	}
	
}
