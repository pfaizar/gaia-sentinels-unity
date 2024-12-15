using UnityEngine;
using M_Pickup;
using M_PlayerCharacterController;
using M_WeaponController;
using M_PlayerWeaponsManager;

namespace M_WeaponPickup
{
    public class WeaponPickup : Pickup
    {
        public WeaponController WeaponPrefab;

        protected override void Start()
        {
            base.Start();

            foreach (Transform t in GetComponentsInChildren<Transform>())
            {
                if (t != transform)
                    t.gameObject.layer = 0;
            }
        }

        protected override void OnPicked(PlayerCharacterController byPlayer)
        {
            PlayerWeaponsManager playerWeaponsManager = byPlayer.GetComponent<PlayerWeaponsManager>();
            if (playerWeaponsManager)
            {
                if (playerWeaponsManager.AddWeapon(WeaponPrefab))
                {
                    if (playerWeaponsManager.GetActiveWeapon() == null)
                    {
                        playerWeaponsManager.SwitchWeapon(true);
                    }

                    PlayPickupFeedback();
                    Destroy(gameObject);
                }
            }
        }
    }
}