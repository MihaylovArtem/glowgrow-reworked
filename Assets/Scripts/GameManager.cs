using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	const float scaleDuration = 2.0f;
	float scaleTimer = 0.0f;
	Vector3 menuScale = new Vector3(1,1,1);
	Vector3 gameScale = new Vector3(7,7,7);

	Vector3 targetScale = new Vector3(1,1,1);
	Vector3 originalScale = new Vector3(1,1,1);

	public enum GameState {
		Menu, Playing, Pause, GameOver
	}

	static public GameState gameState = GameState.Menu;

	public Player player;
	private BulletPatternManager patternManager;
	public PalleteManager palleteManager;
	public RequestManager requestManager;
	public Text scoreText;
	public Text superPowerText;
	public Text levelText;
	public Text highscoreText;
	public InputField usernameField;
	public int currentScore = 0;
	private int bulletsForPower = 0;
	private const int maxBulletsForPower = 10;
	public GameObject menuPanel;

	public SpriteRenderer backgroundSprite;
	public Image[] panelBackgroundSprites;

    private Vector3 gravity;

	private string highscorePrefsKey = "current_highscore";
	private int highscore {
		get {
			var saved = PlayerPrefs.GetInt (highscorePrefsKey);
			return saved;
		}
	}

	private string usernamePrefsKey = "username";
	private string username {
		get {
			var saved = PlayerPrefs.GetString (usernamePrefsKey);
			return saved;
		}
	}

	private Color lastBackgroundColor {
		get {
			return PalleteManager.previousPallete.backgroundColor;
		}
	}
	private Color lastPanelBackgroundColor {
		get {
			return PalleteManager.previousPallete.backgroundColorSecondary;
		}
	}
	private Color backgroundExpectedColor {
		get {
			return PalleteManager.currentPallete.backgroundColor;
		}
	}
	private Color panelBackgroundExpectedColor {
		get {
			return PalleteManager.currentPallete.backgroundColorSecondary;
		}
	}

	public int levelNum = 1;

	public double bulletSpeed {
		get {
			return levelNum <=10 ? 1*levelNum/10 : 2;
		}
	}

	// Use this for initialization
	void Start() {
		Player.OnRightBulletCatch += IncreaseScore;
        Player.OnRightBonusBulletCatch += BonusIncreaseScore;
		Player.OnRightBulletCatch += IncreaseBulletsForPower;
		Player.OnWrongBulletCatch += DecreaseScore;
		Player.OnMaxBulletCountCatch += PerformNextLevel;
		Player.OnGameOver += PerformGameOver;
		Player.OnSuperPower += PerformSuperPower;
		Player.OnSuperPowerBulletCatch += IncreaseScore;

		usernameField.onValueChanged.AddListener (delegate {SaveUsername ();});
		usernameField.text = username;

		backgroundSprite.color = backgroundExpectedColor;
		SetPanelBackgroundColorTo(panelBackgroundExpectedColor);

        Physics2D.gravity = new Vector3(1.0f, -1.0f, 0f);
	}

	void SaveUsername() {
		PlayerPrefs.SetString (usernamePrefsKey, usernameField.text);
	}

	// Update is called once per frame
	void FixedUpdate () {
		CheckInput ();
		HandleCurrentColor ();
	}

	void IncreaseScore() {
		currentScore+=10;
	}

    void BonusIncreaseScore(int score) {
        currentScore += score;
    }
	void IncreaseBulletsForPower() {
		if (bulletsForPower < maxBulletsForPower) {
			bulletsForPower++;
		}
	}

	void DecreaseScore() {
		currentScore-=10;
		bulletsForPower = 0;
	}

	void PerformNextLevel() {
		palleteManager.ChangePallete ();
		DestroyAllBullets ();
		levelNum++;
	}

	void PerformGameOver() {
		gameState = GameState.GameOver;
		if (highscore < currentScore) {
			PlayerPrefs.SetInt ("current_highscore", currentScore);
			requestManager.PostHighscoreWithUsername ();
			requestManager.GetRequest ();
		}
		DestroyAllBullets ();
	}

	void DestroyAllBullets() {
		GameObject[] AllBullets = GameObject.FindGameObjectsWithTag("Bullet");
		foreach (GameObject bullet in AllBullets) {
			bullet.GetComponent<Bullet>().DestroySelfWithExplosion();
		}
	}

	void PerformSuperPower() {
		bulletsForPower = 0;
	}

	void StartGame() {
		targetScale = gameScale;
		originalScale = menuPanel.transform.localScale;

		scaleTimer = 0.0f;
		gameState = GameState.Playing;
		patternManager = gameObject.GetComponent<BulletPatternManager>();

		Invoke("startTestPattern", 1.0f);

		currentScore = 0;
		bulletsForPower = 0;
		levelNum = 1;
	}

	void startTestPattern() {
		patternManager.SpawnTestPattern();
	}

	void ToMainMenu() {
		targetScale = menuScale;
		originalScale = menuPanel.transform.localScale;

		scaleTimer = 0.0f;
		gameState = GameState.Menu;
	}

	void OnGUI() {
		if (scaleTimer < scaleDuration) {
			scaleTimer += Time.deltaTime;
		}
		if (gameState == GameState.GameOver) {
			levelText.text = "You lost. Tap to return.";
		} else if (gameState == GameState.Playing) {
			superPowerText.text = "Superpower " + bulletsForPower.ToString() + " / " + maxBulletsForPower.ToString();
			scoreText.text = currentScore.ToString() + " pts";
			levelText.text = "Level " + levelNum.ToString ();
		}
		menuPanel.transform.localScale = Vector3.Lerp (originalScale, targetScale, scaleTimer / scaleDuration);

		highscoreText.text = "My highscore:\n" + highscore.ToString () + " points";
	}

	void HandleCurrentColor() {
		if (PalleteManager.isColorChangeInProgress) {
			SetPanelBackgroundColorTo(Color.Lerp (lastPanelBackgroundColor, panelBackgroundExpectedColor, PalleteManager.colorChangeProgress));
			backgroundSprite.color = Color.Lerp (lastBackgroundColor, backgroundExpectedColor, PalleteManager.colorChangeProgress);
		} else {
			if (panelBackgroundSprites[0].color != panelBackgroundExpectedColor) {
				SetPanelBackgroundColorTo(panelBackgroundExpectedColor);
			}
			if (backgroundSprite.color != backgroundExpectedColor) {
				backgroundSprite.color = backgroundExpectedColor;
			}
		}
	}

	void SetPanelBackgroundColorTo(Color color) {
		foreach (Image panel in panelBackgroundSprites)  {
			panel.color = color;
		}
	}

	void CheckInput() {
		if (gameState == GameState.Menu) {
			if (Input.GetKeyDown (KeyCode.Space) || 
				Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
				StartGame ();
			}
		} else if (gameState == GameState.GameOver) {
			if (Input.GetKeyDown (KeyCode.Space) ||
				Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
				ToMainMenu ();
			}
		}
	}
}
