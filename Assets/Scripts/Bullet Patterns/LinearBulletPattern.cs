using UnityEngine;
using System;
using System.Collections;

public class LinearBulletPattern : BulletPattern {
	public int bulletAmount;
	private ColorType nextColorType = ColorType.first;

	public override void SpawnSingleBullet() {
		var bullet = Instantiate(bulletPrefab) as GameObject;
		bullet.GetComponent<Bullet>().setColorType(nextColorType);
		var x = (float) (10.0f * Mathf.Cos ((float)instantiatedBullets.Count/(float)bulletAmount * (float) Math.PI*2));
		var y = (float) (10.0f * Mathf.Sin ((float)instantiatedBullets.Count/(float)bulletAmount * (float) Math.PI*2));
		Debug.Log (instantiatedBullets.Count + " " + bulletAmount);
		Debug.Log (x + " " +  y);
		bullet.transform.position = new Vector3(x, y);
		bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.25f*x,-0.25f*y);
		instantiatedBullets.Add (bullet);
	}

	public override IEnumerator SpawnPattern() {
		Debug.Log ("Pattern started");
		Debug.Log (bulletAmount);
		for (int i = 0; i < bulletAmount; i++) {
			SpawnSingleBullet();
			nextColorType = (nextColorType == ColorType.first) ? ColorType.second : ColorType.first;
			yield return new WaitForSeconds(0.20f);
		}
	}
}
