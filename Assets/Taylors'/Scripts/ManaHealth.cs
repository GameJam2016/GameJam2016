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
        Player = GameObject.FindGameObjectWithTag("Player");

        Health.type = Image.Type.Filled;
        Health.fillMethod = Image.FillMethod.Horizontal;
        Health.fillOrigin = (int)Image.OriginHorizontal.Left;
        Health.fillAmount = 1;

        Mana.type = Image.Type.Filled;
        Mana.fillMethod = Image.FillMethod.Horizontal;
        Mana.fillOrigin = (int)Image.OriginHorizontal.Left;
        Mana.fillAmount = 1;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void UpdateManaHealth()
    {
        Mana.fillAmount = (Player.GetComponent<PlayerStatus>().Mana / Player.GetComponent<PlayerStatus>().MaxMana);
        Health.fillAmount = (Player.GetComponent<PlayerStatus>().Health / 100.0f);
    }

}
