using UnityEngine;
using System.Collections;

public class DoubleSpiralPattern : BulletPattern {

	private bool needPair;
	private float lastX;
	private float lastY;

	public override void SpawnSingleBullet()
	{
		sentBulletAmount++;
		var bullet = Instantiate(bulletPrefab) as GameObject;
		var bulletScript = bullet.GetComponent<Bullet>();
		bulletScript.setColorType(nextColorType);
		bulletScript.isSpiral = true;
		if (needPair) {
			bullet.transform.position = new Vector3(-lastX, -lastY);
			needPair = false;
		} else {
			lastX = 7.0f * (Random.Range(0, 3) - 1);
			lastY = 7.0f * (Random.Range(0, 3) - 1);
			while (System.Math.Abs(lastX) < 0.01f && System.Math.Abs(lastY) < 0.01f) {
				lastX = 7.0f * (Random.Range(0, 3) - 1);
				lastY = 7.0f * (Random.Range(0, 3) - 1);
			}
			bullet.transform.position = new Vector3(lastX, lastY);
			needPair = true;
		}
	}

	public override IEnumerator SpawnPattern(int bulletsAmount)
	{
		this.bulletAmount = bulletsAmount;
		this.remainingBulletAmount = bulletsAmount;
		sentBulletAmount = 0;

		for (int i = 0; i < bulletAmount; i++)
		{
			if (GameManager.gameState != GameManager.GameState.Playing)
			{
				break;
			}
			SpawnSingleBullet();
			yield return new WaitForSeconds(0.10f);
			SpawnSingleBullet();
			nextColorType = nextColorType == ColorType.first ? ColorType.second : ColorType.first;
			yield return new WaitForSeconds(0.50f);
		}
	}
}
