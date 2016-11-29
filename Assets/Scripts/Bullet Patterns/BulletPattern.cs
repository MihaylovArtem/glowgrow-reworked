using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BulletPattern : MonoBehaviour
{
	public GameObject bulletPrefab;
	public delegate void BulletPatterDelegate();
	public static event BulletPatterDelegate PatternEnded;

	protected int bulletAmount = 0;
	protected List<GameObject> instantiatedBullets = new List<GameObject>();
    

    public abstract void SpawnSingleBullet();
	public abstract IEnumerator SpawnPattern();
}
