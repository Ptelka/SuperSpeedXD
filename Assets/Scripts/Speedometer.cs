using UnityEngine;

public class Speedometer : MonoBehaviour {
    [SerializeField] private GameObject warning;
    [SerializeField] private GameObject indicator;
    public float rotationOffset;
    public float maxSpeed;
    float startRotation;

    private AudioSource audio;
    
    
    float Map(float val, float inMin, float inMax, float outMin, float outMax)
    {
        return (val - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    public void Start()
    {
        startRotation = indicator.transform.rotation.eulerAngles.z;
        audio = GetComponent<AudioSource>();
    }
    
    public void Update()
    {
        if (Velocity.Get() > maxSpeed)
        {
            PlayAudio();
        } else
        {
            warning.SetActive(false);
            UpdateIndicatorsPosition(Velocity.Get());
        }
    }

    void PlayAudio()
    {
        if (warning.activeSelf == false && !audio.isPlaying)
        {
            audio.Play();
            warning.SetActive(true);
        }
    }

    void UpdateIndicatorsPosition(float speed)
    {
        var rotation = indicator.transform.rotation.eulerAngles;
        rotation.z = startRotation - Map(speed, 0, maxSpeed, 0, rotationOffset);
        indicator.transform.rotation = Quaternion.Euler(rotation);
    }
}
