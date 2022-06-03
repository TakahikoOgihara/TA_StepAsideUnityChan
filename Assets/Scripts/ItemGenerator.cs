using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject coinPrefab;
    public GameObject conePrefab;
    private int startPos = 80;
    private int goalPos = 360;
    private float posRange = 3.4f;

    /*
    TechAcademyの発展課題（Start関数で一気にcoinなどのアイテムを表示させるのではなく、Unitychanの表示範囲にのみアイテムを発生させる）に
    対応するために
    ・myUnitychanをGameObjectで宣言して、GameObject.FindでUnitychanのオブジェクトを紐づけた
    ・教材のようにイベント関数Start()でいっぺんにアイテムをすべて表示ではなく、Update（）でアイテム発生をさせた
    ・ifでUnitychanのtransform.position.z の前後50の範囲内のときのみ、教材のコードと同じように、
    　if/for/Random.Range/InstantiateなどでUnityちゃんの対象のz軸範囲内でのみアイテムをUpdateで都度発生させている。
    ・Updateで都度アイテムを生成すると重い上に、アイテムが重なるためかスクリプトにエラーがでないがPlayすると重くて途中でとまってしまう。
    　Destroyでなんとかできないか検討したが、都度発生による重さは解決しないし、つけたり消えたりがフレームごとに発生はちかちかしそうなので、考えても根本解決がみつからないのでアドバイスをいただきたい。

    */
    public GameObject myUnitychan;

    void Start()
    {
        //In order to answer the KADAI of tech academy,
        this.myUnitychan = GameObject.Find("unitychan");
    }

    // Update is called once per frame

    

    void Update()
    {
        // Unityちゃんの現在地のZ軸を15で割った余りが0の場合のみが、アイテム発生位置であり、それ以外はifとreturnで処理を走らせない。
        float UniZ = myUnitychan.transform.position.z;
        float ItemGenZ = UniZ + 50;

        if (ItemGenZ % 15 != 0)
        {
            return;
        }
        else if (startPos <= UniZ && UniZ <= goalPos)
        {
            int num = Random.Range(1, 11);

            if (num <= 2)
            {
                for (float j = -1; j <= 1; j += -0.4f)
                {
                    GameObject cone = Instantiate(conePrefab);
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, ItemGenZ);
                }
            }
            else
            {
                for (int j = -1; j <= 1; j++)
                {
                    int item = Random.Range(1, 11);
                    int offsetZ = Random.Range(-5, 6);

                    if (1 <= item && item <= 6)
                    {
                        GameObject coin = Instantiate(coinPrefab);
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, ItemGenZ + offsetZ);
                    }
                    if (7 <= item && item <= 9)
                    {
                        GameObject car = Instantiate(carPrefab);
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, ItemGenZ + offsetZ);
                    }
                }
            }
        }
    }
}
