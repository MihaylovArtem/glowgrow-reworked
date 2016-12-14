using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	
//    public <> bulletType;
//    public <> speed;

	public GameObject particlesPrefab;

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

    //уничтожается объект пули
	public void DestroySelf() {
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
		Destroy (gameObject);
    }

    public void SetSpeed() {
        
    }

    public void SetBehaviour() {
        
    }

	// Use this for initialization
	void Start () {
		
	}

	void Update() {
		sprite.color = expectedColor;
	}

	public void setColorType(ColorType type) {
		colorType = type;
		sprite.color = expectedColor;
	}
}
