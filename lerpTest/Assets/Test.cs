using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {
    public enum TestMode
    {
        lerp, circle
    }
    private Text mModeText;
    private const float MAX_ANGLE = 360.0f;
    public TestMode mPlayMode = TestMode.lerp;


    public float mNormarize = 1.0f;
    public float mNowTime = 0.0f;

    [SerializeField] private GameObject mPointObjA;
    [SerializeField] private GameObject mPointObjB;

    [SerializeField] private GameObject mPointerObj;
    [SerializeField] private GameObject mCenterObj;
    [SerializeField] private GameObject mGomiObject;


    public bool isTurn = false;
    public bool isAuto = false;

    public float mTestAngle = 0;
    public float mCircleSpeed = 10.0f;
    public bool isReversal = false;
    public bool isHalf = false;
    // Use this for initialization
    void Start () {
        mModeText = GameObject.Find("Text").GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
       
        switch (mPlayMode)
        {
            case TestMode.lerp:
                mModeText.text = "Lerpテスト";
                modeLerp();
                    break;
            case TestMode.circle:
                mModeText.text = "円移動テスト";
                modeCircle();
                break;
        }

    }
    #region らーぷ
    void modeLerp()
    {
        if (Input.GetKey("l") && !isAuto)
        {
            TestLerp(mNowTime);
            if (isTurn)
            {
                mNowTime -= Time.deltaTime;
            }
            else
            {
                mNowTime += Time.deltaTime;
            }
            if (mNowTime <= 0 || mNowTime >= mNormarize) { isTurn = !isTurn; }
        }
        if (Input.GetKey("s")) { TestLerp(mNowTime); }
        if (Input.GetKeyDown("a")) { isAuto = !isAuto; }
        if (isAuto)
        {
            TestLerp(mNowTime);
            if (isTurn)
            {
                mNowTime -= Time.deltaTime;
            }
            else
            {
                mNowTime += Time.deltaTime;
            }
            if (mNowTime < 0 || mNowTime >= mNormarize) { isTurn = !isTurn; }
        }
    }
    public void TestLerp(float pos)
    {
        Vector3 pointA = mPointObjA.transform.position;
        Vector3 pointB = mPointObjB.transform.position;

        float pointerPos = pos / mNormarize;

        mPointerObj.transform.position = pointA + ((pointB - pointA) * pointerPos);
    }
    #endregion
    void modeCircle()
    {
        if (Input.GetKey(KeyCode.D)) { mGomiObject.transform.Rotate (1, 0, 0); }
        if (!mCenterObj)
        {
            mCenterObj = new GameObject();
            mPointerObj.transform.parent = mCenterObj.transform;
        }
        if (Input.GetKeyDown(KeyCode.A)) { isAuto = !isAuto; }
        if (isAuto)
        {
            if (isHalf)
            {
                Vector3 pointA = mPointObjA.transform.position;
                Vector3 pointB = mPointObjB.transform.position;
                float correctionAngle = Mathf.Atan2(pointB.y - pointA.y, Mathf.Sqrt(Mathf.Pow(pointB.z - pointA.z, 2) + Mathf.Pow(pointB.x - pointA.x, 2))) * Mathf.Deg2Rad;
                Debug.Log(correctionAngle);

                Vector3 radius = (pointB - pointA) / 2;
                float minAngle = radius.y / (Vector3.Magnitude(mPointObjB.transform.position - mPointObjA.transform.position) / 2) * Mathf.Deg2Rad;


                if (!isTurn) { mTestAngle += Time.deltaTime * mCircleSpeed; }
                else         { mTestAngle -= Time.deltaTime * mCircleSpeed; }
                if(Mathf.Sin(mTestAngle * Mathf.Deg2Rad)+ correctionAngle < minAngle ) { isTurn = !isTurn; }
            }
            else
            {
                if (!isReversal)
                {
                    mTestAngle += Time.deltaTime * mCircleSpeed;
                    if (mTestAngle >= MAX_ANGLE) { mTestAngle = 0.0f; }
                }
                else
                {
                    mTestAngle -= Time.deltaTime * mCircleSpeed;
                    if (mTestAngle <= 0) { mTestAngle = MAX_ANGLE; }
                }
            }       
            TestCircle();
        }
        if (Input.GetKey(KeyCode.S)) { TestCircle(); }
    }
    void TestCircle()
    {
      
        Vector3 pointA = mPointObjA.transform.position;
        Vector3 pointB = mPointObjB.transform.position;
        float correctionAngle = Mathf.Atan2(pointB.y - pointA.y, Mathf.Sqrt(Mathf.Pow(pointB.z - pointA.z, 2) + Mathf.Pow(pointB.x - pointA.x, 2)));
        mCenterObj.transform.position = pointA + ((pointB - pointA) * 0.5f);
 
        mCenterObj.transform.LookAt(mPointObjA.transform.position);
        Vector3 centerPos = (mPointObjB.transform.position + mPointObjA.transform.position)/2;

        centerPos.x += Mathf.Sin(mTestAngle * Mathf.Deg2Rad) * Vector3.Magnitude(mPointObjB.transform.position - mPointObjA.transform.position) / 2;

        centerPos.y += Mathf.Cos(mTestAngle * Mathf.Deg2Rad) * Vector3.Magnitude(mPointObjB.transform.position - mPointObjA.transform.position) / 2;
       
        mPointerObj.transform.localPosition = centerPos - mCenterObj.transform.position;
    }
}

//   sqrt 平方根　　pow  乗数
//atan2(Pc[1].Pos.y-Pc[0].Pos.y, sqrt(pow(Pc[1].Pos.z-Pc[0].Pos.z,2)+pow(Pc[1].Pos.x-Pc[0].Pos.x,2)))
//atan2(pointB.y - pointA.y  , Mathf.sprt(mathf.pow(pointB.z - pointA.z , 2) + Mathf.pow(pointB.x - pontA.x,2)))
//     高さ                      斜辺を求める                    底辺を求めてる 