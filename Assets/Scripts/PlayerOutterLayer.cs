using UnityEngine;
using System.Collections;

public class PlayerOutterLayer : MonoBehaviour {

	public ColorType colorType = ColorType.first;
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
	
	}

	public void DestroySelf () {
		Destroy(gameObject);
	}
}
