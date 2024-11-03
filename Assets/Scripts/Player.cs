using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Attachable))]
public class Player : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float beatRateInSeconds = 1.0f;

    int beatTrack = 1;

    private Attachable attachable;

    void Start()
    {
        attachable = GetComponent<Attachable>();
        StartCoroutine("beat");
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

        transform.Translate(movement * movementSpeed * Time.deltaTime);

        if(transform.localScale.x > 1 && transform.localScale.y > 1)
        {
            transform.localScale = new Vector3(transform.localScale.x - 1f * Time.deltaTime, transform.localScale.y - 1f * Time.deltaTime, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    IEnumerator beat()
    {
        yield return new WaitForSeconds(beatRateInSeconds);

        // Play sound main
        attachable.Step(1);

        // Play every other beat
        if(beatTrack % 2 == 0) attachable.Step(2);

        // Play every fourth beat
        if (beatTrack % 4 == 0) attachable.Step(0);

        beatTrack += 1;
        transform.localScale = new Vector3(1.5f, 1.5f, 1);

        StartCoroutine("beat");
    }
}
