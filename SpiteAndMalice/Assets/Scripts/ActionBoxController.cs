using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionBoxController : MonoBehaviour {
	public Image PileType;
	public Text PileNumber;
	public Text MoveNumber;
	public Image ActionBoxBg;

	void OnEnable()
	{
		this.PileType.sprite = null;
		this.PileType.color = Color.black;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnRayCastHit()
	{

	}
}
