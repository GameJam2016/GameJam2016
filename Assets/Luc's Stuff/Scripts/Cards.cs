using UnityEngine;
using System.Collections;

public class Cards : MonoBehaviour
{

 //   public int damageMin, DamageMax;
    public int playerCurrentMaxMana = 200;
    public enum typeOfCard
    {
        Offense,
        Support,
        Defense
    }


    public float damage;
    public int manacost;
    public float DefensivePower;
    public float CCduration;
    public typeOfCard typeOfThisCard;
    public attribute AttributeOfThisCard;
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
                AttributeOfThisCard = attribute.Demonic;
                break;
            case 1:
                AttributeOfThisCard = attribute.Holy;
                break;
            case 2:
                AttributeOfThisCard = attribute.Natural;
                break;
        }
    }

    // initialize all stats
    void InitializeCard()
    {
        manacost = Random.Range(1, playerCurrentMaxMana);
        typeAtrributeInitialize();
    }

    public GameObject CreateCard()
    {
        int typeOfCardRandomizer = Random.Range(0, 3);
        int AttributeCardRandomizer = Random.Range(0, 3);
        foreach (GameObject obj in spells)
        {
            Spell spell = obj.GetComponent<Spell>();
            if((int)spell.Type == typeOfCardRandomizer)
            {
                if((int)spell.Attr == AttributeCardRandomizer)
                {
                    Debug.Log("Created : " + spell.Type + " - " + spell.Attr);
                    return obj;
                }
            }
        }
        return null;
    }


    //spells
    public GameObject[] spells;



}
