using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
	private Vector3 currentPosition, changePosition;
	public float speedX, offsetX;
	bool isGameOver;

	private CameraController cameractrl;
	private GUIText value;
	// Use this for initialization
	void Start () {
		cameractrl = GameObject.Find ("Main Camera").GetComponent<CameraController> ();
		value = GameObject.Find ("Value").GetComponent<GUIText> ();
		isGameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		int score = 0;

		if(!isGameOver){
			score = (int)Mathf.Ceil(cameractrl.transform.position.y);
			value.text = score.ToString();
			

			currentPosition = transform.position;
			changePosition = new Vector3 ();
			
			changePosition.x = currentPosition.x;
			changePosition.y = currentPosition.y;	
			changePosition.z = currentPosition.z;

			if (Input.GetKey (KeyCode.LeftArrow) && currentPosition.x >= (-Camera.main.orthographicSize * Camera.main.aspect + offsetX)) {
				changePosition.x -= speedX;
			} else if (Input.GetKey (KeyCode.RightArrow) && currentPosition.x <= (Camera.main.orthographicSize * Camera.main.aspect - offsetX)) {
				changePosition.x += speedX;
			}

			transform.position = changePosition;
		} else {
			if(Input.GetKey (KeyCode.Space)){
				Application.LoadLevel("MainGame");
			}
		}


	}

	void OnTriggerEnter2D( Collider2D other ){
		isGameOver = true;
		cameractrl.speedY = 0;
	}

	void OnGUI(){
		float middleScreenX = Screen.width / 2;
		float middleScreenY = Screen.height / 2;
		
		float heightText = 40;
		float widthText = 100;
		if(isGameOver){
			string test = GUI.TextArea (new Rect (middleScreenX - (widthText / 2), middleScreenY - (heightText / 2), widthText, heightText), "Game Over");
		}
	}
}
