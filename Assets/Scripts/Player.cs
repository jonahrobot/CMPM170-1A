using UnityEngine;

[RequireComponent(typeof(Attachable))]
public class Player : MonoBehaviour
{
    public float speed = 1.0f;

    private Attachable attachable;

    void Start()
    {
        attachable = GetComponent<Attachable>();
    }

    void Update()
    {
        Vector2 movement = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attachable.Step();
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector2.right;
        }
        transform.Translate(movement.normalized * Time.deltaTime * speed);
    }
}
