using UnityEngine;
using UnityEngine.UI;
using M_Health;

namespace M_WorldspaceHealthBar
{
    public class WorldspaceHealthBar : MonoBehaviour
    {
        public Health Health;

        public Image HealthBarImage;

        public Transform HealthBarPivot;

        public bool HideFullHealthBar = true;

        void Update()
        {
            HealthBarImage.fillAmount = Health.CurrentHealth / Health.MaxHealth;

            HealthBarPivot.LookAt(Camera.main.transform.position);

            if (HideFullHealthBar)
                HealthBarPivot.gameObject.SetActive(HealthBarImage.fillAmount != 1);
        }
    }
}