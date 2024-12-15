using UnityEngine;
using UnityEngine.UI;
using M_Health;
using M_PlayerCharacterController;

namespace M_PlayerHealthBar
{
    public class PlayerHealthBar : MonoBehaviour
    {
        public Image HealthFillImage;

        Health m_PlayerHealth;

        void Start()
        {
            PlayerCharacterController playerCharacterController =
                GameObject.FindObjectOfType<PlayerCharacterController>();

            m_PlayerHealth = playerCharacterController.GetComponent<Health>();
        }

        void Update()
        {
            HealthFillImage.fillAmount = m_PlayerHealth.CurrentHealth / m_PlayerHealth.MaxHealth;
        }
    }
}