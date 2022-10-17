using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using RoR2;
public class WaterAntiGravController : MonoBehaviour
{
    public float elevation;
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

    
}
