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

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private List<GameObject> spawnPoints = new List<GameObject>();

    //[HideInInspector]
    public List<GameObject> babies = new List<GameObject>();

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
        while (true)
        {
            int ran = Random.Range(0, spawnPoints.Count);

            GameObject newSpawn = Instantiate(prefab, spawnPoints[ran].transform.position, Quaternion.identity);
            newSpawn.GetComponent<BabyPenanggal>().spawnPoint = spawnPoints[ran].transform.position;
            newSpawn.GetComponent<BabyPenanggal>().playerPosition = player.transform.position;

            babies.Add(newSpawn);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
