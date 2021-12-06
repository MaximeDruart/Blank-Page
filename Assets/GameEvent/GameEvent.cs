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
        if (onOpen != null)
        {
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


}
