using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoalPileController : MonoBehaviour {
	public Image goalPilePlaceholder;
	public Image downCards;
	public Image upCard;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateCardImages(Card C)
	{
		this.upCard.sprite = DeckArtManager.Instance.FindCardSprite (C.cardValue, C.cardSuit);
	}
}
