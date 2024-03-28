using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance;
    private List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] private int amountPool = 3;
    [SerializeField] private GameObject bulletPrefab;
    public Material[] ballMaterials;

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
    private void Start()
    {
        for(int i=0;i<amountPool; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-40f, 40f), 5f, Random.Range(-40f, 40f));
            GameObject objs = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            objs.GetComponent<Renderer>().material = ballMaterials[i];
            pooledObjects.Add(objs);
        }
    }
    public GameObject GetPooledObject()
    {
        for(int i=0;i<pooledObjects.Count;i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-40f, 40f), 5f, Random.Range(-40f, 40f));
                pooledObjects[i].transform.position = spawnPosition;
                return pooledObjects[i];
            }
        }
        return null;
    }
}