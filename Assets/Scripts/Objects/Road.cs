using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class Road : MonoBehaviour {

	public Sprite activeSprite;
	public Sprite normalSprite;
	private SpriteRenderer renderer;

	public bool _interactiveRoad;
	public bool twoWayRoad;
	public PathManager pathContainer;
	public List<RoadContinuation> Continuations;

	private float animationDelay = 1f;

	// Use this for initialization
	void Start () {
		if ( null == pathContainer ) {
			// Through the exception
			Debug.LogError ( "Path Container for Road: "+name+" is empty!" );
		}
		if ( null == activeSprite ) {
			// Through the exception
			Debug.LogError ( "Sprite that indicates the activity of Road for: "+name+" is missing!" );
		}
	}

	public void StartRoadInteractivity() {
		_interactiveRoad = true;
		StartRoadAnimation ();
	}

	public void StopRoadInteractivity() {
		_interactiveRoad = false;
		StopRoadAnimation ();
	}

	public void StartRoadAnimation () {
		StartCoroutine ( "RoadAnimation" );
	}

	IEnumerator RoadAnimation () {
		while ( _interactiveRoad ) {
			if ( normalSprite == GetComponent<SpriteRenderer>().sprite ) {
				GetComponent<SpriteRenderer>().sprite = activeSprite;

			} else {
				GetComponent<SpriteRenderer>().sprite  = normalSprite;
			}

			yield return new WaitForSeconds ( animationDelay );
		}

		GetComponent<SpriteRenderer>().sprite  = normalSprite;
	}

	public void StopRoadAnimation () {
		_interactiveRoad = false;
		StopAllCoroutines ();

	}

	public bool IsIntarictiveRoad() {
		return _interactiveRoad;
	}
}
