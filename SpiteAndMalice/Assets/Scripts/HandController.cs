using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections;
using System.Collections.Generic;

public class HandController : MonoBehaviour {
	public Image Card1;
	public Image Card2;
	public Image Card3;
	public Image Card4;
	public Image Card5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateCardImages(List<Card> cards)
	{
		if (cards.Count != 5) {
		
		} else {
			this.Card1.sprite = DeckArtManager.Instance.FindCardSprite(cards[0].cardValue, cards[0].cardSuit);
			this.Card2.sprite = DeckArtManager.Instance.FindCardSprite(cards[1].cardValue, cards[1].cardSuit);
			this.Card3.sprite = DeckArtManager.Instance.FindCardSprite(cards[2].cardValue, cards[2].cardSuit);
			this.Card4.sprite = DeckArtManager.Instance.FindCardSprite(cards[3].cardValue, cards[3].cardSuit);
			this.Card5.sprite = DeckArtManager.Instance.FindCardSprite(cards[4].cardValue, cards[4].cardSuit);
		}

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
