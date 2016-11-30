using UnityEngine;
using System;
using System.Collections;

public class LinearBulletPattern : BulletPattern {
	private ColorType nextColorType = ColorType.first;

	public override void SpawnSingleBullet() {
		sentBulletAmount++;
		var bullet = Instantiate(bulletPrefab) as GameObject;
		bullet.GetComponent<Bullet>().setColorType(nextColorType);
		var x = (float)(10.0f * Mathf.Cos((float)sentBulletAmount / (float)bulletAmount * (float)Math.PI * 2));
		var y = (float)(10.0f * Mathf.Sin((float)sentBulletAmount / (float)bulletAmount * (float)Math.PI * 2));
		bullet.transform.position = new Vector3(x, y);
		bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.25f*x,-0.25f*y);
	}

	public override IEnumerator SpawnPattern(int bulletsAmount) {
		this.bulletAmount = bulletsAmount;
		this.remainingBulletAmount = bulletsAmount;
		sentBulletAmount = 0;

		for (int i = 0; i < bulletAmount; i++) {
			SpawnSingleBullet();
			nextColorType = (nextColorType == ColorType.first) ? ColorType.second : ColorType.first;
			yield return new WaitForSeconds(0.40f);
		}
	}


}
