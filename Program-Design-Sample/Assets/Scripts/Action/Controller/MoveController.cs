using System;
using UnityEngine;
/// <summary>
/// Rigidbodyコンポーネントの取得と平面移動の制御をする
/// </summary>
[Serializable]
public class MoveController
{
    [SerializeField] private Rigidbody rigid;
    private Vector3 Velocity;

    public void ReferenceFind(GameObject gameObject)
    {
        gameObject.TryGetComponent(out rigid);
    }

    public void Move(Vector2 velocity)
    {
        var localVelocity = rigid.rotation * new Vector3(velocity.x, 0, velocity.y);
        rigid.velocity = new Vector3(localVelocity.x, rigid.velocity.y, localVelocity.z);
    }
    public void SmoothMove(Vector2 velocity,float smoothTime=0,bool isLocal=false)
    {
        var localVelocity = rigid.rotation * new Vector3(velocity.x, 0, velocity.y);
        rigid.velocity = Vector3.SmoothDamp(rigid.velocity,new Vector3(localVelocity.x,rigid.velocity.y,localVelocity.z),ref Velocity,smoothTime);
    }
}
