using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> pool;
    public int maxObjects;
    float timer = 0f;
    public float spawnCooldown = 1f;
    float zOffsetMin = 25f;
    float zOffsetMax = 30f;

    // Start is called before the first frame update
    void Start()
    {
        pool = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < maxObjects; i++)
        {
            tmp = Instantiate(prefab);
            tmp.SetActive(false);
            pool.Add(tmp);
        }
    }

    public GameObject GetPooledObject() 
    { 
        for (int i = 0; i < maxObjects; i++) 
        {
            if (!pool[i].activeInHierarchy)
                return pool[i];
        } 
        return null; 
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.gameManagerInstance.gameActive) { 
            if (timer <= spawnCooldown)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                //Spawn Enemy
                SpawnObject();
            }
    }
    }

    private void SpawnObject()
    {
        GameObject newObject = GetPooledObject();
        if (newObject != null)
        {
            int randomLane = Random.Range(-2, 3);
            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            float objectPosZ = playerPos.z + Random.Range(zOffsetMin, zOffsetMax);
            Vector3 objectPosition = new Vector3(PlayerController.laneOffset * randomLane, 0, objectPosZ);
            newObject.transform.localPosition = objectPosition;
            newObject.transform.rotation = Quaternion.identity;
            newObject.SetActive(true);

        }
    }
}
