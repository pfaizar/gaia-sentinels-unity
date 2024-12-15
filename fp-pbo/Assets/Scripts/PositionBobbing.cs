
using UnityEngine;

namespace M_PositionBobbing
{
    public class PositionBobbing : MonoBehaviour
    {
        public float VerticalBobFrequency = 1f;

        public float BobbingAmount = 0.5f;

        Vector3 m_StartPosition;

        void Start()
        {
            m_StartPosition = transform.position;
        }

        void Update()
        {
            float bobbingAnimationPhase = ((Mathf.Sin(Time.time * VerticalBobFrequency) * 0.5f) + 0.5f) * BobbingAmount;
            transform.position = m_StartPosition + Vector3.up * bobbingAnimationPhase;
        }
    }
}