using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventoryHandler : MonoBehaviour
{
     
    public GameObject[] InvetoryButtons;
    public GameObject[] CharacterSpellArray;
    public GameObject ButtonTemplate;
    public GameObject Canvas, SelectedSpell, SpellManagerObject;
    public float newButtonX = -150;
    public float newButtonY = 150;

    public Sprite testSprite;

    // Use this for initialization
    void Start ()
    {
        float CanvasWidth = Canvas.GetComponent<RectTransform>().rect.width;
        float CanvasHeight = Canvas.GetComponent<RectTransform>().rect.height;

        LoadSpellsFromInvetory();

        AvailableSpellSlots();
        
        EventSystem.current.SetSelectedGameObject(InvetoryButtons[0]);
    }

   public void OnClickSelectedSpell()
   {
        SelectedSpell.GetComponent<Image>().sprite = InvetoryButtons[0].GetComponent<Image>().sprite;
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
                CharacterSpellArray[j].SetActive(false);
            }
        }
    }


}
