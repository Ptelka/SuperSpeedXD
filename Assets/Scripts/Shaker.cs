using UnityEngine;

public class Shaker : MonoBehaviour {
    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay = 0.002f;
    public float shake_intensity = .3f;

    private float temp_shake_intensity = 0;

    void Update (){
        if (temp_shake_intensity > 0){
            transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity * .2f;
            transform.rotation = new Quaternion(
                originRotation.x + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
                originRotation.y + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
                originRotation.z + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
                originRotation.w + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f);
            temp_shake_intensity -= shake_decay;
        }
    }

    public void Shake(){
        if (Shaking())
        {
            return;
        }
        originPosition = transform.position;
        originRotation = transform.rotation;
        temp_shake_intensity = shake_intensity;

    }

    bool Shaking()
    {
        return temp_shake_intensity > 0;
    }
}