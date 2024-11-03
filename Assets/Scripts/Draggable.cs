using UnityEngine;

[RequireComponent(typeof(Attachable))]
public class Draggable : MonoBehaviour
{
    private const float SNAP_DISTANCE = 0.2f;
    private const float ROTATE_DISTANCE = 0.8f;

    private bool isDragging;
    private Vector3 mouseOffset;

    private Attachable attachable;

    void Start()
    {
        attachable = GetComponent<Attachable>();
    }

    void Update()
    {
        if (isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                UpdateDragging();
            }
            else
            {
                isDragging = false;
            }
        }
    }

    private void UpdateDragging()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = mousePos + mouseOffset;

        if (attachable.attachmentRoot == null)
        {
            transform.position = pos;
            return;
        }

        Vector3 rootPos = pos + (attachable.attachmentRoot.position - transform.position);

        Attachable[] attachmables = FindObjectsByType<Attachable>(FindObjectsSortMode.None);
        Transform nearestAttachmentPoint = null;
        Attachable nearestAttachable = null;
        float nearestDistSq = Mathf.Infinity;
        foreach (Attachable attachable in attachmables)
        {
            foreach (Transform attachmentPoint in attachable.attachmentPoints)
            {
                float distSq = (attachmentPoint.position - pos).sqrMagnitude;
                if (distSq < nearestDistSq)
                {
                    nearestDistSq = distSq;
                    nearestAttachmentPoint = attachmentPoint;
                    nearestAttachable = attachable;
                }
            }
        }
        if (nearestAttachmentPoint != null)
        {
            float rootDistSq = (nearestAttachmentPoint.position - rootPos).sqrMagnitude;
            if (rootDistSq < SNAP_DISTANCE * SNAP_DISTANCE)
            {
                if (transform.GetComponent<SpriteRenderer>())
                {
                    transform.GetComponent<SpriteRenderer>().sortingOrder = 1;
                }
                attachable.Attach(nearestAttachable, nearestAttachmentPoint);
            }
            else
            {
                if (transform.GetComponent<SpriteRenderer>())
                {
                    transform.GetComponent<SpriteRenderer>().sortingOrder = 0;
                }
                attachable.Detach();
            }
            if (nearestDistSq < ROTATE_DISTANCE * ROTATE_DISTANCE && !nearestAttachable.IsConnected(nearestAttachmentPoint))
            {
                float rootAngle = (attachable.attachmentRoot.eulerAngles.z - transform.eulerAngles.z);
                transform.rotation = Quaternion.Euler(0, 0, (nearestAttachmentPoint.eulerAngles.z + 180) - rootAngle);
            }
        }
        if(!attachable.IsConnected())
        {
            transform.position = pos;
        }
    }

    private void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseOffset = transform.position - mousePos;
        isDragging = true;
    }
}
