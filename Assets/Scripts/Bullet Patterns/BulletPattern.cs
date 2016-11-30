using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BulletPattern : MonoBehaviour
{
	public GameObject bulletPrefab;
	public delegate void BulletPatterDelegate();
	public static event BulletPatterDelegate PatternEnded;
    public Player player;

	protected int bulletAmount = 0;
	protected int sentBulletAmount = 0;
	protected int remainingBulletAmount = -1; // Когда станет 0, вызываем ивент PatternEnded

    void Start() {
		Debug.Log (player);
        Player.OnWrongBulletCatch += decreaseRBA;
        Player.OnRightBulletCatch += decreaseRBA;
		Player.OnSuperPowerBulletCatch += decreaseRBA;
    }

    void Update() {
		if (remainingBulletAmount == 0) {
			remainingBulletAmount = -1;
            PatternEnded();
        }
    }

    void decreaseRBA() {
		Debug.Log (remainingBulletAmount);
        remainingBulletAmount--;
    }

	public abstract void SpawnSingleBullet();
	public abstract IEnumerator SpawnPattern(int bulletsAmount);
}
