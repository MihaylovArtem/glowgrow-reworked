using UnityEngine;
using System.Collections;

public class FallBulletPattern : BulletPattern
{

	public override void SpawnSingleBullet()
	{
		sentBulletAmount++;
		var bullet = Instantiate(bulletPrefab) as GameObject;
		var bulletScript = bullet.GetComponent < Bullet>();
		bulletScript.setColorType(nextColorType);
		var x = nextColorType == ColorType.first ? 10 : -10;
		var y = 0;
		bulletScript.affectedByGravity = true;
		bullet.transform.position = new Vector3(x, y);
		bullet.GetComponent < Rigidbody2D>().AddForce(new Vector2(-x*1f, 60f));
	}

	public override IEnumerator SpawnPattern(int bulletsAmount)
	{
		this.bulletAmount = bulletsAmount;
		this.remainingBulletAmount = bulletsAmount;
		sentBulletAmount = 0;
		var blockAmount = Random.Range(2, 5);
		for (var i = 0; i < blockAmount; i++)
		{
			for (var j = 0; j < bulletsAmount / blockAmount; j++)
			{
				if (GameManager.gameState != GameManager.GameState.Playing)
				{
					break;
				}
				SpawnSingleBullet();
				yield return new WaitForSeconds(0.3f);
			}
			nextColorType = nextColorType == ColorType.first ? ColorType.second : ColorType.first;
			yield return new WaitForSeconds(0.5f);
		}
		if (sentBulletAmount != bulletsAmount)
		{
			for (var j = 0; j < bulletsAmount - sentBulletAmount; j++)
			{
				SpawnSingleBullet();
				yield return new WaitForSeconds(0.3f);
			}
		}
	}

}