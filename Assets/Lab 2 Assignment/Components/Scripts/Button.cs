using UnityEngine;

public class Button : MonoBehaviour
{
    public Light light;
    public GameObject[] trapDoorObjects;

    public void Press()
    {
        light.enabled = !light.enabled;
        foreach (GameObject trapDoor in trapDoorObjects)
        {
            trapDoor.SetActive(!trapDoor.activeSelf);
        }
    }
}
