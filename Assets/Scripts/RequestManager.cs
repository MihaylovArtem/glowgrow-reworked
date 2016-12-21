using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RequestManager : MonoBehaviour {

	string highscores;
	public Text highscoresLabel;

	private string highscorePrefsKey = "current_highscore";
	private int highscore {
		get {
			var saved = PlayerPrefs.GetInt (highscorePrefsKey);
			return saved;
		}
	}

	private string usernamePrefsKey = "username";
	private string username {
		get {
			var saved = PlayerPrefs.GetString (usernamePrefsKey);
			return saved;
		}
	}

	void Start() {
		GetRequest ();
	}

	public void GetRequest () {
		string url = "http://glow-grow-server-dev2.us-east-1.elasticbeanstalk.com/highscores/";
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}

	public void PostHighscoreWithUsername() {
		var jsonStr = "{\"user\":\"" + username + "\", \"value\":" + highscore.ToString () + "}";
		WWWForm form = new WWWForm ();
		// The name of the player submitting the scores
		form.AddField( "value", highscore );
		// The score
		form.AddField( "user", username );
		var url = "http://glow-grow-server-dev2.us-east-1.elasticbeanstalk.com/highscores/";
		WWW www = new WWW (url, form);
		StartCoroutine(WaitForPostRequest(www));
	}

	void PutHighscoreWithUsername() {
		var jsonStr = "{\"user\":\"" + username + "\", \"value\":" + highscore.ToString () + "}";
		byte[] myData = System.Text.Encoding.UTF8.GetBytes(jsonStr);
		var url = "http://glow-grow-server-dev2.us-east-1.elasticbeanstalk.com/highscores/" + username + "/";

		WebRequest myRequest = WebRequest.Create(url);
		myRequest.Method = "PUT";
		myRequest.ContentType = "application/json";
		myRequest.ContentLength = myData.Length;

		Stream newStream = myRequest.GetRequestStream ();

		newStream.Write (myData, 0, myData.Length);
		WebResponse myResponse = myRequest.GetResponse();
	}

	IEnumerator WaitForPostRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.text);
		} else {
			Debug.Log("WWW Error: "+ www.error);
			PutHighscoreWithUsername ();
		}    
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			highscores = "Highscores:";
			var json = new JSONObject (www.text);
			json.GetField ("results", delegate(JSONObject results) {
				int i = 0;
				foreach (JSONObject highscore in results.list) {
					i++;
					highscores += "\n" + i.ToString () + ") ";
					highscore.GetField ("user", delegate(JSONObject user) {
						highscores += user.str + " - ";
					});
					highscore.GetField ("value", delegate(JSONObject value) {
						highscores += value.ToString ();
					});
				}
			});
			highscoresLabel.text = highscores;
			Debug.Log("WWW Ok!: " + highscores);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}
}
