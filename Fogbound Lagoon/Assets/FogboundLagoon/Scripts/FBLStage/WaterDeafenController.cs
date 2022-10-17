using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoR2;
using AK;

namespace FBLStage.Content
{
    public class WaterDeafenController : MonoBehaviour
    {
        public float elevation;

        
        void OnEnable()
        {
            On.RoR2.MusicController.RecalculateHealth += MusicController_RecalculateHealth;
        }

        void OnDisable()
        {
            On.RoR2.MusicController.RecalculateHealth -= MusicController_RecalculateHealth;
        }

        private void MusicController_RecalculateHealth(On.RoR2.MusicController.orig_RecalculateHealth orig, MusicController self, GameObject playerObject)
        {
            orig(self, playerObject);
            if (self.targetCamera)
            {
                if (self.targetCamera.transform.position.y < elevation)
                {
                    self.rtpcPlayerHealthValue.value = 0f;
                }
            }

        }
    }

}
