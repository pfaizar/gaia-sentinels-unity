using UnityEngine;
using M_ActorsManager;

namespace M_Actor
{
    public class Actor : MonoBehaviour
    {
        public int Affiliation;
        public Transform AimPoint;
        ActorsManager m_ActorsManager;
        void Start()
        {
            m_ActorsManager = GameObject.FindObjectOfType<ActorsManager>();

            if (!m_ActorsManager.Actors.Contains(this))
            {
                m_ActorsManager.Actors.Add(this);
            }
        }

        void OnDestroy()
        {
            if (m_ActorsManager)
            {
                m_ActorsManager.Actors.Remove(this);
            }
        }
    }
}