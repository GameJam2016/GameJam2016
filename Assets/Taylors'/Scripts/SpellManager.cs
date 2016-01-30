using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//This is a UI Script

public class SpellManager : MonoBehaviour
{
    public int[] SpellSlots = {1, 0, 0, 0, 0, 0};
    public Image[] SpellWheelImageArray;
    public GameObject[] Spells;

    public Image RevolverUI;

    private int CurrentRevolverSlot = 1;

    public Animator RevolverAnimator;
	// Use this for initialization
	void Start ()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        UpdateSpellSlots();
        IncreaseSpellRevolverSize();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.A))
        {
            IncreaseSpellRevolverSize();
        }

        RotateAbilityRevolver();
	}

    //Increases the amount of spells you can have available to you
    void IncreaseSpellRevolverSize()
    {
        for (int i = 0; i < SpellSlots.Length; i++)
        {
            if (SpellSlots[i] != 1)
            {
                SpellSlots[i] = 1;
                UpdateSpellSlots();
                break;
            }
        }
    }

    //Rotates the UI of the revolver thing and calls the attack function of the ability
    void RotateAbilityRevolver()
    {
        if(Input.GetKeyDown(KeyCode.P))//right
        {
            switch (CurrentRevolverSlot)
            {
                case 1:
                    RevolverAnimator.SetInteger("MoveingTo", 2);
                    RevolverAnimator.SetInteger("MoveingFrom", 1);
                    break;
            }

            
        }
        else if(Input.GetKeyDown(KeyCode.O))//left
        {

        }
    }

    //Updates the spell slots available
    public void UpdateSpellSlots()
    {
        for(int i = 0; i<SpellSlots.Length; i++)
        {
            if(SpellSlots[i] == 1)
            {
                SpellWheelImageArray[i].enabled = true;
            }
            else
            {
                SpellWheelImageArray[i].enabled = false;
            }
        }
    }
}
