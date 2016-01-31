using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shrine : MonoBehaviour
{
    private bool FirstTime = true;
    public int numOfCardsToReward = 3;
    [SerializeField]
    private GameObject m_Inventory;
    [SerializeField]
    private GameObject SpellManagerObject;
    
    void Start()
    {
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(FirstTime)
            {
                other.gameObject.GetComponent<PlayerStatus>().AddSpell(numOfCardsToReward);
                SpellManagerObject.GetComponent<SpellManager>().IncreaseSpellRevolverSize();
                FirstTime = false;
                other.GetComponent<PlayerStatus>().TotalShinesVisited += 1;
            }

            if (InputManager.Instance.GetKey("OpenInventory") && m_Inventory.activeInHierarchy == false)
            {
                m_Inventory.SetActive(true);
                m_Inventory.GetComponent<InventoryHandler>().StartInventory();
                SpellManagerObject.GetComponent<SpellManager>().InventoryOpen = true;
            }
            other.GetComponent<PlayerStatus>().LastShrineVisited = this.gameObject;
            other.GetComponent<PlayerStatus>().Mana = other.GetComponent<PlayerStatus>().TotalShinesVisited * other.GetComponent<PlayerStatus>().MaxMana;
            other.GetComponent<PlayerStatus>().Health = 100.0f;
        }
    }
}
