using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HCFramework.UI
{
    public abstract class UIController
    {
        private UIEnum startScreen;

        private BaseUI[] baseUIs;
        private Stack uiScreens;
        private ToastController toastController;
        private BaseUI currentActive;

        public void Initialize(UIEnum startScreen)
        {
            toastController = new ToastController();
            uiScreens = new Stack();
            this.startScreen = startScreen;
            AddListner();
        }

        public void Terminate()
        {
            baseUIs = null;
            RemoveListner();
        }

        public void GetScreens(GameObject uiCanvas)
        {
            baseUIs = uiCanvas.transform.GetComponentsInChildren<BaseUI>(true);

            for (int i = 0; i < baseUIs.Length; i++)
            {
                baseUIs[i].gameObject.SetActive(false);
                if (baseUIs[i].screenID == startScreen)
                {
                    //using n instead of i to avoid variable capturing error
                    int n = i;
                    baseUIs[n].gameObject.SetActive(true);
                    currentActive = baseUIs[n];
                }
            }
        }

        public void AddListner()
        {
            EventManager.Instance.AddListner<BtnClickEvent>(BtnClickEventListner);
            EventManager.Instance.AddListner<SwitchScreenEvent>(SwitchEventListner);
        }

        public void RemoveListner()
        {
            EventManager.Instance.RemoveListner<BtnClickEvent>(BtnClickEventListner);
            EventManager.Instance.RemoveListner<SwitchScreenEvent>(SwitchEventListner);
        }

        private void BtnClickEventListner(BtnClickEvent btnClickEvent)
        {
            OnClick(btnClickEvent.button);
        }

        public virtual void OnClick(Button button)
        {
            if (button.name == "Back")
                GoBackScreen();
        }

        private void SwitchEventListner(SwitchScreenEvent screenEvent)
        {
            SwitchScreen(screenEvent.screenID);
        }

        protected void SwitchScreen(UIEnum screenId)
        {
            for (int i = 0; i < baseUIs.Length; i++)
            {
                baseUIs[i].gameObject.SetActive(false);
                if (baseUIs[i].screenID == screenId)
                {
                    baseUIs[i].gameObject.SetActive(true);
                    if (baseUIs[i].canGoBack == true)
                    {
                        if (uiScreens.Count != 0)
                        {
                            BaseUI lastInStack = uiScreens.Peek() as BaseUI;
                            if (lastInStack.screenID != currentActive.screenID)
                                uiScreens.Push(currentActive);
                        }
                        else
                            uiScreens.Push(currentActive);
                    }
                    currentActive = baseUIs[i];
                }
            }
        }

        private void GoBackScreen()
        {
            BaseUI prevUIScreen = (BaseUI)uiScreens.Pop();
            for (int i = 0; i < baseUIs.Length; i++)
                baseUIs[i].gameObject.SetActive(false);

            prevUIScreen.gameObject.SetActive(true);
            currentActive = prevUIScreen;
        }

    }
}