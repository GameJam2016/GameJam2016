using UnityEngine;
using System.Collections;

public class PlayerStatus : DamageableObject
{
    public float Mana;
    public float MaxMana;
    public bool bIsInvisible = false;
    public GameObject[] MySpells = new GameObject[15];
    public Animator myAnimator;
    public int TotalShinesVisited;
    public GameObject LastShrineVisited;

    // Use this for initialization
    void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();
        if(Mana == 0)
        {
            Mana = 30;
        }

        if(MaxMana == 0)
        {
            MaxMana = 50;

        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D CollisionEnter)
    {
        if(CollisionEnter.gameObject.tag == "Kill")
        {
            Health -= 100.0f;
        }
    }

    public void AddSpell(int num)
    {
        for (int j = 0; j < num; j++)
        {
            for (int i = 0; i < MySpells.Length; i++)
            {
                if (!MySpells[i])
                {
                    MySpells[i] = GameObject.FindGameObjectWithTag("Cards").GetComponent<Cards>().CreateCard();
                    if (MySpells[i])
                    {
                        MySpells[i].GetComponent<Spell>().Randomize(this);
                    }
                    break;
                }
            }
        }
    }
}