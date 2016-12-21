using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Bullet : MonoBehaviour
{
	public delegate void BulletDelegate();
	public static event BulletDelegate OnBulletDestroy;

//    public <> bulletType;
//    public <> speed;

    public bool affectedByGravity = false;
    public bool isSpiral = false;

    public Vector3 playerPosition;

    public Rigidbody2D bulletRigidbody;

    public float bulletSpeed = 2.0f;
    public float bulletCurve = 0.8f;
    
    float maxGravDist = 20.0f;
    float maxGravity = 20.0f;

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
        playerPosition = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        if (Random.Range(0, 20) == 7) {
            isBonus = true;
			transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            bulletScore.text = (Random.Range(2,6) * 50).ToString();
        }
        else {
            isBonus = false;
        }
        bulletRigidbody = gameObject.GetComponent<Rigidbody2D>();
	}

	void Update() {
		sprite.color = expectedColor;

        if (isSpiral) {
            bulletRigidbody.velocity = new Vector2(gameObject.transform.position.y * bulletSpeed, -gameObject.transform.position.x * bulletSpeed) - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y).normalized * bulletCurve * bulletSpeed;
        }

        if (affectedByGravity) {
            var dist = Vector3.Distance(playerPosition, transform.position);
            if (dist <= maxGravDist) {
                Vector3 v = playerPosition - transform.position;
                bulletRigidbody.AddRelativeForce(v.normalized * ((1.0f - dist / maxGravDist) * maxGravity));
            }
        }
	}

	public void setColorType(ColorType type) {
		colorType = type;
		sprite.color = expectedColor;
	}
}
