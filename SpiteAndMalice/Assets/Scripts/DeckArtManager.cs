using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeckArtManager : Singleton<DeckArtManager> {
	protected DeckArtManager (){}
	public DeckArtItem[] commonFronts;
	public DeckArtItem commonBack;
	//for use later 
	//public DeckArtItem altBack;

	// Use this for initialization
	void Start () {
		Object.DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Sprite FindCardSprite(Card.CardValue v, Card.CardSuit s)
	{
		foreach (var item in commonFronts) {
			if(item.suit == s && item.value == v)
			{
				return item.faceArt;
			}
		}
		return null;
	}
}


[System.Serializable]
public class DeckArtItem
{
	public string comName;
	public Sprite faceArt;
	public Card.CardSuit suit;
	public Card.CardValue value;
}