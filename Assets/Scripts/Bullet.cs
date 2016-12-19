using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Bullet : MonoBehaviour
{
	public delegate void BulletDelegate();
	public static event BulletDelegate OnBulletDestroy;

//    public <> bulletType;
//    public <> speed;

	public GameObject particlesPrefab;

	public ColorType colorType = ColorType.first;
    public bool isBonus;
    public Text bulletScore;
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

    //уничтожается объект пули
	public void DestroySelf() {
		OnBulletDestroy ();
		Destroy (gameObject);
    }
    //уничтожается объект пули и вызывается взрыв частиц
    public void DestroySelfWithExplosion() {
		var particles = Instantiate (particlesPrefab) as GameObject;
		particles.transform.position = transform.position;
		var particlesComponent = particles.GetComponent <ParticleSystem> ();
		particlesComponent.startColor = sprite.color;
		particlesComponent.Emit(16);
		Destroy (particles, 3);
		DestroySelf();
    }

    public void SetSpeed() {
        
    }

    public void SetBehaviour() {
        
    }

	// Use this for initialization
	void Start () {
        if (Random.Range(0, 20) == 7) {
            isBonus = true;
			transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            bulletScore.text = (Random.Range(2,6) * 50).ToString();
        }
        else {
            isBonus = false;
        }
        Debug.Log(bulletScore.text);
	}

	void Update() {
		sprite.color = expectedColor;
	}

	public void setColorType(ColorType type) {
		colorType = type;
		sprite.color = expectedColor;
	}
}
