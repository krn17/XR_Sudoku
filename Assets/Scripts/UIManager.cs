using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using JMRSDK.Toolkit;
using DG.Tweening;
using JMRSDK.InputModule;

public class UIManager : MonoBehaviour, IBackHandler, IHomeHandler
{

	[Header("===Texts===")]
	public TextMeshProUGUI difficultyText;
	public TextMeshProUGUI timerText;
	public TextMeshProUGUI hintText;
	public TextMeshProUGUI tryAgainText;

	[Header("===Buttons===")]
	public JMRUIPrimaryButton headerPauseBtn;

	[Header("===Win Screen===")]
	public RectTransform winPanel;
	public JMRUIPrimaryButton restartSceneBtn;
	public JMRUIPrimaryButton goHomeBtn;
	public TextMeshProUGUI winTimerText;
	public TextMeshProUGUI winDifficultyText;
	public GameObject[] disableOnGameOver;

	[Header("===Pause Menu===")]
	public RectTransform pauseMenuPanel;
	public JMRUIPrimaryButton resumeBtn;
	public JMRUIPrimaryButton pausedGoHomeBtn;
	public TextMeshProUGUI pausedDifficultyText;
	public TextMeshProUGUI pausedTimerText;
	[HideInInspector]
	public bool pauseMenuOpen = false;
	private SceneLoader _sceneLoader;


	public static UIManager Instance = null;

	private float startTime;
	private Board board;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(this);
		}

		
	}

	public void Start()
	{
		JMRInputManager.Instance.AddGlobalListener(gameObject);
		board = GetComponent<Board>();
		_sceneLoader = GetComponent<SceneLoader>();
		tryAgainText.GetComponent<CanvasGroup>().alpha = 0;
		
		headerPauseBtn.OnClick.AddListener(() => {ShowPauseMenuPanel();});
		restartSceneBtn.OnClick.AddListener(() => {ChangeScene("GameScene");});
		goHomeBtn.OnClick.AddListener(() => {ChangeScene("MenuScreen");});
		resumeBtn.OnClick.AddListener(() => HidePauseMenuPanel());
		pausedGoHomeBtn.OnClick.AddListener(() => {
			Time.timeScale = 1f;
			ChangeScene("MenuScreen");
			});
		
		UpdateHintText();
		difficultyText.text = PlayerPrefs.GetString("game_difficulty", "EASY");

		startTime = Time.time;
	}

	private void Update() {
		UpdateTimer();
	}

	string UpdateTimer() {

		float t = Time.time - startTime;
		string min = ((int)t/60).ToString();
		string sec = (t%60).ToString("f0");
		// string format
		if(min.Length == 1)
			min = "0"+min;
		if(sec.Length == 1)
			sec = "0"+sec;
		timerText.text = min + ":" + sec;

		return min + ":" + sec;
	}



	public void ResetLevel(int _level)
	{
		SceneManager.LoadScene(_level);
	}

	// public void CheckMoved(bool _playerMoved, bool _kingDead)
	// {
	//     if (_playerMoved && !_kingDead)
	//     {
	//         UndoButton.gameObject.SetActive(true);
	//         EndTurnButton.gameObject.SetActive(true);
	//     }
	//     else if (!_playerMoved)
	//     {
	//         UndoButton.gameObject.SetActive(false);
	//         EndTurnButton.gameObject.SetActive(false);
	//     }
	// }


	public void ShowWinPanel() {
		winTimerText.text = UpdateTimer();
		winDifficultyText.text = PlayerPrefs.GetString("game_difficulty", "EASY");
		// calculate and show score
		// int score = gameManager.CalculateScore(startTime, _isWhiteWin);
		// if(score > SaveManager.Load().saveObject.highscore) {
		// 	gameOverScoreText.text = score + " (Highscore!)";
		// 	SaveManager.SaveHighscore(score);
		// }
		// else {
		// 	gameOverScoreText.text = score + "";
		// }
		// update highscore to leaderboard
		// try {
		// 	SaveData saveData = SaveManager.Load();
		// 	if(saveData.saveObject.highscore > 0) {
		// 		HighScores.UploadScore(saveData.saveObject.playerName, saveData.saveObject.highscore);
		// 	}
		// }
		// catch(System.Exception) {
		// 	Debug.Log("Error uploading to leaderboard");
		// }
		// disable on game over
		for(int i = 0; i< disableOnGameOver.Length; i++) {
			disableOnGameOver[i].SetActive(false);
		}
		winPanel.gameObject.SetActive(true);
		winPanel.GetComponent<CanvasGroup>().DOFade(1f, 0.15f).SetUpdate(true);
		winPanel.DOShakeScale(0.3f,0.2f, 20, 90f).SetUpdate(true);
	}

	public void ShowPauseMenuPanel() {
		pauseMenuOpen = true;
		Time.timeScale = 0f;
		pausedTimerText.text = UpdateTimer();
		pausedDifficultyText.text = PlayerPrefs.GetString("game_difficulty", "EASY");
		// disable on pause menu
		for(int i = 0; i< disableOnGameOver.Length; i++) {
			disableOnGameOver[i].SetActive(false);
		}
		pauseMenuPanel.gameObject.SetActive(true);
		pauseMenuPanel.GetComponent<CanvasGroup>().DOFade(1f, 0.15f).SetUpdate(true);
		pauseMenuPanel.DOShakeScale(0.3f,0.2f, 20, 90f).SetUpdate(true);
	}
	public void HidePauseMenuPanel() {
		pauseMenuOpen = false;
		Time.timeScale = 1f;
		// enable on resume
		for(int i = 0; i< disableOnGameOver.Length; i++) {
			disableOnGameOver[i].SetActive(true);
		}
		pauseMenuPanel.GetComponent<CanvasGroup>().alpha = 0f;
		pauseMenuPanel.gameObject.SetActive(false);
	}

	public void ChangeScene(string name) {
		_sceneLoader.LoadSceneAsync(name);
	}

	public void UpdateHintText() {
		string s = "HINT (" + board.GetCurrentHints() + "/" + board.GetMaxHints() + ")";
		hintText.text = s;
	}

	public void ShowTryAgainText() {
		tryAgainText.GetComponent<CanvasGroup>().DOFade(1f, 0.15f).SetUpdate(true).OnComplete(() => {
			tryAgainText.GetComponent<CanvasGroup>().DOFade(0f, 2f);
		});
		tryAgainText.transform.DOShakeScale(0.3f,0.2f, 20, 90f).SetUpdate(true);
	}

	public void OnBackAction()
	{
		if(!pauseMenuOpen)
			ShowPauseMenuPanel();
		else
			HidePauseMenuPanel();
	}

	public void OnHomeAction()
	{
		if(!pauseMenuOpen)
			ShowPauseMenuPanel();
	}
}
