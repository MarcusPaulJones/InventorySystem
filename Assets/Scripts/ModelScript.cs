using UnityEngine;
using System.Collections;

public class ModelScript : MonoBehaviour
{
    public float mSense = 0.4f;

    public float rSpeed = 0.4f;
    public float rotDamp = 1.0f;

    private Vector3 mRef;
    private Vector3 mOff;
    private Vector3 rot;
    private bool isRot;

    void Update()
    {
        if (isRot == true)
        {
            mOff = Input.mousePosition - mRef;
            rot.y = (mOff.x + mOff.y) * -mSense;
            transform.Rotate(rot);
            mRef = Input.mousePosition;
        }
        else
        {

            transform.rotation *= Quaternion.AngleAxis(rSpeed, Vector3.up);
            rot.y -= rotDamp * Time.deltaTime;
            rot.y = Mathf.Abs(rot.y) <= 1 ? 0 : rot.y;
        }
    }

    void OnMouseDown()
    {
        isRot = true;
        mRef = Input.mousePosition;
    }
    void OnMouseUp()
    {
        isRot = false;
    }
    //public float rotSpeed = 10.0f;
    //// Use this for initialization
    //void Start ()
    //{
    //    
    //}
    //
    //// Update is called once per frame
    //void Update ()
    //{
    //    transform.rotation = Quaternion.AngleAxis(rotSpeed,Vector3.up);
    //}
}
