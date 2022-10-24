using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoR2;
using UnityEngine.Networking;
using RoR2.Networking;
using FBLStage.Content;

namespace FBLStage.Content
{
    public class BadToTheBone : NetworkBehaviour
    {

        [ClientRpc]
        public void RpcPlayBadToTheBone()
        {
            if (FBLContent.BadToTheBone)
                EffectManager.SimpleSoundEffect(FBLContent.BadToTheBone.index, gameObject.transform.position, true);
        }
    }
}

