using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace HCFramework.UI
{
    public class BtnClickEvent : GameEvent
    {
        public Button button;

        public BtnClickEvent(Button button)
        {
            this.button = button;
        }
    }

    public class ShowToastEvent : GameEvent
    {
        public string toastText;
        public int time;
        public ShowToastEvent(string toastText,int timeInMilliSec)
        {
            this.toastText = toastText;
            this.time = timeInMilliSec;
        }
    }

    public class SwitchScreenEvent : GameEvent
    {
        public UIEnum screenID;

        public SwitchScreenEvent(UIEnum screenID)
        {
            this.screenID = screenID;
        }
    }
}