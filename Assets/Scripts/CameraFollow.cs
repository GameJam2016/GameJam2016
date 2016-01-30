using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float m_Dampening = 0.95f;

    [SerializeField]
    private Transform m_Target;

	// Update is called once per frame
	void FixedUpdate ()
    {
        FollowTarget();
	}

    void FollowTarget()
    {
        if (!m_Target)
        {
            return;
        }

        transform.position = (transform.position * m_Dampening) + (new Vector3(m_Target.position.x, m_Target.position.y, transform.position.z) * (1 - m_Dampening));



    }
}
