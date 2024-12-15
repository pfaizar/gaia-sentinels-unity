using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using M_EnemyController;
namespace M_EnemySpawnController
{
    public class EnemySpawnController : MonoBehaviour
    {
        public int initialEnemyPerWave = 5;
        public int currentEnemyPerWave;

        public int additionalEnemyPerWave = 5;
        public int maxOffsetSpawn = 60;
        public int minOffsetSpawn = 50;

        public float spawnDelay = 0.5f;
        public int currentWave = 0;
        public float waveCooldown = 10.0f;

        public EnemyController[] zombiePrefab;

        public bool inCooldown;

        public float cooldownCounter = 0.0f;

        public List<EnemyController> currentEnemyAlive;

        public GameObject waveNoticeUI;

        public TextMeshProUGUI cooldownCounterText;
        public TextMeshProUGUI enemyCounterText;

        private void Start()
        {
            currentEnemyPerWave = initialEnemyPerWave;
            StartNextWave();
        }


        public void StartNextWave()
        {
            currentEnemyAlive.Clear();

            currentWave++;

            StartCoroutine(SpawnWave());
        }

        public IEnumerator SpawnWave()
        {
            for (int i = 0; i < currentEnemyPerWave; i++)
            {
                // Generate random angle in radians
                float randomAngle = Random.Range(0f, 2f * Mathf.PI);

                // Generate random radius between min and max offset
                float randomRadius = Random.Range(minOffsetSpawn, maxOffsetSpawn);

                // Convert polar coordinates to Cartesian coordinates
                Vector3 spawnOffset = new Vector3(
                    Mathf.Cos(randomAngle) * randomRadius,
                    0,
                    Mathf.Sin(randomAngle) * randomRadius
                );

                Vector3 spawnPosition = transform.TransformPoint(spawnOffset);

                EnemyController randomZombie = zombiePrefab[Random.Range(0, zombiePrefab.Length)];
                var zombie = Instantiate(randomZombie, spawnPosition, Quaternion.identity);

                EnemyController zombieInstance = zombie.GetComponent<EnemyController>();

                currentEnemyAlive.Add(zombieInstance);

                yield return new WaitForSeconds(spawnDelay);
            }
        }

        private void Update()
        {
            List<EnemyController> enemyToRemove = new List<EnemyController>();

            foreach (var zombie in currentEnemyAlive)
            {
                if (zombie == null)
                {
                    enemyToRemove.Add(zombie);
                }
            }

            foreach (var zombie in enemyToRemove)
            {
                currentEnemyAlive.Remove(zombie);
            }

            enemyToRemove.Clear();

            if (currentEnemyAlive.Count == 0 && !inCooldown)
            {
                StartCoroutine(WaveCooldown());

            }

            if (inCooldown)
            {
                cooldownCounter -= Time.deltaTime;
            }
            else
            {
                cooldownCounter = waveCooldown;
            }

            cooldownCounterText.text = cooldownCounter.ToString("F1");

            enemyCounterText.text = currentEnemyAlive.Count.ToString("F0");
        }

        public IEnumerator WaveCooldown()
        {
            inCooldown = true;
            waveNoticeUI.gameObject.SetActive(true);
            yield return new WaitForSeconds(waveCooldown);
            inCooldown = false;
            waveNoticeUI.gameObject.SetActive(false);

            currentEnemyPerWave += additionalEnemyPerWave;
            StartNextWave();
        }

    }
}