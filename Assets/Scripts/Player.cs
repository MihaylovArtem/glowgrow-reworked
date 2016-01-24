using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public <> currentColorType;
    public stack<GameObject> bulletStack = new Stack<GameObject>();
    public <> expectedSize;
    public <> currentSize;


    //public GameObject playerGameObject; //объект самого игрока - круг в центре;
    //public GameObject glowGameObject; // объект свечения вокруг игрока - задает цвет пуль, которые игрок может ловить


    private SpriteRenderer glowSpriteRenderer; //необходимо, чтобы каждый раз не искать в компонентах объекта glow компонент цвет;



    public void ChangeColorType(int newColorType)
    {
        if (newColorType != currentColorType)
        {
            currentColorType = newColorType;
            // <дописать> анимированно заменяем цвет свечения игрока
        }
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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
