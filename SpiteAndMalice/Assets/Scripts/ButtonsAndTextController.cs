using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonsAndTextController : MonoBehaviour {
	public Text playerAGoalCount;
	public Text playerBGoalCount;
	public Text drawPileCount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void UpdateGoalStackCounts(Player A, Player B)
	{
		this.playerAGoalCount.text = string.Format("[{0}]", A.goalStack.Count);
		this.playerBGoalCount.text = string.Format("[{0}]", B.goalStack.Count);
	}

	public void UpdateDrawPileCount(int count)
	{
		this.drawPileCount.text = string.Format("[{0}]", count);
	}
}
