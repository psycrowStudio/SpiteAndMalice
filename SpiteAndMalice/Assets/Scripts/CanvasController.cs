using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasController : MonoBehaviour {
	public Canvas canvas;

	// Use this for initialization
	void Start () {
		//GetComponent<Canvas>()
		canvas.overrideSorting = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
