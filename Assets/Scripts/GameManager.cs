using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public Player player;
	//TODO: заменить на нормальный BulletPatternManager
	public LinearBulletPattern linearPattern; 
	public Text scoreText;
	public Text superPowerText;
	public int currentScore = 0;
	private int bulletsForPower = 0;
	private const int maxBulletsForPower = 10;

	// Use this for initialization
	void Start() {
		Player.OnRightBulletCatch += IncreaseScore;
		Player.OnRightBulletCatch += IncreaseBulletsForPower;
		Player.OnWrongBulletCatch += DecreaseScore;
		Player.OnMaxBulletCountCatch += PerformNextLevel;
		Player.OnGameOver += PerformGameOver;
		Player.OnSuperPower += PerformSuperPower;
		Player.OnSuperPowerBulletCatch += IncreaseScore;
		StartCoroutine(linearPattern.SpawnPattern());
	}
	
	// Update is called once per frame
	void Update () {

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


}
