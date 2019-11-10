using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCFramework.UI;
using HCFramework.NetworkSystem;

namespace HCFramework
{
    public class FrameWorkInitializer : MonoBehaviour
    {
        public int zobieCount=1;
        public GameObject zombiePrefab;
        Dictionary<int,ZombieFollow> zombies = new Dictionary<int,ZombieFollow>();
        Transform playerTransform;
        void Start()
        {
            playerTransform = FindObjectOfType<OVRPlayerController>().transform;
            zombiePrefab = Resources.Load<GameObject>("zombiePrefab");
            for (int i = 0; i < zobieCount; i++)
            {
                zombies.Add(i,new ZombieFollow(zombiePrefab,playerTransform,i+""));
            }
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
