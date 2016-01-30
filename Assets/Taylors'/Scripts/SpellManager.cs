using UnityEngine;
using System.Collections;
using UnityEngine.UI;


//This is a UI Script

public class SpellManager : MonoBehaviour
{
    public int[] SpellSlots = {1, 0, 0, 0, 0, 0};
    public Image[] SpellWheelImageArray;
    public GameObject[] Spells;

    [SerializeField]private bool canChangeSpell = true;

    public Image RevolverUI;

    [SerializeField]private int CurrentRevolverSlot = 0;


    public Animator RevolverAnimator;
	// Use this for initialization
	void Start ()
    {
        UpdateSpellSlots();
        //IncreaseSpellRevolverSize();
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
                ResetAnimationValues();
                break;
            }
        }
    }

    //Rotates the UI of the revolver thing and calls the attack function of the ability
    void RotateAbilityRevolver()
    {
        //This changes the revolvers 
        if (InputManager.Instance.GetKeyDown("RevolverLeft") && canChangeSpell == true)//right
        {
            canChangeSpell = false;
            StartCoroutine(SpellChangeDelay());
            switch (CurrentRevolverSlot)
            {
                case 0:
                    if (SpellSlots[CurrentRevolverSlot + 1] != 0)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 2);
                        RevolverAnimator.SetInteger("MoveingFrom", 1);
                        CurrentRevolverSlot += 1;
                    }
                    break;
                case 1:
                    if (SpellSlots[CurrentRevolverSlot + 1] != 0)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 3);
                        RevolverAnimator.SetInteger("MoveingFrom", 2);
                        CurrentRevolverSlot += 1;
                    }
                    else
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 1);
                        RevolverAnimator.SetInteger("MoveingFrom", 2);
                        RevolverAnimator.SetBool("Moveleft", true);
                        CurrentRevolverSlot = 0;
                    }
                    break;
                case 2:
                    if (SpellSlots[CurrentRevolverSlot + 1] != 0)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 4);
                        RevolverAnimator.SetInteger("MoveingFrom", 3);
                        CurrentRevolverSlot += 1;
                    }
                    else
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 1);
                        RevolverAnimator.SetInteger("MoveingFrom", 3);
                        RevolverAnimator.SetBool("Moveleft", true);
                        CurrentRevolverSlot = 0;
                    }
                    break;
                case 3:
                    if (SpellSlots[CurrentRevolverSlot + 1] != 0)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 5);
                        RevolverAnimator.SetInteger("MoveingFrom", 4);
                        CurrentRevolverSlot += 1;
                    }
                    else
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 1);
                        RevolverAnimator.SetInteger("MoveingFrom", 4);
                        RevolverAnimator.SetBool("Moveleft", true);
                        CurrentRevolverSlot = 0;
                    }
                    break;
                case 4:
                    if (SpellSlots[CurrentRevolverSlot + 1] != 0)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 6);
                        RevolverAnimator.SetInteger("MoveingFrom", 5);
                        CurrentRevolverSlot += 1;
                    }
                    else
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 1);
                        RevolverAnimator.SetInteger("MoveingFrom", 5);
                        RevolverAnimator.SetBool("Moveleft", true);
                        CurrentRevolverSlot = 0;
                    }
                    break;
                case 5:
                    if (SpellSlots[CurrentRevolverSlot - 5] != 0)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 1);
                        RevolverAnimator.SetInteger("MoveingFrom", 6);
                        CurrentRevolverSlot = 0;
                    }
                    break;
            }
        }
        else if (InputManager.Instance.GetKeyDown("RevolverRight") && canChangeSpell == true)//left
        {
            canChangeSpell = false;
            StartCoroutine(SpellChangeDelay());
            switch (CurrentRevolverSlot )
            {
                case 0:
                    if (SpellSlots[CurrentRevolverSlot + 5] != 0)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 6);
                        RevolverAnimator.SetInteger("MoveingFrom", 1);
                        RevolverAnimator.SetBool("Moveleft", true);
                        CurrentRevolverSlot = 5;
                    }
                    else if (SpellSlots[CurrentRevolverSlot + 4] != 0)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 5);
                        RevolverAnimator.SetInteger("MoveingFrom", 1);
                        RevolverAnimator.SetBool("Moveleft", true);
                        CurrentRevolverSlot = 4;
                    }
                    else if (SpellSlots[CurrentRevolverSlot + 3] != 0)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 4);
                        RevolverAnimator.SetInteger("MoveingFrom", 1);
                        RevolverAnimator.SetBool("Moveleft", true);
                        CurrentRevolverSlot = 3;
                    }
                    else if (SpellSlots[CurrentRevolverSlot + 2] != 0)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 3);
                        RevolverAnimator.SetInteger("MoveingFrom", 1);
                        RevolverAnimator.SetBool("Moveleft", true);
                        CurrentRevolverSlot = 2;
                    }
                    else if (SpellSlots[CurrentRevolverSlot + 1] != 0)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 2);
                        RevolverAnimator.SetInteger("MoveingFrom", 1);
                        RevolverAnimator.SetBool("Moveleft", true);
                        CurrentRevolverSlot = 1;
                    }
                    break;
                case 1:
                    if (SpellSlots[CurrentRevolverSlot - 1] == 1)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 1);
                        RevolverAnimator.SetInteger("MoveingFrom", 2);
                        CurrentRevolverSlot -= 1;
                    }
                    break;
                case 2:
                    if (SpellSlots[CurrentRevolverSlot - 1] == 1)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 2);
                        RevolverAnimator.SetInteger("MoveingFrom", 3);
                        CurrentRevolverSlot -= 1;
                    }
                    break;
                case 3:
                    if (SpellSlots[CurrentRevolverSlot - 1] == 1)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 3);
                        RevolverAnimator.SetInteger("MoveingFrom", 4);
                        CurrentRevolverSlot -= 1;
                    }
                    break;
                case 4:
                    if (SpellSlots[CurrentRevolverSlot - 1] == 1)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 4);
                        RevolverAnimator.SetInteger("MoveingFrom", 5);
                        CurrentRevolverSlot -= 1;
                    }
                    break;
                case 5:
                    if (SpellSlots[CurrentRevolverSlot - 1] == 1)
                    {
                        RevolverAnimator.SetInteger("MoveingTo", 5);
                        RevolverAnimator.SetInteger("MoveingFrom", 6);
                        CurrentRevolverSlot -= 1;
                    }
                    break;
            }
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

    public void ResetAnimationValues()
    {
        RevolverAnimator.SetInteger("MoveingTo", 0);
        RevolverAnimator.SetInteger("MoveingFrom", 0);
        RevolverAnimator.SetBool("Moveleft", false);
    }

    public void SavePositionOfRevolver()
    {
        switch(CurrentRevolverSlot)
        {
            case 0:
                this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                break;
            case 1:
                this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 60.0f);
                break;
            case 2:
                this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 120.0f);
                break;
            case 3:
                this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                break;
            case 4:
                this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 240.0f);
                break;
            case 5:
                this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 360.0f);
                break;
        }
    }

    IEnumerator SpellChangeDelay()
    {
        yield return new WaitForSeconds(1.0f);
        canChangeSpell = true;
    }
}
