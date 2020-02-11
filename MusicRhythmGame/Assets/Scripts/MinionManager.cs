using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    public GameObject[] minionPrefabs;
    private int lastPrefabIndex = 0;
    private Transform playerTransform;
    private float spawnZ = 15.0f; // where exactly in Z should we spawn this object
    private List<GameObject> activeMinions = new List<GameObject>();
    private const float tileLength = 10.0f;
    private float safeZone = 15.0f; // within the safe zone, the tiles won't be deleted
    private int amnTilesOnScreen = 10; // number of tiles on screen at most
    private float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update() {}

    public void SpawnMinion(float z) {
        GameObject go;
        float[] spawnPos = {-5.0f, -1.66f, 1.66f, 5.0f};
        go = Instantiate(minionPrefabs[0]) as GameObject;
        go.transform.SetParent(transform);
        // 0.5f is half of the z of the minion
        go.transform.position = new Vector3(spawnPos[Random.Range(0, 4)], 1, z * speed + tileLength + 0.5f);
        activeMinions.Add(go);
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
