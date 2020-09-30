using UnityEngine;
using UnityEngine.EventSystems;

public class GrowingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private float multiplayer = 2f;
    private Vector2 scale;
    
    void Start()
    {
        scale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = scale * multiplayer;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = scale;
    }
}
