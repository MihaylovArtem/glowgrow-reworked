using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public Player player;
	//TODO: заменить на нормальный BulletPatternManager
	public LinearBulletPattern linearPattern; 
	public Text scoreText;
	public int currentScore = 0;

	// Use this for initialization
	void Start() {
		Player.OnRightBulletCatch += IncreaseScore;
		Player.OnWrongBulletCatch += DecreaseScore;
		Player.OnMaxBulletCountCatch += PerformNextLevel;
		Player.OnGameOver += PerformGameOver;
		StartCoroutine(linearPattern.SpawnPattern());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void IncreaseScore() {
		currentScore++;
		scoreText.text = currentScore.ToString() + " шаров";
	}

	void DecreaseScore() {
		currentScore--;
		scoreText.text = currentScore.ToString() + " шаров";
	}

	void PerformNextLevel() {
		
	}

	void PerformGameOver() {
		
	}


}
