using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    public GameObject[] minionPrefabs;
    private int lastPrefabIndex = 0;
    private Transform playerTransform;
    private float spawnX = 0.0f;
    private float spawnZ = 0.0f; // where exactly in Z should we spawn this object
    private List<GameObject> activeMinions;
    private const float tileLength = 10.0f;
    private float safeZone = 15.0f; // within the safe zone, the tiles won't be deleted
    private int amnTilesOnScreen = 10; // number of tiles on screen at most
    // Start is called before the first frame update
    void Start()
    {
        activeMinions = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength)) {
            spawnMinion();
            DeleteMinion();
        }
    }

    private void spawnMinion(int prefabIndex = -1) {
        createMinionsInTile(spawnZ);
        spawnZ += tileLength;
    }
    private void createMinionsInTile(float initialMountPoint) {
        for (int i=0; i<5; i++) {
            GameObject go;
            go = Instantiate(minionPrefabs[0]) as GameObject;
            go.transform.SetParent(transform);
            go.transform.position = new Vector3(0, 1, initialMountPoint + i*2);
            activeMinions.Add(go);
        }
    }
    private void DeleteMinion() {
        Destroy(activeMinions[0]);
        activeMinions.RemoveAt(0);
    }

    private int RandomPrefabIndex() {
        if (minionPrefabs.Length <= 1 )
            return 0;
        
        int randomIndex = lastPrefabIndex;
        while(randomIndex == lastPrefabIndex) {
            randomIndex = Random.Range(0, minionPrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
