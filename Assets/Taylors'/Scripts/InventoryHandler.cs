using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventoryHandler : MonoBehaviour
{

    public GameObject[] InvetoryButtons;
    public GameObject[] EquiptedSpells;
    public GameObject ButtonTemplate;
    public GameObject Canvas, SelectedSpell, SpellManagerObject, Player, HoldLastVerionOfButton;
    public Text DesciptionText;
    public float newButtonX = -150;
    public float newButtonY = 150;

    public Sprite testSprite;

    // Use this for initialization
    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        float CanvasWidth = Canvas.GetComponent<RectTransform>().rect.width;
        float CanvasHeight = Canvas.GetComponent<RectTransform>().rect.height;

        StartInventory();

        LoadSpellsFromInvetory();
        //AvailableSpellSlots();
        
        EventSystem.current.SetSelectedGameObject(InvetoryButtons[0]);
    }
    
    void Update()
    {
        CancelSelectedSpell();

        if(InputManager.Instance.GetKeyDown("ExitInventory"))
        {
            SpellManagerObject.GetComponent<SpellManager>().InventoryOpen = false;
            FinalizeCustomizationOfSpells();
        }

    }

    public void StartInventory()
    {
        
        for (int i = 0; i < InvetoryButtons.Length; i++)
        {
            if (Player.GetComponent<PlayerStatus>().MySpells[i])
            {
                InvetoryButtons[i].GetComponent<Image>().sprite = Player.GetComponent<PlayerStatus>().MySpells[i].GetComponent<Spell>().SpellImage.sprite;
               
            }
        }
        AvailableSpellSlots();
    }

   public void OnClickSelectedSpell()
   {
        if(SelectedSpell && EventSystem.current.currentSelectedGameObject.tag == "Spell")
        {
            SelectedSpell.GetComponent<Image>().color = new Color(255.0f, 255.0f, 1.0f, 255.0f);
            SelectedSpell = InvetoryButtons[int.Parse(EventSystem.current.currentSelectedGameObject.name)];
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = new Color(255.0f, 255.0f, 1.0f, 0.1f);
            SelectedSpell.GetComponent<Image>().sprite = InvetoryButtons[int.Parse(EventSystem.current.currentSelectedGameObject.name)].GetComponent<Image>().sprite;
            //change the line below to the actual description string from the spell object array/inventory array
            DesciptionText.text = EventSystem.current.currentSelectedGameObject.name;
        }
        else
        {
            HoldLastVerionOfButton.GetComponent<Image>().color = new Color(255.0f, 255.0f, 1.0f, 255.0f);
            SelectedSpell = InvetoryButtons[int.Parse(EventSystem.current.currentSelectedGameObject.name)];
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = new Color(255.0f, 255.0f, 1.0f, 0.1f);
            SelectedSpell.GetComponent<Image>().sprite = InvetoryButtons[int.Parse(EventSystem.current.currentSelectedGameObject.name)].GetComponent<Image>().sprite;
            DesciptionText.text = EventSystem.current.currentSelectedGameObject.name;
        }
   }

   //use the cancell function to cancel out the currently selected with a new one

   public void CancelSelectedSpell()
    {
        if(InputManager.Instance.GetKeyDown("CancelSelectedSpell") && SelectedSpell != null)
        {
            //Make the image go back to default instead of null
            
            DesciptionText.text = "";
            HoldLastVerionOfButton = SelectedSpell;
            InvetoryButtons[int.Parse(SelectedSpell.name)].GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
            SelectedSpell = null;
            //uncomment later
            //SelectedSpell.GetComponent<Image>().sprite = null;
        }
    }

    public void LoadSpellsFromInvetory()
    {
        for (int i = 0; i < InvetoryButtons.Length; i++)
        {
            //Change teh testSprite to an array that holds all the spells the player has in inventory
            //actually if the players clicks on the spell in here just remove it from the spell bar and array
            //and add it back to the inventory
            InvetoryButtons[i].GetComponent<Image>().sprite = testSprite;
        }
    }
    //Lets the player to only put spells in slots that he/she has unlocked
   public void AvailableSpellSlots()
    {
        for (int j = 0; j < SpellManagerObject.GetComponent<SpellManager>().SpellSlots.Length; j++)
        {
            if (SpellManagerObject.GetComponent<SpellManager>().SpellSlots[j] != 1)
            {
               EquiptedSpells[j].SetActive(false);
            }
            else
            {
                EquiptedSpells[j].SetActive(true);
            }
        }
    }

    //Sets the selected spells sprie, mana cost to the equipted slot
    public void SetSpellToEquipSlot()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject.tag);
        if(SelectedSpell && EventSystem.current.currentSelectedGameObject.tag == "SpellSlots")
        {
            EquiptedSpells[int.Parse(EventSystem.current.currentSelectedGameObject.name)].GetComponent<Image>().sprite = SelectedSpell.GetComponent<Image>().sprite;
            for(int i = 0; i < InvetoryButtons.Length; i++)
            {
                if(SelectedSpell == InvetoryButtons[i])
                {
                    SpellManagerObject.GetComponent<SpellManager>().Spells[int.Parse(EventSystem.current.currentSelectedGameObject.name)] = Player.GetComponent<PlayerStatus>().MySpells[i];
                }
            }
        }
    }

    public void FinalizeCustomizationOfSpells()
    {
        for(int i = 0; i < EquiptedSpells.Length; i++)
        {
            SpellManagerObject.GetComponent<SpellManager>().SpellWheelImageArray[i].GetComponent<Image>().sprite = EquiptedSpells[i].GetComponent<Image>().sprite;
        }
        this.gameObject.SetActive(false);
    }
}
