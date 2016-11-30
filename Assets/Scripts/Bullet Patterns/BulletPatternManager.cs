using UnityEngine;
using System.Collections;

public class BulletPatternManager : MonoBehaviour {

	public LinearBulletPattern linearBulletPattern;

	// Use this for initialization
	void Start () {
		LinearBulletPattern.PatternEnded += SpawnRandomPattern;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnRandomPattern() {
		if (GameManager.gameState == GameManager.GameState.Playing) {
			//random дописать
			int number = 0;
			switch (number) {
			case 0:
				Debug.Log ("Added linear bullet pattern");
				StartCoroutine (linearBulletPattern.SpawnPattern (5));
				break;
			default:
				StartCoroutine (linearBulletPattern.SpawnPattern (5));
				break;
			}
		}
	}
}
