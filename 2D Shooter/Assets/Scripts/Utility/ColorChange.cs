using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{

    [SerializeField] CanvasRenderer randerObject;
    [SerializeField] float seconds;
    float timer = 0.0f;
    bool blueToGreen = true;
    bool greenToRed = false;
    bool redToBlue = false;

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime / seconds;

        if (blueToGreen == true && greenToRed == false && redToBlue == false)
        {
            randerObject.SetColor(Color.Lerp(Color.blue, Color.green, timer));
            if (timer >= 1.0f)
            {
                timer = 0.0f;
                blueToGreen = false;
                greenToRed = true;
            }
        }

        if (greenToRed == true && blueToGreen == false && redToBlue == false)
        {
            randerObject.SetColor(Color.Lerp(Color.green, Color.red, timer));
            if (timer >= 1.0f)
            {
                timer = 0.0f;
                greenToRed = false;
                redToBlue = true;
            }
        }

        if (redToBlue == true && greenToRed == false && blueToGreen == false)
        {
            randerObject.SetColor(Color.Lerp(Color.red, Color.blue, timer));
            if (timer >= 1.0f)
            {
                timer = 0.0f;
                redToBlue = false;
                blueToGreen = true;
            }
        }
    }
}
