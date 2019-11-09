using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCFramework
{
    public static class Utils
    {
        
        public static void EventAsync(GameEvent HCEvent)
        {
            EventManager.Instance.TriggerEvent(HCEvent);
        }
    }
}