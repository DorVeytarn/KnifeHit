using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightPopup : Popup
{
    public override void Open()
    {
        base.Open();
        StartCoroutine(AnimatableDelay());
    }

    private IEnumerator AnimatableDelay()
    {
        yield return new WaitForSeconds(2f);
        Close();
    }
}
