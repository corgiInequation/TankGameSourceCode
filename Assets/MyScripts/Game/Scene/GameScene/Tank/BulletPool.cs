using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    //对象池大致实现逻辑
    //底层原理为一个队列，其实用栈也可以
    //我需要有一个队列作为存储对象的地方，然后有两个方法：一个方法为从队列取出子弹，另外一个方法为把用完的子弹丢回去
    // Start is called before the first frame update
    private static BulletPool instance;
    public static BulletPool Instance => instance;
    private Queue<GameObject> bulletPool1 = new Queue<GameObject>();
    private Queue<GameObject> bulletPool2 = new Queue<GameObject>();
    private Queue<GameObject> bulletPool3 = new Queue<GameObject>();
    private Queue<GameObject> bulletPool4 = new Queue<GameObject>();


    [SerializeField]
    private int poolSize = 100;
    [SerializeField]
    private GameObject bullet1;
    [SerializeField]
    private GameObject bullet2;
    [SerializeField]
    private GameObject bullet3;
    [SerializeField]
    private GameObject bullet4;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {

        for(int i = 0; i < poolSize; i++)
        {
            bulletPool1.Enqueue(CreateBullet("Bullet1"));
            bulletPool2.Enqueue(CreateBullet("Bullet2"));
            bulletPool3.Enqueue(CreateBullet("Bullet3"));
            bulletPool4.Enqueue(CreateBullet("Bullet4"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    GameObject CreateBullet(string name)
    {
        GameObject bullet;
        print(name);
        if(name == "Bullet1")
        {
            print("here1");
            print(bullet1);
            bullet = Instantiate(bullet1);
            print("here2");
        }
        else if(name == "Bullet2")
            bullet = Instantiate(bullet2);
        else if(name == "Bullet3")
            bullet = Instantiate(bullet3);
        else if(name == "Bullet4")
            bullet = Instantiate(bullet4);
        else
            bullet = null;
        bullet.SetActive(false);
        print("here3");
        return bullet;
    }
    public Bullet UseBullet(string name, Transform transform)
    {
        GameObject bullet;
        if(name == "Bullet1")
        {
            if(bulletPool1.Count != 0)
            {
                bullet = bulletPool1.Dequeue();
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
            }
            else
                bullet = Instantiate(bullet1, transform.position, transform.rotation);
        }
        else if(name == "Bullet2")
        {
            if(bulletPool2.Count != 0)
            {
                bullet = bulletPool2.Dequeue();
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
            }
            else
                bullet = Instantiate(bullet2, transform.position, transform.rotation);
        }
        else if(name == "Bullet3")
        {
            if(bulletPool3.Count != 0)
            {
                bullet = bulletPool3.Dequeue();
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
            }
            else
                bullet = Instantiate(bullet3, transform.position, transform.rotation);
        }
        else if(name == "Bullet4")
        {
            if(bulletPool4.Count != 0)
            {
                bullet = bulletPool4.Dequeue();
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
            }
            else
                bullet = Instantiate(bullet4, transform.position, transform.rotation);
        }
        else
        {
            bullet = null;
        }
        bullet.SetActive(true);
        return bullet.GetComponent<Bullet>();
    }
    public void ReturnBullet(Bullet bullet)
    {
        GameObject bulletGameObject = bullet.gameObject;
        bulletGameObject.SetActive(false);
        if(bulletGameObject.name == "Bullet1")
        {
            bulletPool1.Enqueue(bulletGameObject);
        }
        else if(bulletGameObject.name == "Bullet2")
        {
            bulletPool2.Enqueue(bulletGameObject);
        }
        else if(bulletGameObject.name == "Bullet3")
        {
            bulletPool3.Enqueue(bulletGameObject);
        }
        else if(bulletGameObject.name == "Bullet4")
        {
            bulletPool4.Enqueue(bulletGameObject);
        }
    }
}
