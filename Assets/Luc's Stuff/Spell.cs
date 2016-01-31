using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Spell : MonoBehaviour
{

    public float ManaCost;
    public string Description;
    public Cards.typeOfCard Type = Cards.typeOfCard.Offense;
    public attribute Attr = attribute.Demonic;
    public Image SpellImage;
    public float Cooldown;

    void Awake()
    {
        
    }

    public virtual void Randomize(PlayerStatus status)
    {
       

    }

}
