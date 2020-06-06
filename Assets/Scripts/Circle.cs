using UnityEngine;
using UnityEngine.EventSystems;

public class Circle : Upgradable, IPointerClickHandler
{
    static Cube theCube;
    [SerializeField] GameObject myCircle;
    [SerializeField] GameObject myUpgradeButton;
    [SerializeField] Collider2D myCollider;
    Animator myAnimator;
    float t;
    Vector2 randomLocation;
    Vector2 currentLocation;
    Vector2 originalLocation;
    float wanderTime;
    bool clicked;
    private void Awake()
    {
        originalLocation = transform.position;
        myAnimator = GetComponent<Animator>();
        if (!theCube)
        {
            theCube = FindObjectOfType<Cube>();
        }
    }
    void ShootCube()
    {
        Bullet bulletToFire = BulletPoolManager.PullABullet();
        bulletToFire.transform.position = myCircle.transform.position;
        bulletToFire.transform.rotation = myCircle.transform.rotation;
        bulletToFire.myWorth = goldPerTap;
        bulletToFire.gameObject.SetActive(true);
    }
    private void Update()
    {
        myCircle.transform.right = theCube.transform.position - myCircle.transform.position;
        Wander();
    }
    public void Appear()
    {
        IntializeWanderBehaviour();
        InvokeRepeating("ShootCube", Random.Range(0, 2f), 1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!clicked)
        {
            clicked = true;
            myCollider.enabled = false;
            EventSystem.current.SetSelectedGameObject(myUpgradeButton.gameObject);
            myAnimator.SetBool("ButtonAppear",true);
        }
    }

    void IntializeWanderBehaviour()
    {
        t = 0;
        currentLocation = transform.position;
        randomLocation = originalLocation + Random.insideUnitCircle * 0.5f;
        wanderTime = Random.Range(2, 5);
    }
    void Wander()
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
    public void OnUpgradeButtonDeselect()
    {
        if (clicked)
        {
            myAnimator.SetBool("ButtonAppear",false);
            clicked = false;
            myCollider.enabled = true;
        }
    }

}
