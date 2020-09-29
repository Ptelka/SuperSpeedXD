using System;
using UnityEngine;

public class RoadSideFactory : MonoBehaviour {
    public GameObject[] prefabs;
    public float offset;
    public int count;
    [SerializeField] private int logBase = 16; 
    private Vector2 start;

    private GameObject last;
    
    private void Start()
    {
        start = transform.position;
        for (int i = count; i >= 0; --i)
        {
            InstantiateNewObject(i);
        }
    }

    private void Update()
    {
        if (last == null || transform.position.y - last.transform.position.y >= offset / Mathf.Log(Velocity.Get(), logBase))
        {
            InstantiateNewObject(0);
        }
    }

    void InstantiateNewObject(int i)
    {
        var pos = new Vector2(start.x, start.y - i * offset);
        var order = last == null ? i : last.GetComponent<SpriteRenderer>().sortingOrder - 1;
        
        last = Instantiate(prefabs[i % prefabs.Length], pos, transform.rotation, transform);
        last.GetComponent<SpriteRenderer>().sortingOrder = order;
    }
}