using UnityEngine;

public class PlayableArea : MonoBehaviour {
    private void OnTriggerExit2D(Collider2D other)
    {
        var obj = other.GetComponent<RoadObject>();
        if (obj != null)
        {
            Destroy(other.gameObject);
        }
    }
}