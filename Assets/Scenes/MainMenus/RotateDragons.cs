using UnityEngine;
using System.Collections;
using System.Timers;

public class RotateDragons : MonoBehaviour
{
    [SerializeField] float FullRotationPerSecond=0.8f;
    bool OngoingRotation=false;
    public void Rotate(float angle)
    {
        if (!OngoingRotation) { StartCoroutine(SlerpItDown(angle));} 

    }

    IEnumerator SlerpItDown(float angle)
    {
        OngoingRotation = true;
        float alpha = 0;
        Debug.Log("Inside enumerator" + alpha);
        Quaternion startRotation = transform.rotation;
        Quaternion finalRotation = transform.rotation * Quaternion.Euler(0, angle, 0);
        while (alpha < 1.0f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, finalRotation, alpha);
            alpha += FullRotationPerSecond * Time.deltaTime;
            Debug.Log("YO!! I'm WORKING HERE " + alpha);
            yield return null;
        }
        transform.rotation = finalRotation; //for precision
        OngoingRotation = false;
    }

}

/*  transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(0, angle, 0), alpha);
 * If you try put this in IEnumerator or eent update this wont work since what tranform.rotation reperesent would always get updated each time, so like whats the celling is keep getting pushed you'll get cranky results which you dont want
 * What we wanna execute is controlled rotation.
 * So we'll store value of rotation at start point and end point so we can smoothly slerp
 */