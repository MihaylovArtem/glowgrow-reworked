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

//	Vector3 cameraStartingPosition = new Vector3 (0, 0, -8);
//	Vector3 cameraEndingPosition = new Vector3 (0, 0, -8);

	public enum GameState {
		Menu, Playing, Pause, GameOver
	}

	static public GameState gameState = GameState.Menu;

	public Player player;
	//TODO: заменить на нормальный BulletPatternManager
    private BulletPatternManager patternManager;
	public Text scoreText;
	public Text superPowerText;
	public Text levelText;
	public int currentScore = 0;
	private int bulletsForPower = 0;
	private const int maxBulletsForPower = 10;
	public GameObject menuPanel;

	public int levelNum = 1;

	public double bulletSpeed {
		get {
			return levelNum <=10 ? 1*levelNum/10 : 2;
		}
	}

	// Use this for initialization
	void Start() {
		Player.OnRightBulletCatch += IncreaseScore;
		Player.OnRightBulletCatch += IncreaseBulletsForPower;
		Player.OnWrongBulletCatch += DecreaseScore;
		Player.OnMaxBulletCountCatch += PerformNextLevel;
		Player.OnGameOver += PerformGameOver;
		Player.OnSuperPower += PerformSuperPower;
		Player.OnSuperPowerBulletCatch += IncreaseScore;
	}

	// Update is called once per frame
	void Update () {
		CheckInput ();
	}

	void IncreaseScore() {
		currentScore++;
	}

	void IncreaseBulletsForPower() {
		if (bulletsForPower < maxBulletsForPower) {
			bulletsForPower++;
		}
	}

	void DecreaseScore() {
		currentScore--;
		bulletsForPower = 0;
	}

	void PerformNextLevel() {
		levelNum++;
	}

	void PerformGameOver() {
		gameState = GameState.GameOver;
		DestroyAllBullets ();
	}

	void DestroyAllBullets() {
		GameObject[] AllBullets = GameObject.FindGameObjectsWithTag("Bullet");
		foreach (GameObject bullet in AllBullets) {
			bullet.GetComponent<Bullet>().DestroySelf();
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
		patternManager.SpawnRandomPattern();

		currentScore = 0;
		bulletsForPower = 0;
		levelNum = 1;

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
