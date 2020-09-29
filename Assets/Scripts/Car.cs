
using UnityEngine;

public class Car : MonoBehaviour {
    private Animator animator;

    private bool left;
    private bool right;
    private static readonly int Right = Animator.StringToHash("right");
    private static readonly int Left = Animator.StringToHash("left");

    private Shaker shaker;

    public float maxVelocity = 150f;
    public float minVelocity = 40f;
    public float acceleration = 10f;
    
    public float roadWidth = 1.025f;
    public float max = 1.32f;

    public float adhesion = 0.3f;
    public float cornerSpeed = 10f;
    
    private float velocity = 0f;
    public float distance = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        shaker = GetComponent<Shaker>();
        velocity = minVelocity;
    }

    void Update()
    {
        CheckInput();
        UpdateAnim();
        distance += velocity * Time.deltaTime;

        var pos = transform.position;
        
        float curveOffset = Curvature.Get() * Time.deltaTime * velocity * adhesion;
        
        pos.x -= curveOffset;

        if (left)
        {
            pos.x = pos.x - (cornerSpeed * Time.deltaTime + Mathf.Abs(curveOffset * 0.4f));
        }

        if (right)
        {
            pos.x = pos.x + (cornerSpeed * Time.deltaTime + Mathf.Abs(curveOffset * 0.4f));
        }


        if (pos.x < -roadWidth || pos.x > roadWidth)
        {
            velocity = Mathf.Max(minVelocity, velocity * .8f);
            shaker.Shake();
        }
        
        pos.x = Mathf.Max(pos.x, -max);
        pos.x = Mathf.Min(pos.x, max);
        
        transform.position = pos;
        
        Distance.Set(distance);
        Velocity.Set(velocity);
    }

    void UpdateAnim()
    {
        animator.SetBool(Right, right);
        animator.SetBool(Left, left); 
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            left = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            left = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            right = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            right = false;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            velocity = Mathf.Min(velocity + acceleration * Time.deltaTime / Mathf.Sqrt(velocity / 12), maxVelocity);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            velocity = Mathf.Max(velocity - acceleration * Time.deltaTime, minVelocity);
        }
    }
    
    
}
