using Boo.Lang.Environments;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Circle : Upgradable, IPointerClickHandler
{
    static Cube theCube;
    public SpriteRenderer spriteRenderer;
    [SerializeField] GameObject myCircle;
    [SerializeField] GameObject myNozzle;
    [SerializeField] GameObject myUpgradeButton;
    [SerializeField] Collider2D myCollider;
    [SerializeField] Animator muzzleFlashAnimator;
    Animator myAnimator;
    float t;
    Vector2 randomLocation;
    Vector2 currentLocation;
    Vector2 originalLocation;
    float wanderTime;
    bool clicked;
    bool initialised;
    private void Awake()
    {
        originalLocation = transform.position;
        myAnimator = GetComponent<Animator>();
        if (!theCube)
        {
            theCube = FindObjectOfType<Cube>();
        }
    }
   new private void OnEnable()
    {
        base.OnEnable();
        EventManager.StartListening("Explode Circle", Explode);
    }
    new private void OnDisable()
    {
        base.OnDisable();
        EventManager.StopListening("Explode Circle", Explode);
    }

    private void Explode()
    {
        currentLevel = 0;
        PrestigeManager.SpawnExplosion(transform.position);
        gameObject.SetActive(false);
    }

    public override void PlayerPrestiged()
    {
        CancelInvoke();
    }

    void ShootCube()
    {
        Bullet bulletToFire = BulletPoolManager.PullABullet();
        bulletToFire.transform.position = myNozzle.transform.position;
        bulletToFire.transform.rotation = myNozzle.transform.rotation;
        bulletToFire.myWorth = goldPerTap;
        bulletToFire.gameObject.SetActive(true);
        muzzleFlashAnimator.SetTrigger("Shoot");
    }
    private void Update()
    {
        myCircle.transform.right = theCube.transform.position - myCircle.transform.position;
        Wander();
    }
    public void Appear()
    {
        StartCoroutine(AppearRoutine());
    }
    IEnumerator AppearRoutine()
    {
        myAnimator.SetTrigger("CircleAppear");
        yield return new WaitForSeconds(2);
        IntializeWanderBehaviour();
        initialised = true;
        InvokeRepeating("ShootCube", UnityEngine.Random.Range(0, 2f), 1);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!clicked)
        {
            clicked = true;
            myCollider.enabled = false;
            EventSystem.current.SetSelectedGameObject(myUpgradeButton.gameObject);
            myAnimator.SetBool("ButtonAppear", true);
        }
    }

    void IntializeWanderBehaviour()
    {
        t = 0;
        currentLocation = transform.position;
        randomLocation = originalLocation + UnityEngine.Random.insideUnitCircle * 0.8f;
        wanderTime = UnityEngine.Random.Range(2, 5);
    }
    void Wander()
    {
        if (initialised)
        {
            t += Time.deltaTime;
            if (transform.position.x != randomLocation.x)
            {
                transform.position = Vector2.Lerp(currentLocation, randomLocation, t / wanderTime);
            }
            else
            {
                IntializeWanderBehaviour();
            }
        }
    }
    public void OnUpgradeButtonDeselect()
    {
        if (clicked)
        {
            myAnimator.SetBool("ButtonAppear", false);
            clicked = false;
            myCollider.enabled = true;
        }
    }

}
