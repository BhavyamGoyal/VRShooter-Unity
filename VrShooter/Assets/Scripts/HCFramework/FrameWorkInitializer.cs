using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCFramework.UI;
using HCFramework.NetworkSystem;

namespace HCFramework
{
    public class FrameWorkInitializer : MonoBehaviour
    {
        [SerializeField]
        private UIEnum startScreen;
        private UIController uIController;
        // Start is called before the first frame update
        void Start()
        {
            //uIController = new GameUIController();
            //uIController.Initialize(startScreen);
            //uIController.GetScreens(this.gameObject);
        }
        private void OnEnable()
        {
            EventManager.Instance.AddListner<StartCoroutineEvent>(StartCoroutineListener);
        }
        private void OnDestroy()
        {
            EventManager.Instance.RemoveListner<StartCoroutineEvent>(StartCoroutineListener);
        }


        void StartCoroutineListener(StartCoroutineEvent data)
        {
            StartCoroutine(data.request);
        }
    }
}
