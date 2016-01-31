using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManaHealth : MonoBehaviour
{
    public Image Mana, Health;
    public GameObject Player;
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

        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateHealthMana();
        CheckHealth();
    }

    void UpdateHealthMana()
    {
        Mana.fillAmount = (Player.GetComponent<PlayerStatus>().Mana / Player.GetComponent<PlayerStatus>().MaxMana);
        Health.fillAmount = (Player.GetComponent<PlayerStatus>().Health / 100);
    }

    void CheckHealth()
    {
        if(Player.GetComponent<PlayerStatus>().Health <= 0.0f)
        {
            Player.transform.position = Player.GetComponent<PlayerStatus>().LastShrineVisited.transform.position;
        }
    }
}
