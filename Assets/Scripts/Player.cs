using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float colorChangeDuration = 1;
	private int currentColorType = 1;
	private Color lastColor;
	private Color expectedColor;
	//    public Stack<GameObject> bulletStack = new Stack<GameObject>();
	private float expectedSize;
	private float currentSize;
	private float colorChangeTimer = 0.0f;

	//public GameObject playerGameObject; //объект самого игрока - круг в центре;
	//public GameObject glowGameObject; // объект свечения вокруг игрока - задает цвет пуль, которые игрок может ловить

	private SpriteRenderer glowSpriteRenderer; //необходимо, чтобы каждый раз не искать в компонентах объекта glow компонент цвет;

	public void ChangeColorType(int type)
	{
		colorChangeTimer = 0.0f;
		currentColorType = type;
		lastColor = glowSpriteRenderer.color;
	}

	public void IncreaseSize()
	{

	}

	public void DecreaseSize()
	{

	}

	public void DestroySelf()
	{

	}


	// Use this for initialization
	void Start () {
		glowSpriteRenderer = this.transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
		glowSpriteRenderer.color = PalleteManager.getCurrentPallete().bulletFirstTypeColor;
	}

	// Update is called once per frame
	void Update () {
		CheckInput();
		HandleCurrentColor();
	}

	void CheckInput() {
		if (Input.GetKeyUp ("left")) {
			ChangeColorType (1);
		} else if (Input.GetKeyUp ("right")) {
			ChangeColorType (2);
		}
	}

	void HandleCurrentColor() {
		colorChangeTimer += Time.deltaTime;
		if (colorChangeTimer < colorChangeDuration) {
			var neededColor = currentColorType == 1 ? PalleteManager.getCurrentPallete ().bulletFirstTypeColor : PalleteManager.getCurrentPallete ().bulletSecondTypeColor;
			float t = Mathf.PingPong (Time.time, colorChangeDuration) / colorChangeDuration;
			glowSpriteRenderer.color = Color.Lerp (lastColor, neededColor, t);
		}
	}
}
