using HCFramework;
using UnityEngine;
using System;
using System.Collections;

namespace HCFramework.NetworkSystem
{
    public class CommunicateWithServerEvent<T, Y> : GameEvent
    {
        public RequestType requestType;
        public Y data;
        public Action<T> callback;
        public CommunicateWithServerEvent(RequestType requestType, Y data,Action<T> callback)
        {
            this.requestType = requestType;
            this.data = data;
            this.callback = callback;
        }
    }
    public class StartCoroutineEvent : GameEvent
    {
        public IEnumerator request;
        public StartCoroutineEvent(IEnumerator request)
        {
            this.request = request;
        }

    }



}