using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ObjectInfo
    {
        public GameObject prefab;
        public int probability; // hogere waarschijnlijkheid = meer exemplaren van het object = grotere kans om genoemd object te spawnen
    }

    public ObjectInfo[] objectSpawn;
    public int[] spawnPositions = new int[] { -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8 }; // Locaties waar de objecten kunnen spawnen 
    [SerializeField] private float spawnInterval = 1f; // Tijdsinterval tussen elke spawn

    private List<GameObject> prefabList;

    void Start()
    {
        prefabList = GeneratePrefabList(); // Lijst maken met alle objecten op basis van hun waarschijnlijkheid
        StartCoroutine(SpawnObjectsContinuously()); 
    }

    private void Update()
    {
        spawnInterval = LogicManager.instance.GetNewSpawnRate(); // Nieuwe spawnsnelheid vanuit Logic Manager
    }

    IEnumerator SpawnObjectsContinuously()
    {
        // Oneindig loop om voortdurend objecten te spawnen 
        while (true)
        {
            // Als het spel niet actief is of gepauzeerd is dan geen objecten spawnen
            if (LogicManager.instance.isGameActive && !LogicManager.instance.isGamePaused)
            {
                // Random object kiezen
                GameObject prefabToSpawn = prefabList[Random.Range(0, prefabList.Count)];
                // Random spawn locatie kiezen
                Vector3 spawnPosition = new Vector3(spawnPositions[Random.Range(0, spawnPositions.Length)], 12.5f, 0);

                // Het willekeurige object spawnen op de willekeurige locatie
                Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            }

            // Wacht op het opgegeven interval voordat het opnieuw controleert
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // De lijst vullen met de objecten
    List<GameObject> GeneratePrefabList()
    {
        List<GameObject> prefabList = new List<GameObject>();

        foreach (ObjectInfo obj in objectSpawn)
        {
            for (int i = 0; i < obj.probability; i++)
            {
                prefabList.Add(obj.prefab);
            }
        }

        return prefabList;
    }
}