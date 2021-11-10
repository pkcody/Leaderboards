using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rig;
    private float startTime = 10f;
    private float timer;
    private int collectablesPicked;
    public int totalCollectables;
    private bool isPlaying = false;
    public GameObject playButton;
    public TextMeshProUGUI curTimeText;

    public float timesPunched;



    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isPlaying)
            return;

        if (timer <= 0f)
        {
            Debug.Log("in");
            End();
            Debug.Log("after");

        }
        //float x = Input.GetAxis("Horizontal") * speed;
        //float z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Vertical"))
        {
            Debug.Log("up");
            rig.AddForce(new Vector3(0f, 0f, 75f));
        }

        //rig.velocity = new Vector3(x, rig.velocity.y, z);
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }

        curTimeText.text = (timer).ToString("F2");

    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            collectablesPicked++;
            Debug.Log(collectablesPicked);

            Destroy(other.gameObject);
        }
    }

    public void Begin()
    {
        Debug.Log("begining!");
        timer = startTime;
        isPlaying = true;
        playButton.SetActive(false);
    }

    void End()
    {
        Debug.Log("end");
        rig.isKinematic = true;
        totalCollectables = collectablesPicked;
        isPlaying = false;
        playButton.SetActive(true);
        Debug.Log(totalCollectables);
        Leaderboard.instance.SetLeaderboardEntry(totalCollectables);

    }
}
