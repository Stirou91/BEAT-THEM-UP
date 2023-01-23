using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMV2 : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float borderLeft = 350f;
    [SerializeField] float borderRight = 1000f;

    float screenPosXPlayer;
    PlayerSM pm;
    bool right;
    private void Start()
    {
        pm = target.GetComponent<PlayerSM>();
    }



    // Update is called once per frame
    void Update()
    {
        screenPosXPlayer = Camera.main.WorldToScreenPoint(target.transform.position).x;
        //Debug.Log(screenPosXPlayer); //VERIFICATION 

        // SI LE PLAYER DEPASSE LES BORDURES DROITE OU GAUCHE
        if (screenPosXPlayer > borderRight && pm || screenPosXPlayer < borderLeft && !pm || target.transform.parent != null)
        {
            Follow();

        }
        //// SI LE PLAYER DEPASSE LES BORDURES DROITE
        //else if ((screenPosXPlayer < borderLeft && !pm.right))

        //{
        //    Follow();

        //}
        // SI LE PLAYER N'A PS DEPASSER LES BORDURES
        else
        {
            transform.SetParent(null);
        }
    }

    private void Follow()
    {
        transform.SetParent(target.transform);

        // FIX POSE Y
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);


    }
}
