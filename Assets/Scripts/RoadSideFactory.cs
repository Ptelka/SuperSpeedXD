using System;
using UnityEngine;

public class RoadSideFactory : MonoBehaviour, Factory, RoadObjectListener {
    public GameObject[] prefabs;
    public float offset;
    public int count;
    private Vector2 start;

    private GameObject last;
    
    private void Start()
    {
        start = transform.position;
        for (int i = 0; i < count; ++i)
        {
            InstantiateNewObject(i);
        }
    }

    private void Update()
    {
        if (last == null || transform.position.y - last.transform.position.y >= offset / Mathf.Pow(Velocity.Get(), 1f / 3f))
        {
            Instantiate();
        }
    }

    RoadObject InstantiateNewObject(int i)
    {
        var pos = new Vector2(start.x, start.y - i * offset);
        last = Instantiate(prefabs[i % prefabs.Length], pos, transform.rotation, transform);
        var obj = last.GetComponent<RoadObject>();
        obj.SetRoadHeight(0.6f);
        obj.AddListener(this);
        obj.SetSortingLayer(i + 100);
        return obj;
    }

    public RoadObject Instantiate()
    {
        return InstantiateNewObject(0);
    }

    public void OnObjectDestroy(RoadObject obj)
    {

    }
}