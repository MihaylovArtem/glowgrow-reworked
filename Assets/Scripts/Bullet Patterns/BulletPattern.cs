using UnityEngine;
using System.Collections;

public abstract class BulletPattern : MonoBehaviour
{
	public GameObject bulletPrefab;
//    public abstract int bulletAmount;
//    public abstract <> bulletRate;

    public abstract void SpawnSingleBullet();
	public abstract IEnumerator SpawnPattern();
}
