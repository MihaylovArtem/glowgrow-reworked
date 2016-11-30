using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	enum GameState {
		Menu, Playing, Pause, GameOver
	}

	private GameState gameState = GameState.Menu;

	public Player player;
	//TODO: заменить на нормальный BulletPatternManager
	public LinearBulletPattern pattern;

    private BulletPatternManager patternManager;
	public Text scoreText;
	public Text superPowerText;
	public int currentScore = 0;
	private int bulletsForPower = 0;
	private const int maxBulletsForPower = 10;
	public GameObject menuPanel;


	// Use this for initialization
	void Start() {
		Player.OnRightBulletCatch += IncreaseScore;
		Player.OnRightBulletCatch += IncreaseBulletsForPower;
		Player.OnWrongBulletCatch += DecreaseScore;
		Player.OnMaxBulletCountCatch += PerformNextLevel;
		Player.OnGameOver += PerformGameOver;
		Player.OnSuperPower += PerformSuperPower;
		Player.OnSuperPowerBulletCatch += IncreaseScore;
        patternManager = gameObject.GetComponent<BulletPatternManager>();
        patternManager.SpawnRandomPattern();
	}

	// Update is called once per frame
	void Update () {
		CheckInput ();
		UpdateUI ();
	}

	void IncreaseScore() {
		currentScore++;
		scoreText.text = currentScore.ToString() + " шаров";
	}

	void IncreaseBulletsForPower() {
		if (bulletsForPower < maxBulletsForPower) {
			bulletsForPower++;
			superPowerText.text = "Шаров для суперсилы " + bulletsForPower.ToString() + " / " + maxBulletsForPower.ToString();
		}
	}

	void DecreaseScore() {
		currentScore--;
		bulletsForPower = 0;
		scoreText.text = currentScore.ToString() + " шаров";
		superPowerText.text = "Шаров для суперсилы " + bulletsForPower.ToString() + " / " + maxBulletsForPower.ToString();
	}

	void PerformNextLevel() {
		
	}

	void PerformGameOver() {
		
	}

	void PerformSuperPower() {
		bulletsForPower = 0;
		superPowerText.text = "Шаров для суперсилы " + bulletsForPower.ToString() + " / " + maxBulletsForPower.ToString();
	}

	void StartGame() {
		gameState = GameState.Playing;
		StartCoroutine(pattern.SpawnPattern());
	}

	void ToMainMenu() {
		gameState = GameState.Menu;
	}

	void UpdateUI() {
		if (gameState == GameState.Playing) {
			if (menuPanel.transform.localScale.x < 7) {
				menuPanel.transform.localScale *= 1.05f + (menuPanel.transform.localScale.x / 50);
			}
			if (menuPanel.transform.localScale.x > 7) {
				menuPanel.transform.localScale = new Vector3 (7, 7, 7);
			}
		} else if (gameState == GameState.Menu) {
			if (menuPanel.transform.localScale.x > 1) {
				menuPanel.transform.localScale /= 1.05f + (menuPanel.transform.localScale.x / 50);
			}
			if (menuPanel.transform.localScale.x < 1) {
				menuPanel.transform.localScale = new Vector3 (1, 1, 1);
			}
		}
	}

	void CheckInput() {
		if (gameState == GameState.Playing) {
			if (Input.GetKeyUp (KeyCode.DownArrow)) {
				ToMainMenu();
			}
		} else if (gameState == GameState.Menu) {
			if (Input.GetKeyUp(KeyCode.UpArrow)) {
				StartGame ();
			}
		}
	}

}
