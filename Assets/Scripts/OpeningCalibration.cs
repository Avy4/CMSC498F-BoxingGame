using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class OpeningCalibration : MonoBehaviour
{
    [SerializeField] UIDocument userInterface;
    [SerializeField] Texture2D[] stances;
    private enum DominantHand {LEFT, RIGHT, EMPTY}
    private DominantHand hand = DominantHand.EMPTY;
    private VisualElement rootElement;
    private Label topText, bottomText;
    private Image stanceImage;

    void Start()
    {
        rootElement = userInterface.rootVisualElement;
        topText = rootElement.Query<Label>("TopText");
        bottomText = rootElement.Query<Label>("BottomText");
        stanceImage = rootElement.Query<Image>("StanceImage");
    }

    void OnTriggerEnter(Collider other)
    {
        if (hand == DominantHand.EMPTY) {
            if (other.gameObject.tag == "GloveLeft")
            {
                hand = DominantHand.RIGHT;
            }
            else if (other.gameObject.tag == "GloveRight")
            {
                hand = DominantHand.LEFT;
            }

            ShowResult();
        }
        else
        {
            ShowImage();
        }
    }

    void ShowResult()
    {   
        String leadHand = "left";
        String stance = "Orthodox";

        if (hand == DominantHand.LEFT)
        {
            leadHand = "right";
            stance = "Southpaw";
        }

        topText.text = $"Looking at the punch you threw, your lead hand is your {leadHand} hand. This means your dominant hand is probably your {hand.ToString()} hand. Given this information your most natural stance and my recommendation is the {stance} stance";
        bottomText.text = $"{stance} stance means this: your {leadHand} hand and {leadHand} leg are the ones leading. The {leadHand} is the one for jabbing, your {hand.ToString()} throws powerful straights/crosses (which are the same thing). Hit the dummy to continue.";
    }

    void ShowImage()
    {   
        topText.text = "Using the line on the ground as a guide, keep your legs shoulder with apart and get your body facing ~45°. Use the image below as a guide.";
        bottomText.text = "";
        stanceImage.image = stances[(int)hand];
    }
}
