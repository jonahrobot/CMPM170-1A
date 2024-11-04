using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Attachable))]
public class Player : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    [Range(0f, 100f)]
    public float movementMax = 4.0f;
    [Range(0f, 10f)]
    public float movementMin = 1.0f;
    public float beatRateInSeconds = 1.0f;
    [Range(0f, 10f)]
    public float beatRateMin = 0.2f;
    [Range(0f, 10f)]
    public float beatRateMax = 1f;

    int beatTrack = 1;

    private Attachable attachable;

    void Start()
    {
        attachable = GetComponent<Attachable>();
        StartCoroutine("beat");
    }

    void Update()
    {
        // Move
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        transform.Translate(movement * movementSpeed * Time.deltaTime);

        // Accelerate and decelerate
        if (movement.magnitude > 0.02f)
        {
            movementSpeed = Mathf.Clamp(movementSpeed * 1.05f, movementMin, movementMax);
            beatRateInSeconds = Mathf.Clamp(beatRateInSeconds - (0.15f * Time.deltaTime), beatRateMin, beatRateMax);
        }
        else
        {
            movementSpeed = Mathf.Clamp(movementSpeed * 0.75f, movementMin, movementMax);
            beatRateInSeconds = Mathf.Clamp(beatRateInSeconds + (1f * Time.deltaTime), beatRateMin, beatRateMax);
        }

        // Pulse
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

        for (int i = 0; i < 3; i++)
        {
            if(Random.Range(0, 3) == 0)
            {
                attachable.Step(i);
            }
        }

        //// Play sound main
        //attachable.Step(1);

        //// Play every other beat
        //if(beatTrack % 2 == 0) attachable.Step(2);

        //// Play every fourth beat
        //if (beatTrack % 4 == 0) attachable.Step(0);

        beatTrack += 1;
        transform.localScale = new Vector3(1.5f, 1.5f, 1);

        StartCoroutine("beat");
    }
}
