using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gomi2 : MonoBehaviour {
    public bool isMove = false;
    public float mMoveSpeed = 1.0f;
    public float mRusius = 2.0f;
    float mNowTime = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { isMove = !isMove; }
        if (isMove)
        {

            mNowTime += Time.deltaTime * mMoveSpeed;
            Vector3 moveDerection = new Vector3(0, 0, 0);
            moveDerection.z += Mathf.Cos(mNowTime) * mRusius;
            //moveDerection.y += Mathf.Sin(mNowTime) * mRusius;
            transform.position = moveDerection;
        }
    }
}
