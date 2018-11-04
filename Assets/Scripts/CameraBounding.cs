using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounding : MonoBehaviour
{
	public float minCamSpeed = 0.1f;
	public float lerpSpeed = 0.1f;
	public float camZoomSpeed = 0.1f;
	public float minPadding = 2f;
	public float maxPadding = 10f;
	
	private Camera cam;
	private GameObject[] players;
	
	void Awake ()
	{
		cam = GetComponent<Camera>();
		players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Moving the camera
		Vector3 averageCenter = Vector3.zero;
		Vector2 maxPos = Vector2.negativeInfinity;
		Vector2 minPos = Vector2.positiveInfinity;
		foreach (GameObject go in players)
		{
			averageCenter += go.transform.position;
			
			maxPos.x = go.transform.position.x > maxPos.x ? go.transform.position.x : maxPos.x;
			maxPos.y = go.transform.position.y > maxPos.y ? go.transform.position.y : maxPos.y;
			minPos.x = go.transform.position.x < minPos.x ? go.transform.position.x : minPos.x;
			minPos.y = go.transform.position.y < minPos.y ? go.transform.position.y : minPos.y;
		}
		averageCenter /= 4f;
		
		Vector3 moveVec = Vector2.Lerp(cam.transform.position, averageCenter, lerpSpeed);
		if (moveVec.magnitude < minCamSpeed)
		{
			moveVec /= moveVec.magnitude;
			moveVec *= minCamSpeed;
		}
		
		moveVec.z = -10f;
		cam.transform.position = moveVec;
		
		
		// Resizing the box
		Vector3 BR = cam.ViewportToWorldPoint(new Vector3(0, 0, 10));
		Vector3 TL = cam.ViewportToWorldPoint(new Vector3(1, 1, 10));
		
		float a = TL.x - maxPos.x;
		float b = TL.y - maxPos.y;
		float c = minPos.x - BR.x;
		float d = minPos.y - BR.y;
		
		if (a < minPadding || b < minPadding || c < minPadding || d < minPadding)
		{
			cam.orthographicSize += camZoomSpeed;
		}
		else if (a > maxPadding || b > maxPadding || c > maxPadding || d > maxPadding)
		{
			if (a > minPadding + minCamSpeed && b > minPadding + minCamSpeed && c > minPadding + minCamSpeed && d > minPadding + minCamSpeed)
			{
				cam.orthographicSize -= camZoomSpeed;
			}
		}
	}
}
