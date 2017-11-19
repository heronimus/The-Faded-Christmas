using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour {

	public Text itemListText;

	void Start(){	

	}

	public void addItemList(string itemname){
		itemListText.text += "\n"+itemname ;
	}


}
