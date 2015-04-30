using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[System.Serializable]
public class Card {

	public string cardName;
	public enum CardValue {Joker = 0, Ace = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13};
	public CardValue cardValue = CardValue.Ace;
	public enum CardSuit {None = 0, Clubs = 1, Diamonds = 2, Hearts = 3, Spades = 4 };
	public CardSuit cardSuit = CardSuit.Clubs;
	public EventTrigger trigger;
	public Transform sceneObject;
}
