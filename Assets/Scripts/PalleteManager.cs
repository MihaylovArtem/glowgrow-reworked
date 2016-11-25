using UnityEngine;
using System.Collections;

public class PalleteManager : MonoBehaviour {

	static public Pallete firstPallete = new Pallete(new Color(0.4f, 0.4f, 0.4f, 1f), new Color(0.4f, 0.4f, 0.4f, 1f), new Color(0.4f, 0.4f, 0.4f, 1f), new Color(0.4f, 0.4f, 0.4f, 1f), new Color(0.2f, 0.2f, 0.7f, 1f));
	static public int currentPallete = 1;

	static public Pallete getCurrentPallete() {
		switch (currentPallete) {
		case 1:
			return firstPallete;
		default:
			return firstPallete;
		}
	}

}
