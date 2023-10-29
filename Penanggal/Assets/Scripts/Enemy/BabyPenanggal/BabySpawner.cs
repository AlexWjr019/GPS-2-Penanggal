using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySpawner : MonoBehaviour
{
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

    void Start()
    {
        StartCoroutine(SpawnBaby());
    }

    void Update()
    {
        
    }

    private IEnumerator SpawnBaby()
    {
        while (true)
        {
            int ran = Random.Range(0, spawnPoints.Count);

            //Vector3 position = new Vector3(spawnPoints[ran].transform.position.x, spawnPoints[ran].transform.position.y, spawnPoints[ran].transform.position.z);

            GameObject newSpawn = Instantiate(prefab, spawnPoints[ran].transform.position, Quaternion.identity);
            newSpawn.GetComponent<BabyPenanggal>().spawnPoint = spawnPoints[ran].transform.position;
            newSpawn.GetComponent<BabyPenanggal>().playerPosition = player.transform.position;

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
