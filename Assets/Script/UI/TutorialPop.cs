using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TutorialPop : UEPopup
{
    public static TutorialPop Instance;
    public static TutorialPop ShowPop()
    {
        Instance = UEPopup.GetInstantiateComponent<TutorialPop>();
        Instance.ShowPopUp();
        return Instance;
    }
}
