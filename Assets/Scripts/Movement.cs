using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float speed = 3f; 

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        
        transform.Translate(movement * speed * Time.deltaTime);
    }
}