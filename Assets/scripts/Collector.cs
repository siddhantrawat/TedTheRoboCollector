using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A collecting game object
/// </summary>
public class Collector : MonoBehaviour
{
    #region Fields

    // targeting support
    SortedList<Target> targets = new SortedList<Target>();
    Target targetPickup = null;
    
    // movement support
    const float BaseImpulseForceMagnitude = 2.0f;
    const float ImpulseForceIncrement = 0.3f;

    // saved for efficiency
    Rigidbody2D rb2d;
    string a;
    #endregion

    #region Methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        // center collector in screen
        Vector3 position = transform.position;
        position.x = 0;
        position.y = 0;
        position.z = 0;
        transform.position = position;

        // save reference for efficiency
        rb2d = GetComponent<Rigidbody2D>();

        // add as listener for pickup spawned event
        EventManager.AddListener(AddToSortedList);
    }

    /// <summary>
    /// Called when another object is within a trigger collider
    /// attached to this object
    /// </summary>
    /// <param name="other"></param
    /// >
    
    void OnTriggerStay2D(Collider2D other)
    {

        // only respond if the collision is with the target pickup
        if (other.gameObject == targetPickup.GameObject)
        {

            // remove collected pickup from list of targets and game
            targets.IndexOf(targetPickup);
            Debug.Log("destroy index : " + targets.IndexOf (targetPickup));
            targets.RemoveAt(targets.IndexOf(targetPickup));

            Destroy(targetPickup.GameObject);
            
            if (targets.Count != 0)
            {
                for (int i = 0; i < targets.Count; i++)
                {
                    targets[i].UpdateDistance(transform.position);
                }

                //support code 
                /*
                a = a + "unsorted list    ";
                for (int i = 0; i < targets.Count; i++)
                    a = a + "," + targets[i].Distance;
                Debug.Log(a + "and target pickup equals " + targetPickup.Distance);
                a = " ";
                */
                targets.Sort();
                /*
                a = a + "sorted list    ";
                for (int i = 0; i < targets.Count; i++)
                    a = a + "," + targets[i].Distance;
                Debug.Log(a + "and target pickup equals " + targetPickup.Distance);
                a = " ";
                */

                SetTarget(targets[targets.Count - 1].GameObject);
                //Debug.Log("new target set");
            }
            else           
              SetTarget(null);


        }
    }
    
    void AddToSortedList(GameObject pickup)
    {

        Target target = new Target(pickup.gameObject, gameObject.transform.position);
        targets.Add(target);
        
        
        if (targetPickup != null)
        {
            if (target.Distance < targetPickup.Distance)
            {
                //Debug.Log("new closest target");
                SetTarget(pickup);
            }
        }
        else
        {
             SetTarget(pickup);

        }
        /*
        for (int i = 0; i < targets.Count; i++)
            a = a + "," + targets[i].Distance;
        Debug.Log(a + "and target pickup equals " + targetPickup.Distance);
        a = " ";
        */


    }

    /// <summary>
    /// Sets the target pickup to the provided pickup
    /// </summary>
    /// <param name="pickup">Pickup.</param>
    void SetTarget(GameObject pickup)
    {
        targetPickup = new Target (pickup.gameObject, gameObject.transform.position);
        GoToTargetPickup();
    }

    /// <summary>
    /// Starts the teddy bear moving toward the target pickup
    /// </summary>
    
      void GoToTargetPickup()
    {
        Debug.Log("moving toward " + targets.IndexOf(targetPickup));
        // calculate direction to target pickup and start moving toward it
		Vector2 direction = new Vector2(
			targetPickup.GameObject.transform.position.x - transform.position.x,
			targetPickup.GameObject.transform.position.y - transform.position.y);
		direction.Normalize();
		rb2d.velocity = Vector2.zero;
        
		rb2d.AddForce(direction * (BaseImpulseForceMagnitude + ImpulseForceIncrement * GameObject.FindGameObjectsWithTag("Pickup").Length),
			ForceMode2D.Impulse);
	}
	

#endregion
}
