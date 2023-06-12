using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigid;
    private Vector3 Velocity;
    
    private void Reset()
    {
        TryGetComponent(out rigid);
    }
    public void SmoothMove(Vector2 velocity,float smoothTime=0)
    {
        rigid.velocity = Vector3.SmoothDamp(rigid.velocity,new Vector3(velocity.x,0,velocity.y),ref Velocity,smoothTime);
    }
}
