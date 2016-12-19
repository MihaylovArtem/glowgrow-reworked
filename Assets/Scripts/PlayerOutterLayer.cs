using UnityEngine;
using System.Collections;

public class PlayerOutterLayer : MonoBehaviour {

	public ColorType colorType = ColorType.first;
	public float neededScale;
	private Color expectedColor {
		get {
			if (colorType == ColorType.first) {
				return PalleteManager.currentPallete.bulletFirstTypeColor;
			} else {
				return PalleteManager.currentPallete.bulletSecondTypeColor;
			}
		}
	}
	public SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		sprite.color = expectedColor;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x < neededScale) {
			var delta = Time.deltaTime / 2;
			transform.localScale += new Vector3 (delta, delta, 0);
		} else {
			transform.localScale = new Vector3 (neededScale, neededScale, transform.localScale.z);
		}
	}

	public void DestroySelf () {
		Destroy(gameObject);
	}
}
