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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            attachable.Step();
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        transform.Translate(movement * speed * Time.deltaTime);

    }
}
