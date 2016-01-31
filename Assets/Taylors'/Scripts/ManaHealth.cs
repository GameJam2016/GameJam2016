using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManaHealth : MonoBehaviour
{
    public Image Mana, Health;
    public PlayerStatus Player;
	// Use this for initialization
	void Start ()
    {
        Health.type = Image.Type.Filled;
        Health.fillMethod = Image.FillMethod.Horizontal;
        Health.fillOrigin = (int)Image.OriginHorizontal.Left;
        Health.fillAmount = 1;

        Mana.type = Image.Type.Filled;
        Mana.fillMethod = Image.FillMethod.Horizontal;
        Mana.fillOrigin = (int)Image.OriginHorizontal.Left;
        Mana.fillAmount = 1;

        //Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateHealthMana();
        CheckHealth();
    }

    void UpdateHealthMana()
    {
        Mana.fillAmount = (Player.Mana / Player.MaxMana);
        Health.fillAmount = (Player.Health / 100);
    }

    void CheckHealth()
    {
        if(Player.Health <= 0.0f)
        {
            Player.transform.position = Player.LastShrineVisited.transform.position;
        }
    }
}
