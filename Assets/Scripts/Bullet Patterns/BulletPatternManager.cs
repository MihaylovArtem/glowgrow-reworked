using UnityEngine;
using System.Collections;

public class BulletPatternManager : MonoBehaviour {

	public LinearBulletPattern linearBulletPattern;
    public FallBulletPattern fallBulletPattern;
	public SingleSpiralPattern singleSpiralPattern;
	public DoubleSpiralPattern doubleSpiralPattern;

	// Use this for initialization
	void Start () {
		CheckBullets();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CheckBullets() {
		var bullets = GameObject.FindGameObjectsWithTag("Bullet");
		if (bullets.Length == 0) {
			SpawnRandomPattern();
		}
		Invoke("CheckBullets", 1.0f);
	}

	public void SpawnRandomPattern() {
		if (GameManager.gameState == GameManager.GameState.Playing) {
			int number = Random.Range(0,4);
			switch (number) {
			case 0:
				StartCoroutine(linearBulletPattern.SpawnPattern (8));
				break;
            case 1:
                StartCoroutine(fallBulletPattern.SpawnPattern (12));
				break;
			case 2:
				StartCoroutine(singleSpiralPattern.SpawnPattern(6));
				break;
			case 3:
				StartCoroutine(doubleSpiralPattern.SpawnPattern(4));
				break;
			default:
				break;
			}
		}
	}
}
