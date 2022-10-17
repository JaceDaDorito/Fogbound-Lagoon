using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoR2;
using UnityEngine.Networking;

namespace FBLStage.Content
{
    public class BadToTheBone : NetworkBehaviour
    {
        private const string BadToTheBoneEventName = "FBL_BadtotheBone";

        [ClientRpc]
        public void RpcPlayBadToTheBone()
        {
            AkSoundEngine.PostEvent(BadToTheBoneEventName, gameObject);
        }
    }
}

