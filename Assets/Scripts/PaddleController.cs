using UnityEngine;
using Vector2 = UnityEngine.Vector2;


public class PaddleController : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected KeyCode up, down;
        
    protected Rigidbody2D _rbPaddle;
    private Vector2 _velocity;

    private void Awake()
    {
        _rbPaddle = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        HumanMove();
    }

    protected virtual void FixedUpdate()
    {
        MovePaddle();
    }
    
    private void MovePaddle()
    {
        _rbPaddle.MovePosition(_rbPaddle.position + _velocity * Time.fixedDeltaTime);
    }
    
    private void HumanMove()
    {
        
        Vector2 vertical = Input.GetKey(up) ? Vector2.up : Input.GetKey(down) ? Vector2.down : Vector2.zero;
        SetVelocity(vertical);
    }

    protected void SetVelocity(Vector2 movement)
    {
        _velocity = movement * speed;
    }
}