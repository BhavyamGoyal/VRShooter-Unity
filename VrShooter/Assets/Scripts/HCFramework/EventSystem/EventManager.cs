using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: Delegates are callback/pointers to the methods.

namespace HCFramework
{
    public class EventManager
    {
        public delegate void EventDelegate<T>(T e) where T : GameEvent;
        public delegate void EventDelegate(GameEvent gameEvent);

        //this holds same type of delegates in same element even if classes are different
        private readonly Dictionary<System.Type, EventDelegate> multicastDelegateDictionary = new Dictionary<System.Type, EventDelegate>();
        //this hold different classes delegate as different element
        private readonly Dictionary<System.Delegate, EventDelegate> singleCastDelegateDictionary = new Dictionary<System.Delegate, EventDelegate>();

        private static EventManager instance = null;

        public static EventManager Instance
        {
            get 
            {
                if (instance == null)
                    instance = new EventManager();

                return instance;
            }
        }

        public void AddListner<T>(EventDelegate<T> del) where T : GameEvent
        {
            //check if we have the specific delegate in out dictionary
            if (singleCastDelegateDictionary.ContainsKey(del))
                return;

            //convert the generic delegate to non generic delegate
            EventDelegate internalDelegate = e => del((T)e);

            //add the delegate to dictionary
            singleCastDelegateDictionary[del] = internalDelegate;

            EventDelegate tempDel;

            //we check if we have the give type of delegates and if yes
            //then we add the values to "tempDel"
            if (multicastDelegateDictionary.TryGetValue(typeof(T), out tempDel))
            {
                //we add the new delegate to the list
                tempDel += internalDelegate;
                //and update the dictionary
                multicastDelegateDictionary[typeof(T)] = tempDel;
            }
            else //else we create a new element with key and value
                multicastDelegateDictionary[typeof(T)] = internalDelegate;
        }


        public void RemoveListner<T>(EventDelegate<T> del) where T : GameEvent
        {
            //Debug.Log("Cab be Removed");

            EventDelegate internalDelegate;

            if (singleCastDelegateDictionary.TryGetValue(del, out internalDelegate))
            {
                EventDelegate tempDel;

                if (multicastDelegateDictionary.TryGetValue(typeof(T), out tempDel))
                {
                    tempDel -= internalDelegate;

                    if (tempDel == null)
                        multicastDelegateDictionary.Remove(typeof(T));
                    else
                        multicastDelegateDictionary[typeof(T)] = tempDel;

                    singleCastDelegateDictionary.Remove(del);

                    //Debug.Log("Removed");
                }
            }
        }

        public void TriggerEvent(GameEvent gameEvent)
        {
            EventDelegate eventDelegate;
            if(multicastDelegateDictionary.TryGetValue(gameEvent.GetType(), out eventDelegate))
            {
                eventDelegate.Invoke(gameEvent); 
            }
        }
    }
}