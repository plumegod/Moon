using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    public void Fire()
    {
        EventBus<GunFireEvent>.Raise(new GunFireEvent());
    }
}
