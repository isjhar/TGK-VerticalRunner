using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float speedY;
	private Vector3 currentPosition, newPosition;
	private float pointToStop;
	private CharacterController charctrl;

	public Transform background;
	public Transform[] obstacles;

	private ArrayList bgInstant;
	private float currentBackgroundLength, height, nextGeneratePosition;



	private ArrayList obstacleInstant;
	private ArrayList list;
	public float distanceBetweenObstacle, maxWidthObstacle, 
	minWidthObstacle, minHeightObstacle, maxHeightObstacle, minOffsetY,
	maxOffsetY;
	
	private float obstacleLength;
	

	// Use this for initialization
	void Start () {
		// init background
		currentBackgroundLength = 0;
		bgInstant = new ArrayList ();
		height = Camera.main.orthographicSize * 2;
		list = new ArrayList ();
		obstacleLength = 0;
		for (int i = 0; i < 3; i++) {
			bgInstant.Add (Instantiate(background,new Vector3(0, currentBackgroundLength, 0), Quaternion.identity));
			currentBackgroundLength += height;	
			obstacleInstant = new ArrayList ();
			do {
				CreateObstacle();		
			} while (obstacleLength + maxOffsetY + maxHeightObstacle < currentBackgroundLength - (height / 2));
			list.Add(obstacleInstant);
		}
		nextGeneratePosition = height;


	}
	
	// Update is called once per frame
	void Update () {
		Transform temp;
		if (transform.position.y > nextGeneratePosition + 5) {
			// create new background
			bgInstant.Add (Instantiate(background, new Vector3(0, currentBackgroundLength, 0), Quaternion.identity));
			currentBackgroundLength += height;
			nextGeneratePosition += height;
			temp = (Transform) bgInstant[0];
			bgInstant.RemoveAt(0);
			Destroy(temp.gameObject);

			// create new obstacle
			obstacleInstant = new ArrayList ();
			do {
				CreateObstacle();		
			} while (obstacleLength + maxOffsetY + maxHeightObstacle < currentBackgroundLength - (height / 2));
			list.Add(obstacleInstant);

			ArrayList tempList = (ArrayList) list[0];
			for(int i = 0;i < tempList.Count;i++){
				temp = (Transform) tempList[i];
				Destroy(temp.gameObject);
			}
			list.RemoveAt(0);
		}

		currentPosition = transform.position;
		newPosition = new Vector3 (currentPosition.x, currentPosition.y + speedY, currentPosition.z);
		transform.position = newPosition;
	}

	void CreateObstacle(){
		/* inisialisasi posisi y default
		 * inisialisasi panjang obstacle
		 * lakukan dibawah ini sesuai dengan jumlah obstacle
		 * 	generate ukuran obstacle;
		 *  generate posisi y offset
		 * 	generate posisi x dengan rentang sesuai dengan ukuran layar
		 * 	total panjangobstacle = dari hasil generate offset + pertambahan default y
		 * 	buat object obstacle seusai dengan x dan y yang telah digenerate
		 * 	
		 */
		
		float widthObstalce, heightObstacle, minObstacleX, maxObstacleX,
		offsetY, obstacleX;
		Vector3 obstacleScale, obstaclePosition;

		int indexObstacleSelected = Random.Range (0, obstacles.Length);

		minObstacleX = -Camera.main.orthographicSize * Camera.main.aspect;
		maxObstacleX = Camera.main.orthographicSize * Camera.main.aspect;

		heightObstacle = Random.Range (minHeightObstacle, maxHeightObstacle);
		widthObstalce = Random.Range (minWidthObstacle, maxWidthObstacle);
		obstacleScale = new Vector3(widthObstalce,heightObstacle,1);

		obstacles[indexObstacleSelected].transform.localScale = obstacleScale;
		
		offsetY = Random.Range(minOffsetY,maxOffsetY);
		
		obstacleX = Random.Range(minObstacleX,maxObstacleX);
		
		obstacleLength += (distanceBetweenObstacle + offsetY);
		
		obstaclePosition = new Vector3(obstacleX, obstacleLength, 0);


		obstacleInstant.Add(Instantiate(obstacles[indexObstacleSelected], obstaclePosition, Quaternion.identity));
	}





}
