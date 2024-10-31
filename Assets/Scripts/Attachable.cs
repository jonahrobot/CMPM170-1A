using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using TreeEditor;
using UnityEngine;
using UnityEngine.Events;

public class Attachable : MonoBehaviour
{
    public Transform attachmentRoot;
    public List<Transform> attachmentPoints;
    public UnityEvent stepHandler;

    private Attachable parent;
    private Transform parentAttachmentPoint;
    private List<Attachable> connectedAttachements = new List<Attachable>();

    void Start()
    {
    }

    void Update()
    {
        if (parent != null)
        {
            transform.position = parentAttachmentPoint.position - (attachmentRoot.position - transform.position);
        }
    }

    public void Step()
    {
        stepHandler.Invoke();
        foreach (Attachable attachment in connectedAttachements)
        {
            attachment.Step();
        }
    }

    public void Attach(Attachable attachable, Transform attachmentPoint)
    {
        if(parent != attachable)
        {
            Detach();
            parent = attachable;
            parentAttachmentPoint = attachmentPoint;
            parent.connectedAttachements.Add(this);
        }
    }

    public void Detach()
    {
        if(parent != null)
        {
            parent.connectedAttachements.Remove(this);
            parent = null;
            parentAttachmentPoint = null;
        }
    }

    public bool IsConnected()
    {
        return parent != null;
    }
}
