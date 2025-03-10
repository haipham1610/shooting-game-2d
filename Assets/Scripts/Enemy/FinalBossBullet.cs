using UnityEngine;

public class FinalBossBullet : MonoBehaviour
{
    private Vector3 moveDirection;
    void Start()
    {
        Destroy(gameObject,5f);
    }

    void Update()
    {
        if (moveDirection == Vector3.zero) return;
        transform.position += moveDirection*Time.deltaTime;
    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }
}
