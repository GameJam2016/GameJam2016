using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour
{
    public Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();
    }

    public void EndAttack()
    {
        myAnimator.SetBool("Attacking", false);
    }
}
