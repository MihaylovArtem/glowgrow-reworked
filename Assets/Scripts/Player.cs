using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	const int bulletMaxCount = 20;

	public delegate void PlayerDelegate();
	public static event PlayerDelegate OnRightBulletCatch;
	public static event PlayerDelegate OnWrongBulletCatch;
	public static event PlayerDelegate OnMaxBulletCountCatch;
	public static event PlayerDelegate OnGameOver;
	public static event PlayerDelegate OnSuperPower;

	private ColorType currentColorType = ColorType.first;
	private Color lastColor;
	private Color expectedColor {
		get {
			if (currentColorType == ColorType.first) {
				return PalleteManager.getCurrentPallete().bulletFirstTypeColor;
			} else {
				return PalleteManager.getCurrentPallete().bulletSecondTypeColor;
			}
		}
	}
	public Stack<GameObject> outterLayerStack = new Stack<GameObject>();
	public GameObject outterLayerPrefab;
	private float expectedSize;
	private float expectedGlowSize;
	private float currentSize;

	private float increasePlayerSizeDelta;
	private float increaseGlowSizeDelta;

	private CircleCollider2D playerCollider;

	private float colorChangeDuration = 0.2f;
	private float colorChangeTimer = 0.0f;

	private int bulletsForPower = 0;
	private const int maxBulletsForPower = 10;

	public GameObject glowObject; //необходимо, чтобы каждый раз не искать в компонентах объекта glow компонент цвет;
	public SpriteRenderer glowSpriteRenderer; //необходимо, чтобы каждый раз не искать в компонентах объекта glow компонент цвет;

	public void ChangeColorType(ColorType type) {
		colorChangeTimer = 0.0f;
		currentColorType = type;
		lastColor = glowSpriteRenderer.color;
	}

	public void IncreaseSize(Bullet catchedBullet) {
		//TODO: анимация
		expectedSize += increasePlayerSizeDelta;
		expectedGlowSize += increaseGlowSizeDelta;
		playerCollider.radius *= expectedSize / (expectedSize - increasePlayerSizeDelta);

		var outterLayer = Instantiate (outterLayerPrefab, gameObject.transform) as GameObject;
		outterLayerStack.Push(outterLayer);

		var outterLayerScript = outterLayer.GetComponent<PlayerOutterLayer> ();
		outterLayerScript.colorType = catchedBullet.colorType;

		outterLayer.transform.position = new Vector3 (0, 0, outterLayerStack.Count/100.0f);
		outterLayer.transform.localScale = new Vector2 (expectedSize, expectedSize);
		glowObject.transform.localScale = new Vector2 (expectedGlowSize, expectedGlowSize);

		if (outterLayerStack.Count == bulletMaxCount) {
			DestroyAllLayers();
			OnMaxBulletCountCatch();
		}

	}

	public void DecreaseSize() {
		//TODO: анимация
		if (outterLayerStack.Count == 0) {
			DestroySelf();
		} else {

			playerCollider.radius /= (expectedSize / (expectedSize - increasePlayerSizeDelta));

			expectedSize -= increasePlayerSizeDelta;
			expectedGlowSize -= increaseGlowSizeDelta;

			var lastLayer = outterLayerStack.Pop () as GameObject;
			lastLayer.GetComponent<PlayerOutterLayer>().DestroySelf();

			glowObject.transform.localScale = new Vector2(expectedGlowSize, expectedGlowSize);
		}

	}

	public void DestroyAllLayers() {
		//TODO: анимация

		playerCollider.radius /= (expectedSize / (expectedSize - increasePlayerSizeDelta * outterLayerStack.Count));

		expectedSize -= increasePlayerSizeDelta * outterLayerStack.Count;
		expectedGlowSize -= increaseGlowSizeDelta * outterLayerStack.Count;


		while (outterLayerStack.Count > 0) {
			var lastLayer = outterLayerStack.Pop() as GameObject;
			lastLayer.GetComponent<PlayerOutterLayer>().DestroySelf();
		}

		glowObject.transform.localScale = new Vector2(expectedGlowSize, expectedGlowSize);
	}

	public void performSuperPower() {
		GameObject[] allBullets = GameObject.FindGameObjectsWithTag("Bullet");
		OnSuperPower();
		bulletsForPower = 0;
		foreach (GameObject bullet in allBullets) {
			bullet.GetComponent<Bullet>().DestroySelf();
		}
	}

	public void DestroySelf() {
		
 	}


	// Use this for initialization
	void Start () {
		glowSpriteRenderer.color = PalleteManager.getCurrentPallete().bulletFirstTypeColor;

		playerCollider = gameObject.GetComponent<CircleCollider2D>();

		expectedSize = 1;
		expectedGlowSize = glowObject.transform.localScale.x;

		increasePlayerSizeDelta = expectedSize / 10;
		increaseGlowSizeDelta = expectedGlowSize / 10;
	}

	// Update is called once per frame
	void Update () {
		CheckInput();
		HandleCurrentColor();
		if (Input.GetKeyUp("space")) {
			if (bulletsForPower == maxBulletsForPower) {
				Debug.Log("Go!");
				performSuperPower();
			}
		}
	}

	void CheckInput() {
		if (Input.GetKeyDown ("left")) {
			ChangeColorType (ColorType.first);
		} else if (Input.GetKeyDown ("right")) {
			ChangeColorType (ColorType.second);
		}
	}

	void HandleCurrentColor() {
		colorChangeTimer += Time.deltaTime;
		var completionPercent = colorChangeTimer / colorChangeDuration;
		glowSpriteRenderer.color = Color.Lerp (lastColor, expectedColor, completionPercent);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Bullet") {
			Bullet bullet = other.gameObject.GetComponent<Bullet>();
			if (bullet.colorType == currentColorType) {
				IncreaseSize (bullet);
				OnRightBulletCatch ();
				if (bulletsForPower < maxBulletsForPower) {
					bulletsForPower++;
				}
			} else {
				DecreaseSize ();
				OnWrongBulletCatch();
				bulletsForPower = 0;
			}
			bullet.DestroySelf ();
		}
	}
}
