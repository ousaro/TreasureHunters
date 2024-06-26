using System;
using System.Collections.Generic;
using UnityEngine;

namespace Osaro.Utilities
{
    public class EventManager : MonoBehaviour
    {


        private  Dictionary<string, Delegate> eventDictionary = new Dictionary<string, Delegate>();

        // For parameterless events
        public  void StartListening(string eventName, Action listener)
        {
            if (eventDictionary.TryGetValue(eventName, out Delegate thisEvent))
            {
                thisEvent = Delegate.Combine(thisEvent, listener);
                eventDictionary[eventName] = thisEvent;
            }
            else
            {
                thisEvent = listener;
                eventDictionary.Add(eventName, thisEvent);
            }
        }

        public  void StopListening(string eventName, Action listener)
        {
            if (eventDictionary.TryGetValue(eventName, out Delegate thisEvent))
            {
                thisEvent = Delegate.Remove(thisEvent, listener);
                if (thisEvent == null)
                {
                    eventDictionary.Remove(eventName);
                }
                else
                {
                    eventDictionary[eventName] = thisEvent;
                }
            }
        }

        public  void TriggerEvent(string eventName)
        {
            if (eventDictionary.TryGetValue(eventName, out Delegate thisEvent))
            {
                (thisEvent as Action)?.Invoke();
            }
        }

        // For parameterized events
        public  void StartListening<T>(string eventName, Action<T> listener)
        {
            if (eventDictionary.TryGetValue(eventName, out Delegate thisEvent))
            {
                thisEvent = Delegate.Combine(thisEvent, listener);
                eventDictionary[eventName] = thisEvent;
            }
            else
            {
                thisEvent = listener;
                eventDictionary.Add(eventName, thisEvent);
            }
        }

        public  void StopListening<T>(string eventName, Action<T> listener)
        {
            if (eventDictionary.TryGetValue(eventName, out Delegate thisEvent))
            {
                thisEvent = Delegate.Remove(thisEvent, listener);
                if (thisEvent == null)
                {
                    eventDictionary.Remove(eventName);
                }
                else
                {
                    eventDictionary[eventName] = thisEvent;
                }
            }
        }

        public  void TriggerEvent<T>(string eventName, T param)
        {
            if (eventDictionary.TryGetValue(eventName, out Delegate thisEvent))
            {
                (thisEvent as Action<T>)?.Invoke(param);
            }
        }

       
    }

}
