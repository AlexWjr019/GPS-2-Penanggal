using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySpawner : MonoBehaviour
{
    public static BabySpawner Instance;

    [SerializeField]
    private float maxSpawnDelay = 20f;
    [SerializeField]
    private float minSpawnDelay = 10f;
    [HideInInspector]
    public bool spawnBaby;

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private List<GameObject> spawnPoints = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator SpawnBaby()
    {
        while (spawnBaby)
        {
            int ran = Random.Range(0, spawnPoints.Count);

            GameObject newSpawn = Instantiate(prefab, spawnPoints[ran].transform.position, Quaternion.identity);
            newSpawn.GetComponent<BabyPenanggal>().spawnPoint = spawnPoints[ran].transform.position;
            newSpawn.GetComponent<BabyPenanggal>().playerPosition = player.transform.position;

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
