using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace HCFramework.UI
{
    public abstract class BaseUI : MonoBehaviour
    {
        public bool canGoBack;
        public UIEnum screenID;

        public virtual void Awake()
        {
            Button[] buttons = GetComponentsInChildren<Button>(true);
            for (int i = 0; i < buttons.Length; i++)
            {
                //using n instead of i to avoid variable capturing error
                int n = i;
                buttons[i].onClick.AddListener(() => { OnClick(buttons[n]); });
            }
        }

        public virtual void OnClick(Button btn)
        {
            Utils.EventAsync(new BtnClickEvent(btn));
            switch (btn.name)
            {
                case "Back":

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Add listner to specific button
        /// </summary>
        /// <param name="buttonName"></param>
        protected void AddListnerToButtons(Button btn)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => OnClick(btn));
        }

        /// <summary>
        /// Add listner to array of buttons
        /// </summary>
        /// <param name="buttons"></param>
        protected void AddListnerToButtons(Button[] buttons)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                //using n instead of i to avoid variable capturing error
                int n = i;
                buttons[n].onClick.RemoveAllListeners();
                buttons[n].onClick.AddListener(() => OnClick(buttons[n]));
            }
        }
    }
}