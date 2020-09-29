using System;
using System.Collections.Generic;
using UnityEngine;

public class RoadObject: MonoBehaviour {
    private Vector2 startScale;
    private Vector2 startPosition;
    private Vector2 currentPosition;
    
    private float offset;
    private float perspective;
    
    private List<RoadObjectListener> listeners = new List<RoadObjectListener>();

    private void Awake()
    {
        startScale = transform.localScale;
        startPosition = UnitConversion.PointToScreenRatio(transform.position);
    }

    public void Update()
    {
        if (IsOnScreen())
        {
            offset += CalculateOffset();
            currentPosition = new Vector2(startPosition.x, startPosition.y + offset);
            perspective = RoadCommon.PERSPECTIVE(currentPosition.y, RoadSize.Get().y);
            float distanceFromCenter = 0.5f - startPosition.x;
            currentPosition.x += RoadCommon.CURVE(Curvature.Get(), perspective) + distanceFromCenter * perspective;
            ApplyPosition(currentPosition);
        } else
        {
            WarnListeners();
            Destroy(gameObject);
        }
    }

    void WarnListeners()
    {
        foreach (var listener in listeners)
        {
            listener.OnObjectDestroy(this);
        }
    }

    public void AddListener(RoadObjectListener listener)
    {
        listeners.Add(listener);
    }

    public void SetSortingLayer(int layer)
    {
        GetComponent<SpriteRenderer>().sortingOrder = layer;
    }

    public bool IsOnScreen() {
        return currentPosition.y >= 0f && currentPosition.y <= 1f;
    }

    void ApplyPosition(Vector2 position)
    {
        transform.position = UnitConversion.ScreenRatioToPoint(position);
        transform.localScale = startScale * (1f - perspective);
    }

    private float CalculateOffset()
    {
        return -Distance.Delta() * Time.deltaTime * (1f - perspective);
    }
}
