using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameEvent : ScriptableObject
{

    public delegate void OpenAction();
    public event OpenAction onOpen;
    public delegate void CloseAction();
    public event CloseAction onClose;

    public void open()
    {
        Debug.Log(onOpen);
        if (onOpen != null)
        {
            Debug.Log("starting open function");
            onOpen();
        }
    }

    public void close()
    {
        if (onClose != null)
        {
            onClose();
        }
    }


    public void test2()
    {
        Debug.Log("hely");
    }
    public void test()
    {
        onOpen += test2;
    }
}
