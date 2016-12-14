using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour {
	const int bulletMaxCount = 20;

    public delegate void PlayerDelegate();
    public delegate void PlayerDelegateWithScore(int score);
    public static event PlayerDelegate OnRightBulletCatch;
    public static event PlayerDelegateWithScore OnRightBonusBulletCatch;
	public static event PlayerDelegate OnWrongBulletCatch;
	public static event PlayerDelegate OnMaxBulletCountCatch;
	public static event PlayerDelegate OnGameOver;
	public static event PlayerDelegate OnSuperPower;
    public static event PlayerDelegate OnSuperPowerBulletCatch;

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
			DestroySelf ();
			OnGameOver ();
		} else {
			var colorTypeDestroy = outterLayerStack.Peek ().GetComponent <PlayerOutterLayer> ().colorType;
			while (outterLayerStack.Peek ().GetComponent <PlayerOutterLayer> ().colorType == colorTypeDestroy) {
				playerCollider.radius /= (expectedSize / (expectedSize - increasePlayerSizeDelta));

				expectedSize -= increasePlayerSizeDelta;
				expectedGlowSize -= increaseGlowSizeDelta;

				var lastLayer = outterLayerStack.Pop () as GameObject;
				lastLayer.GetComponent<PlayerOutterLayer>().DestroySelf();

				glowObject.transform.localScale = new Vector2(expectedGlowSize, expectedGlowSize);
			}
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
			OnSuperPowerBulletCatch ();
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
	}

	void CheckInput() {
		if (Input.GetKeyDown (KeyCode.LeftArrow) ||
			Input.touchCount >0 && Input.GetTouch (0).phase == TouchPhase.Began && Input.GetTouch(0).position.x<Screen.width/2.0f) {
			ChangeColorType (ColorType.first);
		} else if (Input.GetKeyDown (KeyCode.RightArrow) || 
			Input.touchCount >0 &&  Input.GetTouch (0).phase == TouchPhase.Began && Input.GetTouch(0).position.x>Screen.width/2.0f) {
			ChangeColorType (ColorType.second);
		}

		if (Input.GetKeyDown (KeyCode.Space) ||
			Input.touchCount == 2 && Input.GetTouch (0).phase == TouchPhase.Began && Input.GetTouch(0).position.x<Screen.width/2.0f &&
			Input.GetTouch (1).phase == TouchPhase.Began && Input.GetTouch(1).position.x>Screen.width/2.0f) {
			if (bulletsForPower == maxBulletsForPower) {
				Debug.Log("Go!");
				performSuperPower();
			}
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
                if (bullet.isBonus) {
                    OnRightBonusBulletCatch(Convert.ToInt32(bullet.bulletScore.text));
                }
                else {
                    OnRightBulletCatch();
                }
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
