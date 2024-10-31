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

    public void Step(int index)
    {
        foreach (Attachable attachment in connectedAttachements)
        {
            if (attachment.parentAttachmentPoint == attachmentPoints[index])
            {
                attachment.Step();
                return;
            }
        }
    }

    public void Attach(Attachable attachable, Transform attachmentPoint)
    {
        if(attachable.IsConnected(attachmentPoint))
        {
            return;
        }
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

    public bool IsConnected(Transform attachmentPoint)
    {
        foreach (Attachable attachment in connectedAttachements)
        {
            if (attachment.parentAttachmentPoint == attachmentPoint)
            {
                return true;
            }
        }
        return false;
    }
}
