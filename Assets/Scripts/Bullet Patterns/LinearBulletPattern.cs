using UnityEngine;
using System.Collections;

public class LinearBulletPattern : BulletPattern {

	private ColorType nextColorType = ColorType.first;

	public override void SpawnSingleBullet() {
		var Bullet = Instantiate(bulletPrefab) as GameObject;
		Bullet.GetComponent<Bullet>().setColorType(nextColorType);
		Bullet.transform.position = new Vector3(10, 0, 0);
		Bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-1,0);
	}

	public override IEnumerator SpawnPattern() {
		for (int i = 0; i < 20; i++) {
			SpawnSingleBullet();
			nextColorType = (nextColorType == ColorType.first) ? ColorType.second : ColorType.first;
			yield return new WaitForSeconds(2.0f);
		}
	}
}
