using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PunchingBag : MonoBehaviour
{
    [SerializeField] UIDocument userInterface;
    [SerializeField] AudioSource audioSource;
    private VisualElement rootElement;
    private Label velocityValue, contactAreaValue, hitCounterLabel;
    private float threshold = .3f;
    private int hitCounter = 0;

    // Init private UI Document variables
    void Start()
    {
        rootElement = userInterface.rootVisualElement;
        velocityValue = rootElement.Query<Label>("VelocityValue");
        contactAreaValue = rootElement.Query<Label>("ContactAreaValue");
        hitCounterLabel = rootElement.Query<Label>("HitCounterLabel");
    }

    // Checks for a collision with the bag
    void OnCollisionEnter(Collision collision)
    {
        GameObject otherCollisionObject = collision.collider.gameObject;

        if (otherCollisionObject.tag == "GloveLeft" || otherCollisionObject.tag == "GloveRight")
        {   
            PlaySound(collision);
            GetAndSetImpactVelocity(otherCollisionObject.tag);
            GetAndSetCollisionPosition(collision);
            AddToHitCounter();
        }
    }

    // Plays a sound based on the impact force of the punch
    void PlaySound(Collision c)
    {
        float impactForce = c.relativeVelocity.magnitude;
        if (impactForce > threshold)
        {
            audioSource.volume = Mathf.Clamp01(impactForce / 3);
            audioSource.Play();
        }
    }

    // Changes velocity on the dashboard, only works if you use the actual controller (Not hand tracking)
    void GetAndSetImpactVelocity(String tag)
    {
        Vector3 linearVelocity = new Vector3(100,100,100);
        if (tag == "GloveLeft")
        {
            linearVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
        }
        else if (tag == "GloveRight")
        {
            linearVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
        }
        
        velocityValue.text = $"{linearVelocity} m/s";
    }

    // Gets the point of collision and adds it to the dashboard
    void GetAndSetCollisionPosition(Collision c)
    {
        ContactPoint contact = c.GetContact(0);
        Vector3 impactPoint = contact.point;

        contactAreaValue.text = $"{impactPoint}";
    }

    // +1
    void AddToHitCounter()
    {
        hitCounter += 1;
        hitCounterLabel.text = $"Hit Counter: {hitCounter}";
    }
}
