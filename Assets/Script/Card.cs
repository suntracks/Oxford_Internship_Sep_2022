using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Card 
{
    // Start is called before the first frame update
public string name;
 public int id;
public Sprite thisImage;

public Card(){

}

public Card(string Name, Sprite ThisImage){
    //id = ID;
	name = Name;
	thisImage = ThisImage;
}


   
}
