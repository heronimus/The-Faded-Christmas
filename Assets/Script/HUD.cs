using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour {

	public Slider playerSpiritSlider;
	public Text itemListText;
	public Text presentsList;
	public Text infoLargeText;
	public Text infoSmallText;
	public int maxBox;

	public GameObject infoLarge;
	public GameObject infoSmall;
	int playerHealth =3;
	int currentBox = 0;
	List<string> ItemInventory = new List<string>();

	void Start(){
		showBoxList ();
	}

	public void spiritMinus(){
		playerSpiritSlider.value--;
		if (playerSpiritSlider.value <= 0)
			showInfoSmall ("Game Over :(");
	}

	public void spiritPlus(){
		playerSpiritSlider.value++;
	}

	public void healthMinus(){
		GameObject.Find ("heart" + playerHealth).SetActive (false);
		playerHealth--;
		if (playerHealth <= 0)
			showInfoSmall ("Game Over :(");
	}

	public void addItemList(string itemname){
		ItemInventory.Add (itemname);
		showItemList ();
	}
		
	public bool getBoxComplete(){
		if (currentBox >= maxBox)
			return true;
		return false;
	}
	public void addBox(){
		currentBox++;
		showBoxList ();
	}

	public void showItemList(){
		if (ItemInventory.Count == 0)
			itemListText.text = "";
		else
			itemListText.text = " - "+string.Join ("\n - ", ItemInventory.ToArray());
	}

	public void showBoxList(){
		presentsList.text = currentBox+"/"+maxBox;
	}

	public void showInfoLarge(string textinfo){
		infoLarge.SetActive (true);
		infoLargeText.text = textinfo;
	}

	public void showInfoSmall(string textinfo){
		infoSmall.SetActive (true);
		infoSmallText.text = textinfo;
	}

	public void hideInfo(){
		infoLarge.SetActive (false);
		infoSmall.SetActive (false);
	}

	public bool checkItems(string itemname){
		if (ItemInventory.Contains(itemname)){
			return true;
		} else {
			return false;
		}
	}

	public void deleteItem(string itemname){
		if (ItemInventory.Contains(itemname)){
			ItemInventory.Remove (itemname);
		} 
		showItemList ();
	}

	public void cheat(){
		currentBox=maxBox;
		showBoxList ();
	}

	public void cheat2(){
		currentBox=maxBox;
		showBoxList ();
		addItemList("Car Key");

	}


}
