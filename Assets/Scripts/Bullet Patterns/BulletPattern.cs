using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BulletPattern : MonoBehaviour
{
	public GameObject bulletPrefab;
	public delegate void BulletPatterDelegate();
	public static event BulletPatterDelegate PatternEnded;
    public Player player;

	public int bulletAmount = 0;
    public int remainingBulletAmount; // Когда станет 0, вызываем ивент PatternEnded

    void Start() {
        remainingBulletAmount = bulletAmount;
        Player.OnWrongBulletCatch += decreaseRBA;
        Player.OnRightBulletCatch += decreaseRBA;
    }

    void Update() {
        if (remainingBulletAmount == 0) {
            PatternEnded();
            Destroy(this);
        }
    }

    void decreaseRBA() {
        remainingBulletAmount--;
    }

    public abstract void SpawnSingleBullet();
	public abstract IEnumerator SpawnPattern();
}
