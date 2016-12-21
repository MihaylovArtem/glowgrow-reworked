using UnityEngine;
using System.Collections;

public class SingleSpiralPattern : BulletPattern {

	public override void SpawnSingleBullet() {
		sentBulletAmount++;
		var bullet = Instantiate(bulletPrefab) as GameObject;
		var bulletScript = bullet.GetComponent<Bullet>();
		bulletScript.setColorType(nextColorType);
		var x = (float)(10.0f * Mathf.Cos((float)sentBulletAmount / (float)bulletAmount * (float)Mathf.PI * 2));
		var y = (float)(10.0f * Mathf.Sin((float)sentBulletAmount / (float)bulletAmount * (float)Mathf.PI * 2));
		bulletScript.isSpiral = true;
		bullet.transform.position = new Vector3(x, y);
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
			nextColorType = (sentBulletAmount % 2 == 1) ? ColorType.second : ColorType.first;
			yield return new WaitForSeconds(0.50f);
		}
	}
}
