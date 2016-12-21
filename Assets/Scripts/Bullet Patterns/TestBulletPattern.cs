using UnityEngine;
using System.Collections;

public class TestBulletPattern : BulletPattern {

	public GameObject leftText;
	public GameObject rightText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void SpawnSingleBullet()
	{
		
	}

	public override IEnumerator SpawnPattern(int bulletsAmount)
	{

		this.bulletAmount = 2;
		this.remainingBulletAmount = 2;
		sentBulletAmount = 0;

		var bullet1 = Instantiate(bulletPrefab) as GameObject;
		var bulletScript1 = bullet1.GetComponent<Bullet>();
		bulletScript1.setColorType(ColorType.second);
		bullet1.transform.position = new Vector3(-10, 0);
		bullet1.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
		sentBulletAmount++;
		yield return new WaitForSeconds(1.5f);
		rightText.SetActive(true);
		yield return new WaitForSeconds(0.7f);
		rightText.SetActive(false);
		yield return new WaitForSeconds(0.7f);
		rightText.SetActive(true);
		yield return new WaitForSeconds(0.7f);
		rightText.SetActive(false);

		yield return new WaitForSeconds(0.7f);

		var bullet2 = Instantiate(bulletPrefab) as GameObject;
		var bulletScript2 = bullet2.GetComponent<Bullet>();
		bulletScript2.setColorType(ColorType.first);
		bullet2.transform.position = new Vector3(10, 0);
		bullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
		sentBulletAmount++;
		yield return new WaitForSeconds(1.5f);
		leftText.SetActive(true);
		yield return new WaitForSeconds(0.7f);
		leftText.SetActive(false);
		yield return new WaitForSeconds(0.7f);
		leftText.SetActive(true);
		yield return new WaitForSeconds(0.7f);
		leftText.SetActive(false);
	}

}
