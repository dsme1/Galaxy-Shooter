using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField] // 0 = TripleShot 1 = Speed 2 = Shields
    public int _powerupID;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        void DestroyThisObject()
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            player player = other.transform.GetComponent<player>();

            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;

                    case 1:
                        player.SpeedActive();
                        break;

                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("Not recognised");
                        break;
                }
            }

            DestroyThisObject();
            
        }
    }
}
