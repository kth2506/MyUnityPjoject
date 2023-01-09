using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Object
{
    public override void Initialize()
    {
        base.Value = 2;
        base.Key = "Player";

        Debug.Log(base.Key + " : " + base.Value);
    }

    public override void Progress()
    {
    }

    public override void Release()
    {
    }

    public override string GetKey()
    {
        return base.Key;
    }

}
