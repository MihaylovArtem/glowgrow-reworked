using UnityEngine;
using System.Collections; 

public class PalleteManager : MonoBehaviour {

	static private Pallete firstPallete = new Pallete(new Color(0.77f, 0.85f, 0.95f, 1f), new Color(0.93f, 0.78f, 0.73f, 1f), 
		new Color(1f, 0.46f, 0.24f, 1f), new Color(0.16f, 0.56f, 1f, 1f));
	static private Pallete secondPallete = new Pallete(new Color(0.87f, 0.87f, 0.94f, 1f), new Color(0.39f, 0.37f, 0.57f, 1f), 
		new Color(0.8f, 0.33f, 0.73f, 1f), new Color(0.32f, 0.72f, 0.87f, 1f));
	static private Pallete thirdPallete = new Pallete(new Color(0.94f, 0.93f, 0.87f, 1f), new Color(0.5f, 0.7f, 0.5f, 1f), 
		new Color(0.85f, 0.44f, 0.29f, 1f), new Color(0.87f, 0.8f, 0.35f, 1f));
	static private Pallete greyPallete = new Pallete(new Color(0.8f, 0.8f, 0.8f, 1f), new Color(0.3f, 0.3f, 0.3f, 1f), 
		new Color(1f, 0.4f, 0.4f, 0.4f), new Color(0.4f, 0.4f, 0.4f, 1f));
	static private int currentPalleteNum = 1;
	static private int previousPalleteNum = 1;
	static private float timer = 0.0f;
	static private float easeInDuration = 2f;
	static private float easeOutDuration = 1f;

	static public float colorChangeDuration {
		get {
			return currentPalleteNum == 0 ? easeOutDuration : easeInDuration;
		}
	}

	static public bool isColorChangeInProgress {
		get {
			return timer <= colorChangeDuration;
		}
	}
	static public float colorChangeProgress {
		get {
			return currentPalleteNum == 0 ? Mathf.Pow (Mathfx.Sinerp(0f, 1f, timer/colorChangeDuration),0.5f) : Mathf.Pow (Mathfx.Coserp(0f, 1f, timer/colorChangeDuration), 2f);
		}
	}


	static public Pallete previousPallete {
		set {
			currentPallete = value;
		}
		get {
			switch (previousPalleteNum) {
			case 0:
				return greyPallete;
			case 1:
				return firstPallete;
			case 2:
				return secondPallete;
			case 3:
				return thirdPallete;
			default:
				return firstPallete;
			}
		}
	}

	static public Pallete currentPallete {
		set {
			currentPallete = value;
		}
		get {
			switch (currentPalleteNum) {
			case 0:
				return greyPallete;
			case 1:
				return firstPallete;
			case 2:
				return secondPallete;
			case 3:
				return thirdPallete;
			default:
				return firstPallete;
			}
		}
	}

	private IEnumerator ChangePallete(bool toGrey, float delayTime = 0.0f) {
		yield return new WaitForSeconds (delayTime);
		timer = 0.0f;
		previousPalleteNum = currentPalleteNum;
		while (previousPalleteNum == currentPalleteNum) {
			currentPalleteNum = toGrey ? 0 : Random.Range (1, 4);
		}
		if (toGrey) {
			StartCoroutine (ChangePallete (false, colorChangeDuration));
		}
	}

	public void ChangePallete() {
		StartCoroutine(ChangePallete (toGrey: true));
	}

	public void Update() {
		timer += Time.deltaTime;
	}
}