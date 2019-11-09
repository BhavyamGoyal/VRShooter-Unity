using System.Collections;
using System.Collections.Generic;
public class Singleton<T>
    where T : Singleton<T>, new()
{

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
}
