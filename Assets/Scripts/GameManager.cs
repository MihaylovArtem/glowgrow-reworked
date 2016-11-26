using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public Player player;

	public Text scoreText;
	public int currentScore = 0;

	// Use this for initialization
	void Start () {
		Player.OnBulletCatch += IncreaseScore;
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = currentScore.ToString() + " шаров";
	}

	void IncreaseScore() {
		currentScore++;
	}
}
