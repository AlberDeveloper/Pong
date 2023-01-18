using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float thrust;
    private Rigidbody2D _rbBall;

    void Start()
    {
        _rbBall = GetComponent<Rigidbody2D>();
        Serve(Vector2.left);
    }

    public void Serve(Vector2 direction)
    {
        _rbBall.position = Vector2.zero;
        SetVelocity(direction);
    }

    private void SetVelocity(Vector2 velocity)
    {
        _rbBall.velocity = new Vector2(velocity.x * thrust, velocity.y);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Player2"))
        {
            var velocity = Vector2.left;
            if (col.gameObject.CompareTag("Player")) velocity = Vector2.right;
            velocity.y = _rbBall.velocity.y;
            SetVelocity(velocity);
        }
    }
}
