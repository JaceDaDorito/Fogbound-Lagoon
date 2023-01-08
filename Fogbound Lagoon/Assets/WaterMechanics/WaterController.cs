using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using RoR2;
using AK;

public class WaterController : MonoBehaviour
{
    /*Features Checklist
     
    - Seperate music and sound muffle for what
    - Improve physics (?) Like lowering acceleration and making free fall speed lower
    - Particle effects for entering the water
    - Particle effects for being in the water
    - Make Localized water controllers
    - Drowning lmao
     
    */


    public float elevation;
    public float muffleValue = 100;
    public float minimumRtpcValue = -100;
    public float antiGravCoeff;

    void FixedUpdate()
    {
        foreach (CharacterBody body in CharacterBody.readOnlyInstancesList)
        {
            if (body.corePosition.y >= elevation || !body.characterMotor)
                continue;

            CharacterMotor characterMotor = body.characterMotor;

            if (characterMotor && !characterMotor.isGrounded && characterMotor.useGravity && characterMotor.hasEffectiveAuthority)
            {
                //body.characterMotor.ApplyForce(-Physics.gravity * antiGravCoeff, false, false);

                characterMotor.velocity.y += -Physics.gravity.y * antiGravCoeff * Time.fixedDeltaTime;
            }
            /*else if (body.rigidbody)
            {
                body.rigidbody.AddForce(-Physics.gravity * antiGravCoeff, ForceMode.Acceleration);
            }*/

        }
    }
    void OnEnable()
    {
        On.RoR2.MusicController.RecalculateHealth += MusicController_RecalculateHealth;
    }

    void OnDisable()
    {
        On.RoR2.MusicController.RecalculateHealth -= MusicController_RecalculateHealth;
    }

    private void MusicController_RecalculateHealth(On.RoR2.MusicController.orig_RecalculateHealth orig, RoR2.MusicController self, GameObject playerObject)
    {
        orig(self, playerObject);
        if (self.targetCamera)
        {
            if (self.targetCamera.transform.position.y < elevation)
            {
                self.rtpcPlayerHealthValue.value -= muffleValue;
                self.rtpcPlayerHealthValue.value = Mathf.Max(self.rtpcEnemyValue.value, minimumRtpcValue);
            }
        }

    }
}
