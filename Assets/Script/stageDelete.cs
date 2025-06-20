using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageDelete : MonoBehaviour
{
    public createStage createStage;
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("floor"))
        {
            createStage.DeleteStage();
            createStage.CreateStage();

        }
        else
        {
            Destroy(other.gameObject);

        }
        

        
    }
}
