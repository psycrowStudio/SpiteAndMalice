using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Table : MonoBehaviour {
	public List<Card> deck = new List<Card>();
	public List<Card> discard = new List<Card> ();
	public List<Card> playStack1 = new List<Card>();
	public List<Card> playStack2 = new List<Card> ();
	public List<Card> playStack3 = new List<Card> ();
	public List<Card> playStack4 = new List<Card> ();

	public PlayStacksController playStackController;
	public DeckController deckController;
	public ButtonsAndTextController btnAndTxtController;



	// methods around the table
	// deal x count
	// start game (alternating deal?)
	// 


	public int numberOfDecks = 1;
	public bool shuffleOnStart = true;

	//public Transform TEST_CARD_PREFAB;
	public UnityEngine.UI.Image[] PlayerBCards; 
	//public int optn_baseOnDeal = 10;
	//public int optb_JacksOnDeal =  


	// Use this for initialization
	void Start () {

//		deck.AddRange (BuildStandardDeck ());
//		if (this.shuffleOnStart == true) {
//			this.deck = Shuffle (this.deck, 3);
//		}
		//Debug.Log (DeckArtManager.Instance.commonBack.comName);
		//shuffle ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if (GUILayout.Button ("Draw 5")) {
			List<Card> hand = GetCardsFromTop(5);
			int counter = 0;
			foreach(var card in hand)
			{
				Sprite sCheck = DeckArtManager.Instance.FindCardSprite(card.cardValue, card.cardSuit);
//				string status = sCheck == null ? "..." : "SpriteFound!";
				if(sCheck != null)
				{
					this.PlayerBCards[counter].sprite = sCheck;
				}
				//Debug.Log(string.Format("{0} of {1} -- SpriteStatus: {2}", card.cardValue, card.cardSuit, status));
				counter++;
			}
		}
	}


	public List<Card> BuildStandardDeck()
	{
		Debug.Log ("Building Standard Deck");
		List<Card> rtDeck = new List<Card> ();
		for(int z = 1; z <= 4; z ++)
		{
			for(int x = 1; x < 14; x++)
			{
				Card C = new Card();
				C.cardSuit = (Card.CardSuit)z;
				C.cardValue = (Card.CardValue)x;
				C.cardName =  string.Format("{0} of {1}", x, ((Card.CardSuit)z).ToString().Substring(0,1)); 
				rtDeck.Add(C);
			}
		}
		return rtDeck;
	}

	public List<Card> GetCardsFromTop(int count, bool removeFromDeck = false)
	{
		List<Card> thisObject = new List<Card> ();
		thisObject.AddRange(this.deck.GetRange(0, count));

		// move cards to bottom of pile
		this.deck.AddRange (this.deck.GetRange (0, count));
		this.deck.RemoveRange (0, count);

		return thisObject;
	}

	public List<Card> Shuffle(List<Card> d, int ruffles = 1)
	{
		Dictionary<int, Card> shuffler = new Dictionary<int, Card> ();

		for (int z = 0; z < d.Count; z++) {
			shuffler.Add(z, null);
			//deck[z].
		}

		foreach (var card in d) {
			int rng = UnityEngine.Random.Range(0, d.Count-1);
			if(shuffler[rng] == null)
			{
				//shuffler.Value = 
				shuffler[rng] = card;
			}
			else
			{
				bool saved = false;
				while(!saved)
				{
					if(rng < d.Count-1)
					{
						rng++;
					}
					else
					{
						rng = 0;
					}

					if(shuffler[rng] == null)
					{
						shuffler[rng] = card;
						saved = true;
					}
				}
			}
		}

		if (ruffles > 0) {
			return Shuffle (d, ruffles - 1);
		} else {
			return shuffler.Values.ToList ();
		}
	}



}
