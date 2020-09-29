using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private float timeOffset;
    
    private float timer;

    void Start()
    {
        
    }
    
    void Update()
    {
        timer += Time.deltaTime * Mathf.Pow(Velocity.Get(), 1f / 4f);
        if (timer >= timeOffset)
        {
            InstantiateObstacle();
            timer = 0;
        }
    }

    void InstantiateObstacle()
    {
        Instantiate(prefabs[Random.Range(0, prefabs.Length)], CalculatePosition(), transform.rotation, transform);
    }

    Vector2 CalculatePosition()
    {
        var pos = UnitConversion.PointToScreenRatio(transform.position);
        var perspective = RoadCommon.PERSPECTIVE(pos.y, RoadSize.Get().y);
        var max = RoadSize.Get().x * perspective;

        var offset= Random.Range(-max, max);
        return new Vector2(transform.position.x + offset, transform.position.y);
    }
}
