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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(pInput.bIsAttacking)
        {
            if(other.tag == "Enemy")
            {
                DamageManager.Instance.SendDamage(gameObject, other.GetComponent<DamageableObject>(), attribute.Demonic, 10, false);
            }
        }
    }
}
