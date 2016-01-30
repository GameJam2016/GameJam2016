using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScreen : MonoBehaviour
{
    [SerializeField]
    private Text m_UIText;

    private float m_TextFlashSpeed = 5.0f;
    void Update()
    {
        UpdateText();
        if (Input.anyKeyDown)
        {
            EndMenu();
        }
    }
    
    void UpdateText()
    {
        m_UIText.color = new Color(m_UIText.color.r, m_UIText.color.g, m_UIText.color.b, Mathf.Sin(Time.time * m_TextFlashSpeed));
    }

    void EndMenu()
    {
        Destroy(gameObject);
    }
}
