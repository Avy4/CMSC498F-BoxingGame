using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OpeningCalibration : MonoBehaviour
{
    [SerializeField] UIDocument userInterface;
    [SerializeField] Texture2D[] stances;
    [SerializeField] string nextScene;
    private enum DominantHand {LEFT, RIGHT, EMPTY}
    private DominantHand hand = DominantHand.EMPTY;
    private VisualElement rootElement;
    private Label topText, bottomText;
    private Image stanceImage;
    private bool readyToExit = false;

    // Init private UI Document variables
    void Start()
    {
        rootElement = userInterface.rootVisualElement;
        topText = rootElement.Query<Label>("TopText");
        bottomText = rootElement.Query<Label>("BottomText");
        stanceImage = rootElement.Query<Image>("StanceImage");
    }

    // Probably a better system but this is simple
    void OnTriggerEnter(Collider other)
    {   
        // If a dominant hand hasnt been found (i.e you arent past the first slide)
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
        // Once you get a dominant hand this is the next branch that it enters
        else if (!readyToExit)
        {
            ShowImage();
            readyToExit = true;
        }
        // Scene switching branch
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    // Changes the text on the second slide based on what hand you punched with
    void ShowResult()
    {   
        String leadHand = "left";
        String stance = "Orthodox";

        if (hand == DominantHand.LEFT)
        {
            leadHand = "right";
            stance = "Southpaw";
        }

        topText.text = $"Looking at the punch you threw, your lead hand is your {leadHand} hand. This means your dominant hand is probably your {hand.ToString().ToLower()} hand. Given this information your most natural stance and my recommendation is the {stance.ToLower()} stance";
        bottomText.text = $"{stance} stance means this: your {leadHand} hand and {leadHand} leg are the ones leading. The {leadHand} is the one for jabbing, your {hand.ToString().ToLower()} throws powerful straights/crosses (which are the same thing). Hit the dummy to continue.";
    }

    // Changes the image on the third slide based on what hand you punched with
    void ShowImage()
    {   
        topText.text = "Using the line on the ground as a guide, keep your legs shoulder with apart and get your body facing ~45°. Use the image below as a guide.";
        bottomText.text = "";
        stanceImage.image = stances[(int)hand];
    }
}
