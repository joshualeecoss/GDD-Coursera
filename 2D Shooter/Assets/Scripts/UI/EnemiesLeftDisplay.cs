using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class EnemiesLeftDisplay : UIelement
{
    [Tooltip("The text UI to use for display")]
    public Text displayText = null;

    void Start()
    {
        DisplayEnemiesLeft();
    }

    /// <summary>
    /// Description:
    /// Changes the enemies left display
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    public void DisplayEnemiesLeft()
    {
        if (displayText != null)
        {
            if (!GameManager.infiniteGame)
            {
                displayText.text = "Enemies Left: " + GameManager.enemiesLeft.ToString();
            }
            else
            {
                displayText.text = "Enemies Left: INFINITE";
            }
        }
    }

    /// <summary>
    /// Description:
    /// Overides the virtual UpdateUI function and uses the DisplayScore to update the score display
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    public override void UpdateUI()
    {
        // This calls the base update UI function from the UIelement class
        base.UpdateUI();

        // The remaining code is only called for this sub-class of UIelement and not others
        DisplayEnemiesLeft();
    }

}
