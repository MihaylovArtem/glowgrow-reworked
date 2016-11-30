using UnityEngine;
using System.Collections;

public class BulletPatternManager : MonoBehaviour {



	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnRandomPattern() {
        //random дописать
        int number = 0;
        switch (number) {
            case 0:
                gameObject.AddComponent<LinearBulletPattern>();
                LinearBulletPattern.PatternEnded += SpawnRandomPattern;
                break;
            default:
                gameObject.AddComponent<LinearBulletPattern>();
                LinearBulletPattern.PatternEnded += SpawnRandomPattern;
                break;
        }
	}
}
