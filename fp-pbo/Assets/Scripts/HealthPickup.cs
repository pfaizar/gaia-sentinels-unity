using UnityEngine;
using M_Pickup;
using M_PlayerCharacterController;
using M_Health;

namespace M_HealthPickup
{
    public class HealthPickup : Pickup
    {
        public float HealAmount;

        public int Cost = 0;

        public bool Respawn = false;

        protected override void OnPicked(PlayerCharacterController player)
        {
            if (Cost > 0)
            {
                if (player.Money >= Cost)
                {
                    player.RemoveMoney(Cost);
                    HealPlayer(player);
                }
            }
            else
            {
                HealPlayer(player);
            }

        }

        protected void HealPlayer(PlayerCharacterController player)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth && playerHealth.CanPickup())
            {
                playerHealth.Heal(HealAmount);

                PlayPickupFeedback();

                if (!Respawn)
                {
                    Destroy(gameObject);
                }
            }
        }


    }
}