using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CardUIController : MonoBehaviour, ISelectHandler, IDeselectHandler {
	public ActionBoxController ActionBox;
	public Button btnSelf; 
	//public int moveNumber = 0;
	//public int pileNumber = 0;
	public enum CardUIStates {idle = 0, clicked = 1, activated = 2, played = 3 }
	public CardUIStates state = CardUIStates.idle;
	public CardUIStates stateLastFrame;

	
	private float clickTime;            // time of click


	// Use this for initialization
	void Start () {
		//btnSelf.onClick.AddListener ((e) => { this.OnClick(e); } );
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.state != this.stateLastFrame) {
			this.stateLastFrame = this.state;

			switch(state)
			{
				case CardUIStates.clicked:
						
					break;

				case CardUIStates.idle:
					this.ActionBox.gameObject.SetActive(false);
					break;

				case CardUIStates.activated:
					this.ActionBox.gameObject.SetActive(true);
					break;

				case CardUIStates.played:

					break;
			}

		}
	}

	public void HandleOnClick(BaseEventData eData)
	{
		PointerEventData pointerData = eData as PointerEventData;
		//Debug.Log (pointerData.clickCount);

		int clickCount = 1; // single click
		
		// get interval between this click and the previous one (check for double click)
		float interval = pointerData.clickTime - clickTime;
		
		// if this is double click, change click count
		if (interval < .5f && interval > .05f)
			clickCount = 2;
		
		// reset click time
		clickTime = pointerData.clickTime;
		
		// double click
		if (clickCount == 2 && this.state != CardUIStates.idle) {
			// enter code here
			Debug.Log ("Reset Card To Idle" + " -- " + interval);
			if (this.state == CardUIStates.activated) {
				this.state = CardUIStates.idle;
			}
		} else if (clickCount == 1 && pointerData.selectedObject == this.gameObject) {
			Debug.Log ("!!!" + " -- " + interval);
			this.ActionBox.gameObject.SetActive (true);
			this.state = CardUIStates.activated;
		} else {
			//Debug.Log(pointerData.clickCount + " -- " + interval);
		}
	}


	public void OnSelect(BaseEventData eventData)
	{
		Debug.Log("test");
		if (this.state == CardUIStates.idle) {
			this.state = CardUIStates.activated;
		}


		this.ActionBox.MoveNumber.text = string.Format("{0}", Random.Range (20, 29));
	}

	public void OnDeselect(BaseEventData eData)
	{
		Debug.Log ("deselected");
		PointerEventData pointerData = eData as PointerEventData;
		//var pointer = new PointerEventData(EventSystem.current);
		// convert to a 2D position
		//pointer.position = po
		
		var raycastResults = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerData, raycastResults);
		
		if (raycastResults.Count > 0) {
			// Do anything to the hit objects. Here, I simply disable the first one.


			if (raycastResults [0].gameObject.tag.Contains ("MyDiscard")) {
				this.ActionBox.PileType.sprite = raycastResults [0].gameObject.GetComponent<Image> ().sprite;
				this.ActionBox.PileType.color = Color.white;
				this.ActionBox.PileNumber.text = raycastResults [0].gameObject.tag.Substring (raycastResults [0].gameObject.tag.Length - 1);
				//this.transform.SetParent(raycastResults[0].gameObject.transform);
				//this.GetComponent<RectTransform>().localPosition = Vector3.zero;

			}
			else if (raycastResults [0].gameObject.tag.Contains ("PlayStack")) {
				//this.transform.SetParent(raycastResults[0].gameObject.transform);
				//this.GetComponent<RectTransform>().localPosition = Vector3.zero;
				this.ActionBox.PileType.sprite = raycastResults [0].gameObject.GetComponent<Image> ().sprite;
				this.ActionBox.PileType.color = Color.white;
				this.ActionBox.PileNumber.text = raycastResults [0].gameObject.tag.Substring (raycastResults [0].gameObject.tag.Length - 1);
			}
			else
			{
				Debug.Log(string.Format("Hit Something: {0}", raycastResults [0].gameObject.tag));
				this.state = CardUIStates.idle;
			}

		}
		else {
			this.state = CardUIStates.idle;
		}

	}

}
