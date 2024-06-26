using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawnerController : MonoBehaviour
{
    public GameObject obstaclePref, colorChangerPref, player;
    public Camera mainCamera;
    private GameObject obstacle, colorChanger;
    public float spawnHeightOffset = 5f; 
    public float destroyHeightOffset = -5f;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private float spawnPos = 1f, destroyThreshold = -10f;
    void Start()
    {
        obstacle = obstaclePref;
        colorChanger = colorChangerPref;
        spawnPos = player.transform.position.y;
        SpawnObjects();
    }

    void Update()
    {
        if (mainCamera.transform.position.y >= spawnPos) SpawnObjects();        
        if(spawnedObjects.Count > 0) DestroyObjectLowerThanPlayer();
    }
    void SpawnObjects()
    {
            Vector3 spawnPositionObstacle = mainCamera.transform.position + new Vector3(0, mainCamera.orthographicSize + spawnHeightOffset, 10f);
            Vector3 spawnPositionColorChanger = mainCamera.transform.position + new Vector3(0, mainCamera.orthographicSize + spawnHeightOffset + 5f, 10f);
            spawnPos = spawnPositionObstacle.y;
            spawnedObjects.Add(Instantiate(obstacle, spawnPositionObstacle, Quaternion.identity));
            spawnedObjects[spawnedObjects.Count - 1].GetComponent<SmallCircleController>().speed = Random.Range(100,300);
            float randomScale = Random.Range(1f, 2.5f);
            spawnedObjects[spawnedObjects.Count - 1].GetComponent<SmallCircleController>().transform.localScale = new Vector3(randomScale, randomScale, 0);

            spawnedObjects.Add(Instantiate(colorChanger, spawnPositionColorChanger, Quaternion.identity));
    }
    void DestroyObjectLowerThanPlayer()
    {
        if (mainCamera.transform.position.y >= destroyThreshold)
        {
            destroyThreshold = mainCamera.transform.position.y - mainCamera.orthographicSize - Mathf.Abs(destroyHeightOffset);
            
            for(int i = 0;i<spawnedObjects.Count -1;i++) 
            {
                GameObject obj  = spawnedObjects[i];
                if (obj != null)
                {
                    if (obj.transform.position.y <= destroyThreshold)
                    {
                        Destroy(obj);
                        spawnedObjects.RemoveAt(i);
                    }
                }
            }
        }
    }
}
