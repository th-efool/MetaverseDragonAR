using UnityEngine;
using System.Collections;
using System.Timers;

public class RotateDragons : MonoBehaviour
{
    [SerializeField] float FullRotationPerSecond=0.8f;
    int SelectionDragon=0;
    bool OngoingRotation=false;
    int NumberOfDragonTypes = System.Enum.GetValues(typeof(DragonType)).Length;
    public void Rotate(float angle)
    {
        if (!OngoingRotation) { StartCoroutine(SlerpItDown(angle));} 

    }

    IEnumerator SlerpItDown(float angle)
    {
        if (angle > 0) { SelectionDragon++; }else { SelectionDragon--; }
        if (SelectionDragon < 0 || SelectionDragon >= NumberOfDragonTypes)
        {
            SelectionDragon=((SelectionDragon % NumberOfDragonTypes) + NumberOfDragonTypes) % NumberOfDragonTypes;
        }
        Debug.Log(SelectionDragon);

        OngoingRotation = true;
        float alpha = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion finalRotation = transform.rotation * Quaternion.Euler(0, angle, 0);
        while (alpha < 1.0f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, finalRotation, alpha);
            alpha += FullRotationPerSecond * Time.deltaTime;
            yield return null;
        }
        transform.rotation = finalRotation; //for precision
        OngoingRotation = false;
        PlayerData.Instance.SetDragonChoice((DragonType)SelectionDragon);
    }

}

/*  transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(0, angle, 0), alpha);
 * If you try put this in IEnumerator or eent update this wont work since what tranform.rotation reperesent would always get updated each time, so like whats the celling is keep getting pushed you'll get cranky results which you dont want
 * What we wanna execute is controlled rotation.
 * So we'll store value of rotation at start point and end point so we can smoothly slerp
 */