using UnityEngine;
using System.Collections;

public class Cards : MonoBehaviour {

 //   public int damageMin, DamageMax;
    public int playerCurrentMaxMana = 200;
    
    public enum typeOfCard
    {
        Offense,
        Support,
        Defense
    }
    public enum AttributeOfCard
    {
        Holy,
        Denomic,
        Natural,
    }

    public int damage;
    public int manacost;
    public int DefensivePower;
    public float CCduration;
    public typeOfCard typeOfThisCard;
    public AttributeOfCard AttributeOfThisCard;
    public string discription;
    
    public void spell()
    {

    }

	void Start () {
        InitializeCard();
    	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // initialiizes the type of card and attribute of the card
    void typeAtrributeInitialize()
    {
        int typeOfCardRandomizer = Random.Range(0, 2);
        int AttributeCardRandomizer = Random.Range(0, 2);
       
        switch (typeOfCardRandomizer)
        {
           case 0:
                typeOfThisCard = typeOfCard.Offense;
                damage = manacost * 5;
                break;
           case 1:
                typeOfThisCard = typeOfCard.Support;
                CCduration = manacost * 5;
                break;
           case 2:
                typeOfThisCard = typeOfCard.Defense;
                DefensivePower = manacost * 5;
                break;
        }
        
        switch (AttributeCardRandomizer)
        {
            case 0:
                AttributeOfThisCard = AttributeOfCard.Denomic;
                break;
            case 1:
                   AttributeOfThisCard = AttributeOfCard.Holy;
                break;
            case 2:
                   AttributeOfThisCard = AttributeOfCard.Natural;
                break;
        }
    }

    // initialize all stats
    void InitializeCard()
    {
        manacost = Random.Range(1, playerCurrentMaxMana);
        typeAtrributeInitialize();
    }
}
