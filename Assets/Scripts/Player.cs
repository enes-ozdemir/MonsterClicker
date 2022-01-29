using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Player : MonoBehaviour
{
    UnityArmatureComponent armatureComponent;

    private void Start()
    {
        armatureComponent = GetComponent<UnityArmatureComponent>();
        //armatureComponent.animation.Play("Attack A");

        //armatureComponent.animation.FadeIn("Attack A", 0.25f, -1);

        /*   UnityFactory.factory.LoadDragonBonesData("PrivateAssets/Player/KingArthur_ske");
           UnityFactory.factory.LoadTextureAtlasData("PrivateAssets/Player/KingArthur_tex");

           var armatureComponent = UnityFactory.factory.BuildArmatureComponent("Player");

           armatureComponent.animation.Play("Idle");

           armatureComponent.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f); */
    }

    private void Update()
    {
        armatureComponent.animation.Play("Damage");
        Debug.Log("Attacked");


    }



}
