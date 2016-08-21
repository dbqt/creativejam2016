using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Transform Propulsor1, Propulsor2;
    public float FirePartial;
    public float PropulsorRange;
    public float PropulsorForce;
    public float FireExtinguishingPower;
    public float MaxSpeed;
    public LayerMask GroundMask;

    public AudioClip[] audioClips;
    public AudioClip[] collisionClips;
    public float soundPlayRate;

    private float nextSoundPlay = 0.0f;
    private bool needsReplacement = true;

    private bool needsCollSoundReplacement = true;

    private Vector3 projectedPropulsor1;
    private Vector3 projectedPropulsor2;

    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        projectedPropulsor1 = Vector3.zero;
        projectedPropulsor2 = Vector3.zero;

        audioSource = gameObject.GetComponent<AudioSource>();
        SoundReplacer();

    }
	
    void SoundReplacer()
    {
        if (needsReplacement == true && audioSource.isPlaying == false)
        {
            audioSource.clip = audioClips[Mathf.FloorToInt(Random.Range(0, 3))];
            needsReplacement = false;
        }
    }

    void CollisionSoundReplacer()
    {
        if (needsCollSoundReplacement == true && audioSource.isPlaying == false)
        {
            audioSource.clip = collisionClips[Mathf.FloorToInt(Random.Range(0, 4))];
            needsCollSoundReplacement = false;
        }
    }


	void FixedUpdate () {
        // add translation movement
        float angle1 = Propulsor1.eulerAngles.y;
        float angle2 = Propulsor2.eulerAngles.y;

        projectedPropulsor1 = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle1), 0f, Mathf.Cos(Mathf.Deg2Rad * angle1));
        projectedPropulsor2 = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle2), 0f, Mathf.Cos(Mathf.Deg2Rad * angle2));

        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle1), 0f, Mathf.Cos(Mathf.Deg2Rad * angle1)).normalized * PropulsorForce);
        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle2), 0f, Mathf.Cos(Mathf.Deg2Rad * angle2)).normalized * PropulsorForce);

        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(this.gameObject.GetComponent<Rigidbody>().velocity, MaxSpeed);

        //rotate player body
        Vector3 direction = this.gameObject.GetComponent<Rigidbody>().velocity;
        transform.GetChild(transform.childCount - 1).LookAt(transform.position + direction.normalized);
    }

    void OnCollisionEnter (Collision other)
    {
        Debug.Log("Collision! audioSource.clip = " + audioSource.clip);
        if (other.gameObject.GetComponent<PlayerMovement>() != null && Time.time > nextSoundPlay && audioSource.clip != null)
        {
            nextSoundPlay = Time.time + soundPlayRate;
            needsCollSoundReplacement = true;
            audioSource.Play();
        }
    }
    void Update ()
    {
        
        if (Input.anyKeyDown && Time.time > nextSoundPlay && audioSource.clip != null)
        {
            nextSoundPlay = Time.time + soundPlayRate;
            //audioSource.Play();
            needsReplacement = true;
        }
        
        if (needsReplacement && Time.time > nextSoundPlay)
        {
            SoundReplacer();
        }

        if (needsCollSoundReplacement && Time.time > nextSoundPlay)
        {
            CollisionSoundReplacer();
        }


        Quaternion proAngle = Quaternion.identity;

        proAngle.eulerAngles = new Vector3(0f, -Propulsor1.eulerAngles.y, 0f);
        
        Collider[] grounds1 = Physics.OverlapBox(
            transform.position - projectedPropulsor1*PropulsorRange/2,
            new Vector3(0f, 3f, PropulsorRange/4),
            proAngle,
            GroundMask);

        proAngle.eulerAngles = new Vector3(0f, -Propulsor2.eulerAngles.y, 0f);

        Collider[] grounds2 = Physics.OverlapBox(
            transform.position - projectedPropulsor2* PropulsorRange/2 ,
            new Vector3(0f, 3f, PropulsorRange/4),
            proAngle,
            GroundMask);

        foreach (Collider o in grounds1)
        {
            o.gameObject.GetComponent<GroundController>().node.flameRate -= FireExtinguishingPower* PropulsorRange/Vector3.Distance(o.transform.position,transform.position)*Time.deltaTime;
        }
        foreach (Collider o in grounds2)
        {
            o.gameObject.GetComponent<GroundController>().node.flameRate -= FireExtinguishingPower * PropulsorRange / Vector3.Distance(o.transform.position, transform.position) * Time.deltaTime;
        }
    }

    void playSound()
    {

    }


}
