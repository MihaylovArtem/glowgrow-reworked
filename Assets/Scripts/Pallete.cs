using UnityEngine;
using System.Collections;

public class Pallete : MonoBehaviour {
	public Color backgroundColor;
	public Color backgroundColorSecondary;
	public Color playerColor;
	public Color bulletFirstTypeColor;
	public Color bulletSecondTypeColor;


	public Pallete(Color backgroundColor, Color backgroundColorSecondary, Color bulletFirstTypeColor, Color bulletSecondTypeColor)
	{
		this.backgroundColor = backgroundColor;
		this.backgroundColorSecondary = backgroundColorSecondary;
		this.playerColor = backgroundColorSecondary;
		this.bulletFirstTypeColor = bulletFirstTypeColor;
		this.bulletSecondTypeColor = bulletSecondTypeColor;
	}
}
