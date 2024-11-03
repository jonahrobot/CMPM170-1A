using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SimpleMovement : MonoBehaviour
{
    public float speed = 3f;
    float moveHorizontal;
    float moveVertical;

    private void Start()
    {
        StartCoroutine("Beat");
    }

    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        transform.Translate(movement * speed * Time.deltaTime);

        if (moveHorizontal == 0 && moveVertical == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    IEnumerator Beat()
    {
        Debug.Log("Beat");
        yield return new WaitForSeconds(1f);
        Debug.Log("Beat: " + moveVertical);
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            if (transform.rotation.eulerAngles.z == 15)
            {
                transform.rotation = Quaternion.Euler(0, 0, -15);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 15);
            }
        }
        StartCoroutine("Beat");
    }
}