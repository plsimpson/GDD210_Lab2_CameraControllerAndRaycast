using UnityEngine;

public class NewRay : MonoBehaviour
{
    //public bool Hit;
    //public string HitObj;

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            
            if(hit.transform.CompareTag("Friend"))
            {
                Debug.DrawLine(transform.position, hit.point, Color.green);
                if (Input.GetMouseButtonDown(0))
                {
                    //hit.transform.GetComponent<CharacterHP>().TakeDamage(3);
                    hit.transform.position += transform.forward;
                }
            }
            else if(hit.transform.CompareTag("Enemy"))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);

                if(Input.GetMouseButtonDown(0))
                {
                    //hit.transform.GetComponent<CharacterHP>().TakeDamage(3);
                    hit.transform.position += transform.forward;  
                }
            }
            
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.forward * 100f, Color.white);
        }
    }
}