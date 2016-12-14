using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Bullet : MonoBehaviour
{



//    public <> bulletType;
//    public <> speed;

	public ColorType colorType = ColorType.first;
    public bool isBonus;
    public Text bulletScore;
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
        if (Random.Range(0, 20) == 7) {
            isBonus = true;
            transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            bulletScore.text = (Random.Range(2,6) * 50).ToString();
        }
        else {
            isBonus = false;
        }
        Debug.Log(bulletScore.text);
	}

	public void setColorType(ColorType type) {
		colorType = type;
		sprite.color = expectedColor;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
