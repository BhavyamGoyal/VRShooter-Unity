using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCFramework.SaveSystem
{
    public class PlayerPrefSave : ISave
    {
        public PlayerPrefSave()
        {
            DebugMsg("Created");
        }

        public bool HasKey<T>(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                DebugMsg(key + ": Key Found");
                return true;
            }

            DebugMsg(key + ": Key Not Found");

            return false;
        }

        public T LoadData<T>(string key)
        {
            var value = new object();

            if (typeof(T) == typeof(int))
            {
                value = PlayerPrefs.GetInt(key);
            }
            else if (typeof(T) == typeof(float))
            {
                value = PlayerPrefs.GetFloat(key);
            }
            else if (typeof(T) == typeof(string))
            {
                value = PlayerPrefs.GetString(key);
            }

            return (T)value;
        }

        public void SaveData<T>(string key, T value)
        {
            if (typeof(T) == typeof(int))
            {
                PlayerPrefs.SetInt(key, (int)Convert.ChangeType(value, typeof(int)));
            }
            else if (typeof(T) == typeof(float))
            {
                PlayerPrefs.SetFloat(key, (float)Convert.ChangeType(value, typeof(float)));
            }
            else if (typeof(T) == typeof(string))
            {
                PlayerPrefs.SetString(key, (string)Convert.ChangeType(value, typeof(string)));
            }
        }

        private void DebugMsg(string msg)
        {
            Debug.Log("<color=green>[PlayerPrefSave] </color>" + msg);
        }
    }
}