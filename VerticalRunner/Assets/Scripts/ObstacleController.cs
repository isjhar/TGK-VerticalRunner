using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {
	public Transform[] obstacles;
	public int numOfObstacle;
	public float distanceBetweenObstacle, maxWidthObstacle, 
	minWidthObstacle, minHeightObstacle, maxHeightObstacle, minOffsetY,
	maxOffsetY;

	private float obstacleLength;

	// Use this for initialization
	void Start () {
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
		obstacleLength = 0;

		minObstacleX = -Camera.main.orthographicSize * Camera.main.aspect;
		maxObstacleX = Camera.main.orthographicSize * Camera.main.aspect;
		for (int i = 0; i < numOfObstacle; i++) {
			heightObstacle = Random.Range (minHeightObstacle, maxHeightObstacle);
			widthObstalce = Random.Range (minWidthObstacle, maxWidthObstacle);
			obstacleScale = new Vector3(widthObstalce,heightObstacle,1);
			obstacles[0].transform.localScale = obstacleScale;

			offsetY = Random.Range(minOffsetY,maxOffsetY);

			obstacleX = Random.Range(minObstacleX,maxObstacleX);

			obstacleLength += (distanceBetweenObstacle + offsetY);

			obstaclePosition = new Vector3(obstacleX, obstacleLength, 0);

			Instantiate(obstacles[0], obstaclePosition, Quaternion.identity);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
