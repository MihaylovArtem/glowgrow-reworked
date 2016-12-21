using UnityEngine;
using System.Collections;

public class BulletPatternManager : MonoBehaviour {

	public LinearBulletPattern linearBulletPattern;
    public FallBulletPattern fallBulletPattern;

	// Use this for initialization
	void Start () {
		LinearBulletPattern.PatternEnded += SpawnRandomPattern;
        FallBulletPattern.PatternEnded += SpawnRandomPattern;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnRandomPattern() {
		if (GameManager.gameState == GameManager.GameState.Playing) {
			int number = Random.Range(0,2);
			switch (number) {
			case 0:
				Debug.Log ("Added linear bullet pattern");
				StartCoroutine(linearBulletPattern.SpawnPattern (8));
				break;
            case 1:
                StartCoroutine(fallBulletPattern.SpawnPattern (12));
                break;
			default:
				//StartCoroutine (linearBulletPattern.SpawnPattern (8));
				break;
			}
		}
	}
}
