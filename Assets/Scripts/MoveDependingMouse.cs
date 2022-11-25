using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDependingMouse : MonoBehaviour
{
    public Vector3 mouseDelta;
    public float sensitivity = 2f;
    private Rigidbody2D rb;

    private void Start()
    {
        /*Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;*/

        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
        if(mouseDelta.magnitude <= 100 && Cursor.lockState == CursorLockMode.Locked)
        {
            rb.velocity = mouseDelta * sensitivity;
            /*Vector3 lerpedValue = Vector3.MoveTowards(transform.position, transform.position + mouseDelta, sensitivity * Time.deltaTime);
            
            transform.position = lerpedValue;*/
        } else {
            
            Debug.Log(mouseDelta);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        RaycastHit obstacle;
 
        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, 1 << LayerMask.NameToLayer("Floor")))
        {
            if (Physics.Raycast(new Vector3(hit.point.x, 0.5f, hit.point.z), new Vector3(((GetComponent<BoxCollider>().size.x / 2) + 0.05f) * (-1), 0, 0), out obstacle, 0.6f) || Physics.Raycast(new Vector3(hit.point.x, 0.5f, hit.point.z), new Vector3((GetComponent<BoxCollider>().size.x / 2) + 0.05f, 0, 0), out obstacle, 0.6f) || Physics.Raycast(new Vector3(hit.point.x, 0.5f, hit.point.z), new Vector3(0, 0, ((GetComponent<BoxCollider>().size.z / 2) + 0.05f) * (-1)), out obstacle, 0.6f) || Physics.Raycast(new Vector3(hit.point.x, 0.5f, hit.point.z), new Vector3(0, 0, (GetComponent<BoxCollider>().size.z / 2) + 0.05f), out obstacle, 0.6f))
            {
                if (obstacle.collider.gameObject.name == GetComponent<Rigidbody>().name)
                    GetComponent<Rigidbody>().MovePosition(new Vector3(hit.point.x, 0.43f, hit.point.z));
            }
        }
    }
}
