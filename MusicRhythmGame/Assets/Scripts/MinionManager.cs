using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    public GameObject[] minionPrefabs;
    private int lastPrefabIndex = 0;
    private Transform playerTransform;
    private float spawnZ = 15.0f; // where exactly in Z should we spawn this object
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
        GameObject soundManager = GameObject.Find("SoundManager");
        bool isGameEnded = soundManager.GetComponent<SoundManager>().IsGameEnded();
        if (!isGameEnded && (playerTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength))) {
            SpawnMinion();
            DeleteMinion();
        }
    }

    private void SpawnMinion(int prefabIndex = -1) {
        CreateMinionsInTile(spawnZ);
        spawnZ += tileLength;
    }
    private void CreateMinionsInTile(float initialMountPoint) {
        for (int i=0; i<5; i++) {
            GameObject go;
            go = Instantiate(minionPrefabs[0]) as GameObject;
            go.transform.SetParent(transform);
            go.transform.position = new Vector3(Random.Range(-1, 2), 1, initialMountPoint + i*2);
            activeMinions.Add(go);
        }
    }
    private void DeleteMinion() {
        // Delete any missed minion
        for(int i=0;i<activeMinions.Count; i++) {
            if (activeMinions[i]) {
                if (playerTransform.position.z >= activeMinions[i].transform.position.z) {
                    Destroy(activeMinions[i]);
                    activeMinions.RemoveAt(i);
                }
            }
        }
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
