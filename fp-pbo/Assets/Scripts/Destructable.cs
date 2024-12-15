using UnityEngine;
using M_Health;

namespace M_Destructable
{
    public class Destructable : MonoBehaviour
    {
        Health m_Health;

        void Start()
        {
            m_Health = GetComponent<Health>();

            m_Health.OnDie += OnDie;
            m_Health.OnDamaged += OnDamaged;
        }

        void OnDamaged(float damage, GameObject damageSource)
        {
        }

        void OnDie()
        {
            Destroy(gameObject);
        }
    }
}