using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileMapGenerator : MonoBehaviour
{
    [Header("Values to create tile map")]
    [Tooltip("Hexagon which will be used for generation")]
    public ProcHexa hexagon;

    public bool createFlatMap = false;
    public bool waveOnUpdate = false;

    public int width = 10;
    public int height = 10;

    public float maxHeight = 10f;

    public float perlinScale = 10f;
    public Gradient grad;

    private ProcHexa[,] poleObjektu;


    // Start is called before the first frame update
    void Start()
    {
        //initGradient();
        CreateMap();
    }

    private void Update()
    {
        if (waveOnUpdate)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    ProcHexa obj = poleObjektu[x, y];
                    obj.transform.localScale = new Vector3(1f,maxHeight/2 * (Mathf.Sin( 0.2f * y + x * Time.time * 0.2f) ), 1f);
                    changeColorOfObject(obj);
                }
            }
        }
    }
    void CreateMap()
    {
        poleObjektu = new ProcHexa[width,height];

        float innerRadius = ProcHexa.innerRadius;
        float outerRadius = ProcHexa.outerRadius;

        float perlinOffX = Random.Range(0, 100);
        float perlinOffY = Random.Range(0, 100);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                ProcHexa obj = Instantiate(hexagon);
                poleObjektu[x,y] = obj;
                


                obj.transform.parent = gameObject.transform;

                if (y % 2 == 0)
                {
                    obj.transform.position = new Vector3(x * 2 * innerRadius, height/2, y * 1.5f * outerRadius);
                }
                else
                {
                    obj.transform.position = new Vector3(x * 2 * innerRadius + (innerRadius), height/2, y * 1.5f * outerRadius);
                }


                float xRec = (float)x / width * perlinScale;
                float yRec = (float)y / height * perlinScale;

                float perlin = Mathf.PerlinNoise(perlinOffX + xRec, perlinOffY + yRec);

                perlin = Mathf.Clamp(perlin, 0.000000000001f, 1);
                if (!createFlatMap)
                {
                    obj.transform.localScale = new Vector3(1f, maxHeight * perlin, 1f);
                }
                obj.name = "Pole [" + x + "," + obj.transform.localScale[1] + "," + y + "]";



                changeColorOfObject(obj);
                //float colorNumber = obj.transform.localScale[1];

                //colorNumber = remapValue(colorNumber, 0, maxHeight, 0, 1);

                //obj.setColor(grad.Evaluate(colorNumber));
            }
        }
    }

    private float remapValue(float value, float inFrom, float inTo, float outFrom, float outTo)
    {
        return (value - inFrom) / (inTo - inFrom) * (outTo - outFrom) + outFrom;
    }

    private void changeColorOfObject(ProcHexa hex)
    {
        float colorNumber = hex.transform.localScale[1];

        colorNumber = remapValue(colorNumber, 0, maxHeight, 0, 1);

        hex.setColor(grad.Evaluate(colorNumber));
    }

    

    // private void initGradient()
    // {
    //     Gradient gradientLocal = new Gradient();
    //     GradientColorKey[] gck = new GradientColorKey[8];
    //     gck[0].color = new Color(27, 0, 255);
    //     gck[0].time = 0.1f;
    //     gck[1].color = new Color(47, 224, 255);
    //     gck[1].time = 0.2f;
    //     gck[2].color = new Color(245, 255, 160);
    //     gck[2].time = 0.3f;
    //     gck[3].color = new Color(118, 253, 73);
    //     gck[3].time = 0.4f;
    //     gck[4].color = new Color(62, 245, 17);
    //     gck[4].time = 0.5f;
    //     gck[5].color = new Color(137, 137, 137);
    //     gck[5].time = 0.7f;
    //     gck[6].color = new Color(87, 87, 87);
    //     gck[6].time = 0.8f;
    //     gck[7].color = new Color(255, 255, 255);
    //     gck[7].time = 0.9f;

    //     GradientAlphaKey[] gak = new GradientAlphaKey[1];
    //     gak[0].time = 1;
    //     gak[0].alpha = 1;
    //     gradientLocal.SetKeys(gck, gak);
    //     grad = gradientLocal;
    // }
}
