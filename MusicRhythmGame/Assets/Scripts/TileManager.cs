using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private Transform playerTransform;
    private float spawnZ = 0.0f; // where exactly in Z should we spawn this object
    private float tileLength = 50.0f; // according to the tile asset length
    private float safeZone = 75.0f; // within the safe zone, the tiles won't be deleted
    private int amnTilesOnScreen = 10; // number of tiles on screen at most
    private int lastPrefabIndex = 0;
    private List<GameObject> activeTiles;
    private int ranTheme = -1; // 0 forest, 1 desert, 2 snow

    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < amnTilesOnScreen; i++) {
            // Use this code when we have 3 different prefabs, generate two normal bridges for the first 2 bridges
            // if (i < 2)
            //     SpawnTile(0);
            // else
                SpawnTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength)) {
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1) {
        GameObject go;
        // use this code when we have 3 different prefabs
        if (prefabIndex == -1)
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        // go = Instantiate(tilePrefabs[0]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * (spawnZ + 25.0F);
        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    private void DeleteTile() {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex() {
        if (tilePrefabs.Length <= 1 )
            return 0;
        
        // int randomIndex = lastPrefabIndex;
        // while(randomIndex == lastPrefabIndex) {
        //     randomIndex = Random.Range(0, tilePrefabs.Length);
        // }

        // lastPrefabIndex = randomIndex;
        if (ranTheme == -1) 
            ranTheme = Random.Range(0, tilePrefabs.Length);
        return ranTheme;
    }
}
