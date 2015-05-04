using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public List<Card> hand = new List<Card>();
	public List<Card> discard1 = new List<Card>();
	public List<Card> discard2 = new List<Card>();
	public List<Card> discard3 = new List<Card>();
	public List<Card> discard4 = new List<Card>();
	public List<Card> goalStack = new List<Card>();


	public DiscardPilesController discardObjectsController;
	public HandController handObjectsController;
	public GoalPileController goalObjectsController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
