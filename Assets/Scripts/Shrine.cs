using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shrine : MonoBehaviour
{
    private bool FirstTime = true;
    public int numOfCardsToReward = 3;
    public Image[] cards;

    void Start()
    {
        cards = GameObject.Find("CardReward").GetComponentsInChildren<Image>();

        foreach (Image im in cards)
        {
            im.enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(FirstTime)
            {
                other.gameObject.GetComponent<PlayerStatus>().AddSpell(numOfCardsToReward);
                FirstTime = false;
                foreach (Image im in cards)
                {
                    im.enabled = true;
                }

            }
        }
    }
}
