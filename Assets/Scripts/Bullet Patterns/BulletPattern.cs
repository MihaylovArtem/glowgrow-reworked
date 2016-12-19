using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class BulletPattern : MonoBehaviour
{
	public GameObject bulletPrefab;
	public delegate void BulletPatterDelegate();
	public static event BulletPatterDelegate PatternEnded;
    public Player player;

	protected int bulletAmount = 0;
	protected int sentBulletAmount = 0;
	protected int remainingBulletAmount = -1; // Когда станет 0, вызываем ивент PatternEnded
    protected ColorType nextColorType = ColorType.first;

    void Start() {
		Bullet.OnBulletDestroy += decreaseRBA;
		CheckBullets ();
    }

    void Update() {
//		if (remainingBulletAmount <= 0) {
//			remainingBulletAmount = null;
//        }
    }

	void CheckBullets() {
		var bullets = GameObject.FindGameObjectsWithTag ("Bullet");
		if (bullets.Count() == 0) {
			PatternEnded();
		}
		Invoke ("CheckBullets", 1.0f);
	}

    void decreaseRBA() {
//		remainingBulletAmount--;
//		Debug.Log (remainingBulletAmount.ToString ());
    }

	public abstract void SpawnSingleBullet();
	public abstract IEnumerator SpawnPattern(int bulletsAmount);
}
