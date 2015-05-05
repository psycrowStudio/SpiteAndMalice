using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionBoxController : MonoBehaviour {
	public Image PileType;
	public Text PileNumber;
	public Text MoveNumber;
	public Image ActionBoxBg;
	public bool ManuallyFollowParent = false;

	public RectTransform myRect;
	public RectTransform parentRect;
	public Image parentImage;

	void OnEnable()
	{
		this.PileType.sprite = null;
		this.PileType.color = Color.black;

		this.myRect = GetComponent<RectTransform> () as RectTransform;
		this.parentRect = this.transform.parent.GetComponent<RectTransform>() as RectTransform;
		this.parentImage = this.transform.parent.GetComponent<Image>() as Image;
		//Debug.Log (this.transform.parent.name);
	}

	void OnDisable()
	{
		this.PileType.sprite = null;
		this.PileType.color = Color.black;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.ManuallyFollowParent == true) {

//			Vector3[] myCorners = new Vector3[4];
//			this.myRect.GetLocalCorners(myCorners);
//
//			foreach(var corner in myCorners)
//			{
//				Debug.Log(corner.ToString());
//			}
//
//
//			
//			Vector3[] parentCorners = new Vector3[4];
//			this.parentRect.GetLocalCorners(parentCorners);
//			
//			foreach(var corner in parentCorners)
//			{
//				Debug.Log(corner.ToString());
//			}

			//float centerPos = (parentCorners[2].x)/2;
			//this.myRect.localPosition = new Vector3(centerPos, parentCorners[2].y, parentCorners[2].z);
			//this.myRect.anchoredPosition = new Vector2(.5f,1f);
			//this.myRect.rect = new Rect();
			//Debug.Log("Image: " + this.parentImage.rectTransform.rect.ToString() + " Rt:" +  this.parentRect.rect.ToString());
			//this.myRect.localPosition = new Vector3(-this.parentRect.rect.width/2, this.parentRect.rect.height/2, 0);
		}
	}

	void OnRayCastHit()
	{

	}
}
