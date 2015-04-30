using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TestingGUIEvents : MonoBehaviour {
	//public Transform cards;
	public EventTrigger[] cards;
	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RaiseAndSpreadCards()
	{
		//Debug.Log ("Pointer Enter");
		Vector2 aPos = this.GetComponent<RectTransform> ().anchoredPosition;
		this.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (aPos.x, aPos.y + 50);
		this.GetComponent<HorizontalLayoutGroup> ().spacing = -100;
	}

	public void LowerAndCollapseCards()
	{
		//Debug.Log ("Pointer Exit");
		Vector2 aPos = this.GetComponent<RectTransform> ().anchoredPosition;
		this.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (aPos.x, aPos.y - 50);
		this.GetComponent<HorizontalLayoutGroup> ().spacing = -300;
	}

	public void OnMouseOverCard(BaseEventData eData)
	{
		//Debug.Log("Caller: "+ eData.selectedObject.name);
	}

	public void OnMouseExitCard(BaseEventData eData)
	{
		//Debug.Log (eData.selectedObject);

		//Debug.Log("Caller: "+ eData.selectedObject.name);
	}


}
