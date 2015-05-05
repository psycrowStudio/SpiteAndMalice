using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;

public class CardUIController : MonoBehaviour, ISelectHandler, IDeselectHandler {
	public ActionBoxController ActionBox;
	public Button btnSelf; 
	//public int moveNumber = 0;
	//public int pileNumber = 0;
	public enum CardUIStates {idle = 0, clicked = 1, activated = 2, played = 3 }
	public CardUIStates state = CardUIStates.idle;
	public CardUIStates stateLastFrame;
	public Card cardData;

	public enum CardBehaviorPattern
	{
		Unknown = 0,
		Discard = 1,
		Deck = 2,
		Hand = 3, 
		Goal = 4,
		Play = 5
	}
	public CardBehaviorPattern currentBehavior;


	
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

		if ( EventSystem.current.currentSelectedGameObject == this.gameObject && (Input.GetKeyUp (KeyCode.Escape))) {
			//Debug.Log("!!!!!!!!!!!!");
			EventSystem.current.SetSelectedGameObject(null, new BaseEventData(EventSystem.current));

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
			//Debug.Log ("Reset Card To Idle" + " -- " + interval);
			if (this.state == CardUIStates.activated) {
				this.state = CardUIStates.idle;
			}
		} else if (clickCount == 1 && pointerData.selectedObject == this.gameObject) {
			//Debug.Log ("!!!" + " -- " + interval);
			this.ActionBox.gameObject.SetActive (true);
			this.state = CardUIStates.activated;
		} else {
			//Debug.Log(pointerData.clickCount + " -- " + interval);
		}
	}


	public void OnSelect(BaseEventData eventData)
	{
		//Debug.Log("test");
		if (this.state == CardUIStates.idle) {
			this.state = CardUIStates.activated;
			this.ActionBox.MoveNumber.text = string.Format("{0}", GameLogicManager.Instance.markedCards.Count+1);
		}
	}

	public void OnDeselect(BaseEventData eData)
	{
		//Debug.Log ("deselected");
		PointerEventData pointerData = eData as PointerEventData;
		//var pointer = new PointerEventData(EventSystem.current);
		// convert to a 2D position
		//pointer.position = po
		if (pointerData == null && this.ActionBox.PileType.sprite != null)
		{
			return;
		}
		else if(pointerData == null)
		{
			this.state = CardUIStates.idle;
			return;
		}

		var raycastResults = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerData, raycastResults);
		
		if (raycastResults.Count > 0) 
		{
			// Do anything to the hit objects. Here, I simply disable the first one.

			switch(this.currentBehavior)
			{
				case CardBehaviorPattern.Goal:
					if (raycastResults [0].gameObject.tag.Contains ("PlayStack")) {
						//this.transform.SetParent(raycastResults[0].gameObject.transform);
						//this.GetComponent<RectTransform>().localPosition = Vector3.zero;
						if(GameLogicManager.Instance.CanPlayCard(GameLogicManager.Instance.playerB.goalStack[GameLogicManager.Instance.playerB.goalStack.Count-1], Int32.Parse(raycastResults [0].gameObject.tag.Substring (raycastResults [0].gameObject.tag.Length - 1))))
						{
							MarkedCard mark = new MarkedCard(){ 
								card = GameLogicManager.Instance.playerB.goalStack[GameLogicManager.Instance.playerB.goalStack.Count-1],
								cardObj = this,
								destinationNumber = Int32.Parse(raycastResults [0].gameObject.tag.Substring (raycastResults [0].gameObject.tag.Length - 1)),
								destinationPile = raycastResults [0].gameObject.tag,
								order = GameLogicManager.Instance.markedCards.Count == 0 ? 1 : GameLogicManager.Instance.markedCards.Count
							};
							if(GameLogicManager.Instance.markedCards.IndexOf(mark) != -1)
							{
								// card already in the pile.
								return;
							}
								
							this.ActionBox.PileType.sprite = raycastResults [0].gameObject.GetComponent<Image> ().sprite;
							this.ActionBox.PileType.color = Color.white;
							this.ActionBox.PileNumber.text = raycastResults [0].gameObject.tag.Substring (raycastResults [0].gameObject.tag.Length - 1);
							GameLogicManager.Instance.AddMarkedCard(mark);
						}
						else
						{
							GameLogicManager.Instance.RemoveMarkedCard(GameLogicManager.Instance.playerB.goalStack[GameLogicManager.Instance.playerB.goalStack.Count-1]);
							this.state = CardUIStates.idle;
						}
					}
					else
					{
						GameLogicManager.Instance.RemoveMarkedCard(GameLogicManager.Instance.playerB.goalStack[GameLogicManager.Instance.playerB.goalStack.Count-1]);
						this.state = CardUIStates.idle;
					}
					break;
				case CardBehaviorPattern.Discard:
					if (raycastResults [0].gameObject.tag.Contains ("PlayStack")) {
						//this.transform.SetParent(raycastResults[0].gameObject.transform);
						//this.GetComponent<RectTransform>().localPosition = Vector3.zero;
						this.ActionBox.PileType.sprite = raycastResults [0].gameObject.GetComponent<Image> ().sprite;
						this.ActionBox.PileType.color = Color.white;
						this.ActionBox.PileNumber.text = raycastResults [0].gameObject.tag.Substring (raycastResults [0].gameObject.tag.Length - 1);
					}
					else
					{
						GameLogicManager.Instance.RemoveMarkedCard(GameLogicManager.Instance.playerB.goalStack[GameLogicManager.Instance.playerB.goalStack.Count-1]);
						this.state = CardUIStates.idle;
					}
					break;

				case CardBehaviorPattern.Hand:
				Card inHand = GameLogicManager.Instance.playerB.hand.Find((match) => { return match == this.cardData; });
				if (raycastResults [0].gameObject.tag.Contains ("PlayStack")) {
						//this.transform.SetParent(raycastResults[0].gameObject.transform);
						//this.GetComponent<RectTransform>().localPosition = Vector3.zero;

						if(GameLogicManager.Instance.CanPlayCard(inHand, Int32.Parse(raycastResults [0].gameObject.tag.Substring (raycastResults [0].gameObject.tag.Length - 1))))
						{
							MarkedCard mark = new MarkedCard(){ 
								card = inHand,
								cardObj = this,
								destinationNumber = Int32.Parse(raycastResults [0].gameObject.tag.Substring (raycastResults [0].gameObject.tag.Length - 1)),
								destinationPile = raycastResults [0].gameObject.tag,
								order = GameLogicManager.Instance.markedCards.Count == 0 ? 1 : GameLogicManager.Instance.markedCards.Count
							};
							if(GameLogicManager.Instance.markedCards.Find((c) => { return c.card == inHand && c.destinationNumber == mark.destinationNumber && c.destinationPile == mark.destinationPile; }) != null)
							{
								// card already in the pile.
								return;
							}
							this.ActionBox.PileType.sprite = raycastResults [0].gameObject.GetComponent<Image> ().sprite;
							this.ActionBox.PileType.color = Color.white;
							this.ActionBox.PileNumber.text = raycastResults [0].gameObject.tag.Substring (raycastResults [0].gameObject.tag.Length - 1);
							GameLogicManager.Instance.AddMarkedCard(mark);
						}
						else
						{
							GameLogicManager.Instance.RemoveMarkedCard(inHand);
							this.state = CardUIStates.idle;
						}
					}
					else
					{
						if (this.ActionBox.PileType.sprite != null)
						{
							return;
						}
						else
						{
							GameLogicManager.Instance.RemoveMarkedCard(inHand);
							this.state = CardUIStates.idle;
							return;
						}
					}
					break;
				default:
					GameLogicManager.Instance.RemoveMarkedCard(GameLogicManager.Instance.playerB.goalStack[GameLogicManager.Instance.playerB.goalStack.Count-1]);
					this.state = CardUIStates.idle;
					break;
				}
			}
			else // raycast hit nothing
			{
				//Debug.Log(string.Format("Hit Something: {0}", raycastResults [0].gameObject.tag));
				//GameLogicManager.Instance.RemoveMarkedCard(GameLogicManager.Instance.playerB.goalStack[GameLogicManager.Instance.playerB.goalStack.Count-1]);
				//this.state = CardUIStates.idle;
				//GameLogicManager.Instance.RemoveMarkedCard(
				if (this.ActionBox.PileType.sprite != null)
				{
					return;
				}
				else
				{
					this.state = CardUIStates.idle;
					return;
				}
			}
		}
}
