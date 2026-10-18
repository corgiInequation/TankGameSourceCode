using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletPool : MonoBehaviour
{
    //对象池大致实现逻辑
    //底层原理为一个队列，其实用栈也可以
    //我需要有一个队列作为存储对象的地方，然后有两个方法：一个方法为从队列取出子弹，另外一个方法为把用完的子弹丢回去
    // Start is called before the first frame update
    private static BulletPool instance;
    public static BulletPool Instance => instance;
    public bool isInitted = false;
    private List<GameObject> activatedBullets = new List<GameObject>();
    private Queue<GameObject> bulletPool1 = new Queue<GameObject>();
    private Queue<GameObject> bulletPool2 = new Queue<GameObject>();
    private Queue<GameObject> bulletPool3 = new Queue<GameObject>();
    private Queue<GameObject> bulletPool4 = new Queue<GameObject>();
    private HashSet<GameObject> activeBullet = new HashSet<GameObject>();


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
        //避免重复创建管理器，以及误删正常的管理器
        //因为这个脚本对应的gameobject是不会随着场景变化删除的，所以每次到LoadingScene场景，都会多一个管理器，这个时候就需要把这个管理器销毁
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        CreateBulletPool();
    }
    public void CreateBulletPool()
    {
        for(int i = 0; i < poolSize; i++)
        {
            bulletPool1.Enqueue(CreateBullet("Bullet1"));
            bulletPool2.Enqueue(CreateBullet("Bullet2"));
            bulletPool3.Enqueue(CreateBullet("Bullet3"));
            bulletPool4.Enqueue(CreateBullet("Bullet4"));
        }
        isInitted = true;
    }
    GameObject CreateBullet(string name)
    {
        GameObject bullet;
        if(name == "Bullet1")
        {
            bullet = Instantiate(bullet1, this.transform);
            bullet.name = name;
        }
        else if(name == "Bullet2")
        {
            bullet = Instantiate(bullet2, this.transform);
            bullet.name = name;
        }
        else if(name == "Bullet3")
        {
            bullet = Instantiate(bullet3, this.transform);
            bullet.name = name;
        }
        else if(name == "Bullet4")
        {
            bullet = Instantiate(bullet4, this.transform);
            bullet.name = name;
        }
        else
            bullet = null;
        bullet.SetActive(false);
        // print("here3");
        return bullet;
    }
    public Bullet UseBullet(string name)
    {
        GameObject bullet;
        if(name == "Bullet1")
        {
            if(bulletPool1.Count != 0)
            {
                bullet = bulletPool1.Dequeue();
            }
            else
            {
                bullet = Instantiate(bullet1);
                bullet.transform.SetParent(this.transform);
                bullet.name = name;
            }
                
        }
        else if(name == "Bullet2")
        {
            if(bulletPool2.Count != 0)
            {
                bullet = bulletPool2.Dequeue();
            }
            else {
                bullet = Instantiate(bullet2);
                bullet.name = name;
                bullet.transform.SetParent(this.transform);
            }
            
        }
        else if(name == "Bullet3")
        {
            if(bulletPool3.Count != 0)
            {
                bullet = bulletPool3.Dequeue();
            }
            else {
                bullet = Instantiate(bullet3);
                bullet.name = name;
                bullet.transform.SetParent(this.transform);
            }
        }
        else if(name == "Bullet4")
        {
            if(bulletPool4.Count != 0)
            {
                bullet = bulletPool4.Dequeue();
            }
            else {
                bullet = Instantiate(bullet4);
                bullet.name = name;
                bullet.transform.SetParent(this.transform);
            }
        }
        else
        {
            return null;
        }
        activeBullet.Add(bullet);
        bullet.SetActive(true);
        return bullet.GetComponent<Bullet>();
    }
    public void ReturnBullet(Bullet bullet)
    {
        GameObject bulletGameObject = bullet.gameObject;
        if(!activeBullet.Remove(bulletGameObject))
        {
            return;
        }
        bulletGameObject.SetActive(false);
        //对于每个子弹，在回收的时候，需要对其复原，比如清空其fatherTank
        bullet.nowTime = 0;
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
    //此外，还需要写一个整体回收函数，把当前所有活跃状态的Bullet全部回收，防止进入到下一个场景
    public void ReturnAllActiveBullet()
    {
        List<GameObject> list = new List<GameObject>(activeBullet);
        for(int i = 0; i < list.Count; i++)
        {
            ReturnBullet(list[i].GetComponent<Bullet>());
        }   
    }
}
