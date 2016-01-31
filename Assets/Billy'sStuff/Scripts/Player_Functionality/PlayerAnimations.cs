using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour
{
    public Animator myAnimator;
    public PlayerStatus pStatus;
    public PlayerInput pInput;

    void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();
        pStatus = GetComponentInParent<PlayerStatus>();
        pInput = GetComponentInParent<PlayerInput>();
    }
    
    public void EndAttack()
    {
        myAnimator.SetBool("Attacking", false);
        pInput.HitObjects.Clear();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(pInput.bIsAttacking)
        {
            if(other.tag == "Enemy")
            {
         
                foreach(GameObject obj in pInput.HitObjects)
                {
                    if(obj == other.gameObject)
                    {
                        return;
                    }
                }

                if (pInput.HitObjects.Count <= 2)
                {
                    DamageManager.Instance.SendDamage(gameObject, other.GetComponent<DamageableObject>(), attribute.Demonic, 11, true);
                }
                else
                {
                    DamageManager.Instance.SendDamage(gameObject, other.GetComponent<DamageableObject>(), attribute.Demonic, 11, false);
                }
                pInput.HitObjects.Add(other.gameObject);

            }
        }
    }
}
