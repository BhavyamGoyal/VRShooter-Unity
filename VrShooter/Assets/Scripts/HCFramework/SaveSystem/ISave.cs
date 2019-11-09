using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCFramework.SaveSystem
{
    public interface ISave
    {
        void SaveData<T>(string key, T value);
        T LoadData<T>(string key);

        bool HasKey<T>(string key);
    }
}