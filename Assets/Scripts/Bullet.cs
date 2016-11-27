using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

//    public <> bulletType;
//    public <> speed;

	public ColorType colorType = ColorType.first;
	private Color expectedColor {
		get {
			if (colorType == ColorType.first) {
				return PalleteManager.getCurrentPallete().bulletFirstTypeColor;
			} else {
				return PalleteManager.getCurrentPallete().bulletSecondTypeColor;
			}
		}
	}
	public SpriteRenderer sprite;

    //уничтожается объект пули
    public void DestroySelf() {
		Destroy (gameObject);
    }
    //уничтожается объект пули и вызывается взрыв частиц
    public void DestroySelfWithExplosion() {
        
    }

    public void SetSpeed() {
        
    }

    public void SetBehaviour() {
        
    }

	// Use this for initialization
	void Start () {
		
	}

	public void setColorType(ColorType type) {
		colorType = type;
		sprite.color = expectedColor;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
