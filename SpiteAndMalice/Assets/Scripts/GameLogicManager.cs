using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogicManager : Singleton<GameLogicManager> {
	protected GameLogicManager (){}

	public Table table;
	public Player playerA;
	public Player playerB;
	public Card joker;

	public enum GameStates {unknown = 0, starting = 1, paused = 2, playerATurn = 3, playerBTurn = 4  } //, reshuffle = 5, 
	public GameStates currentState = GameStates.unknown;

	public List<MarkedCard> markedCards = new List<MarkedCard>();


	//who's turn
	// route game events
	// turn on or off stats (backend)
	// 

	// Use this for initialization
	void Start () {
		this.SetUpGame();
	}

	// Update is called once per frame
	void Update () {
	}

	public void SetUpGame()
	{
		table.deck.AddRange (table.BuildStandardDeck ());
		table.deck = table.Shuffle (table.deck, 3);

		List<Card> altDeck = table.BuildStandardDeck ();
		altDeck.Add (joker);
		altDeck.Add (joker);
		altDeck = table.Shuffle (altDeck, 3);

		this.playerA.hand.Clear();
		this.playerB.hand.Clear ();

		// deal player hands
		for (int z = 0; z < 10; z++) {
			if(z % 2 == 0)
			{
				this.playerA.hand.Add(altDeck[z]);
			}
			else
			{
				this.playerB.hand.Add(altDeck[z]);
			}
		}
		altDeck.RemoveRange(0, 10);

		for (int z = 0; z < 44; z++) {
			if(z % 2 == 0)
			{
				this.playerA.goalStack.Add(altDeck[z]);
			}
			else
			{
				this.playerB.goalStack.Add(altDeck[z]);
			}
		}
		altDeck.RemoveRange(0, 44);
		//this.playerA.handObjectsController.UpdateCardImages (this.playerA.hand);
		this.playerB.handObjectsController.UpdateCardImages (this.playerB.hand);

		this.playerA.goalObjectsController.UpdateCardImages (this.playerA.goalStack [this.playerA.goalStack.Count - 1]);
		this.playerB.goalObjectsController.UpdateCardImages (this.playerB.goalStack [this.playerB.goalStack.Count - 1]);

		this.table.btnAndTxtController.UpdateGoalStackCounts (this.playerA, this.playerB);
		this.table.btnAndTxtController.UpdateDrawPileCount (this.table.deck.Count);
	}


	public void DrawCards()
	{
		Debug.Log ("Drawing cards...");
	}

	public void PlayMarkedCards()
	{
		Debug.Log ("Playing Marked Cards...");
	}

	public void PassTurn()
	{
		Debug.Log ("Passing turn... (THIS SHOULD PROMPT BEFORE ACTIVATING)");
	}

	public void AddMarkedCard(MarkedCard card)
	{
		//card.order = this.markedCards.Count;
		int mc = this.markedCards.IndexOf (card);
		if (mc < 0) {
			this.markedCards.Add (card);
			SortMarkedCards ();
		}
		//return card.order;
	}

	public void RemoveMarkedCard(Card card)
	{
		MarkedCard mc = this.markedCards.Find ((c) => {
			return c.card == card; });
		if (mc != null) {
			this.markedCards.Remove(mc);
			SortMarkedCards ();
		}
	}

	public void SortMarkedCards()
	{
		//reorder
		this.markedCards.Sort ((x,y) => {
			if (x.order > y.order && x.destinationPile != "MyDiscard") return 1;
			else if (x.order == y.order) return 0;
			else return -1;
		});

		// renumber
		int count = 0;
		foreach (var card in this.markedCards) {
			count++;
			card.order = count;
			// write back to the UI object
			card.cardObj.ActionBox.MoveNumber.text = string.Format("{0}", count);
		}
	}

	public bool CanPlayCard(Card card, int pile)
	{
		switch (pile) {
			case 1:
				if(this.table.playStack1.Count > 0)
				{
					if( card.cardValue == Card.CardValue.Joker && (this.table.playStack1[this.table.playStack1.Count-1].cardValue != Card.CardValue.Ace || this.table.playStack1[this.table.playStack1.Count-1].cardValue != Card.CardValue.King))
					{
						return true;
					}
					else if( (int)this.table.playStack1[this.table.playStack1.Count-1].cardValue == ((int)card.cardValue)-1) // TODO: add joker support 
					{
						// check if King, and trigger clean-up on the end of turn? or immediately?
						return true;
					}
					else
					{
						return false;
					}
				}
				else if(this.table.playStack1.Count == 0 && card.cardValue == Card.CardValue.Ace)
				{
					return true;
				}
				else if(this.markedCards.Find((e) => { return e.destinationNumber == 1 && (int)e.card.cardValue == ((int)card.cardValue)-1; } ) != null)
				{
					return true;
				}
				else
				{
					return false;
				}

			case 2:
				
				if(this.table.playStack2.Count > 0)
				{
					if( card.cardValue == Card.CardValue.Joker && (this.table.playStack2[this.table.playStack2.Count-1].cardValue != Card.CardValue.Ace || this.table.playStack2[this.table.playStack2.Count-1].cardValue != Card.CardValue.King))
					{
						return true;
					}
					else if( (int)this.table.playStack2[this.table.playStack2.Count-1].cardValue == ((int)card.cardValue)-1)
					{
						// check if King, and trigger clean-up on the end of turn? or immediately?
						return true;
					}
					else
					{
						return false;
					}
				}
				else if(this.table.playStack2.Count == 0 && card.cardValue == Card.CardValue.Ace)
				{
					return true;
				}
				else
				{
					return false;
				}

				case 3:
					
					if(this.table.playStack3.Count > 0)
					{
						if( card.cardValue == Card.CardValue.Joker && (this.table.playStack3[this.table.playStack3.Count-1].cardValue != Card.CardValue.Ace || this.table.playStack3[this.table.playStack3.Count-1].cardValue != Card.CardValue.King))
						{
							return true;
						}
						else if( (int)this.table.playStack3[this.table.playStack3.Count-1].cardValue == ((int)card.cardValue)-1)
						{
							// check if King, and trigger clean-up on the end of turn? or immediately?
							return true;
						}
						else
						{
							return false;
						}
					}
					else if(this.table.playStack3.Count == 0 && card.cardValue == Card.CardValue.Ace)
					{
						return true;
					}
					else
					{
						return false;
					}

				case 4:
					
					if(this.table.playStack4.Count > 0)
					{
						if( card.cardValue == Card.CardValue.Joker && (this.table.playStack4[this.table.playStack4.Count-1].cardValue != Card.CardValue.Ace || this.table.playStack4[this.table.playStack4.Count-1].cardValue != Card.CardValue.King))
						{
							return true;
						}
						else if( (int)this.table.playStack4[this.table.playStack4.Count-1].cardValue == ((int)card.cardValue)-1)
						{
							// check if King, and trigger clean-up on the end of turn? or immediately?
							return true;
						}
						else
						{
							return false;
						}
					}
					else if(this.table.playStack4.Count == 0 && card.cardValue == Card.CardValue.Ace)
					{
						return true;
					}
					else
					{
						return false;
					}
		}
		return false;
	}
	
	public bool CanDiscardCard(Card card, int pile)
	{
	
		return false;
	}

}


public class MarkedCard
{
	public Card card { get; set; }
	public CardUIController cardObj { get; set; }
	public int destinationNumber { get; set; }
	public string destinationPile { get; set; }
	public int order { get; set; }

}