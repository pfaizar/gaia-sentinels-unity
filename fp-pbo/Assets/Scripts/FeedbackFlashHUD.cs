using UnityEngine;
using UnityEngine.UI;
using M_Health;
using M_GameFlowManager;
using M_PlayerCharacterController;

namespace M_FeedbackFlashHUD
{
    public class FeedbackFlashHUD : MonoBehaviour
    {
        public Image FlashImage;
        public CanvasGroup FlashCanvasGroup;
        public CanvasGroup VignetteCanvasGroup;
        public Color DamageFlashColor;
        public float DamageFlashDuration;

        public float DamageFlashMaxAlpha = 1f;
        public float CriticaHealthVignetteMaxAlpha = .8f;
        public float PulsatingVignetteFrequency = 4f;
        public Color HealFlashColor;

        public float HealFlashDuration;
        public float HealFlashMaxAlpha = 1f;

        bool m_FlashActive;
        float m_LastTimeFlashStarted = Mathf.NegativeInfinity;
        Health m_PlayerHealth;
        GameFlowManager m_GameFlowManager;

        void Start()
        {
            PlayerCharacterController playerCharacterController = FindObjectOfType<PlayerCharacterController>();

            m_PlayerHealth = playerCharacterController.GetComponent<Health>();

            m_GameFlowManager = FindObjectOfType<GameFlowManager>();

            m_PlayerHealth.OnDamaged += OnTakeDamage;
            m_PlayerHealth.OnHealed += OnHealed;
        }

        void Update()
        {
            if (m_PlayerHealth.IsCritical())
            {
                VignetteCanvasGroup.gameObject.SetActive(true);
                float vignetteAlpha =
                    (1 - (m_PlayerHealth.CurrentHealth / m_PlayerHealth.MaxHealth /
                          m_PlayerHealth.CriticalHealthRatio)) * CriticaHealthVignetteMaxAlpha;

                if (m_GameFlowManager.GameIsEnding)
                    VignetteCanvasGroup.alpha = vignetteAlpha;
                else
                    VignetteCanvasGroup.alpha =
                        ((Mathf.Sin(Time.time * PulsatingVignetteFrequency) / 2) + 0.5f) * vignetteAlpha;
            }
            else
            {
                VignetteCanvasGroup.gameObject.SetActive(false);
            }


            if (m_FlashActive)
            {
                float normalizedTimeSinceDamage = (Time.time - m_LastTimeFlashStarted) / DamageFlashDuration;

                if (normalizedTimeSinceDamage < 1f)
                {
                    float flashAmount = DamageFlashMaxAlpha * (1f - normalizedTimeSinceDamage);
                    FlashCanvasGroup.alpha = flashAmount;
                }
                else
                {
                    FlashCanvasGroup.gameObject.SetActive(false);
                    m_FlashActive = false;
                }
            }
        }

        void ResetFlash()
        {
            m_LastTimeFlashStarted = Time.time;
            m_FlashActive = true;
            FlashCanvasGroup.alpha = 0f;
            FlashCanvasGroup.gameObject.SetActive(true);
        }

        void OnTakeDamage(float dmg, GameObject damageSource)
        {
            ResetFlash();
            FlashImage.color = DamageFlashColor;
        }

        void OnHealed(float amount)
        {
            ResetFlash();
            FlashImage.color = HealFlashColor;
        }
    }
}