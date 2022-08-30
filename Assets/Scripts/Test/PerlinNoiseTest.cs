using UnityEngine;
using KMath.Noise;
using UnityEngine.UI;
using TMPro;
using TreeEditor;


public class PerlinNoiseTest : MonoBehaviour
{
    // Unlit
    [SerializeField]
    private Material Unlit;

    float lastDrawTime = 0;

    private Perlin1D perlin1D;
    private PerlinField2D perlin2D;
    private PerlinField3D perlin3D;

    private Texture2D MathFGeneratedPerlin;
    private Texture2D KMathGeneratedPerlin;

    TMP_InputField Seed;
    TMP_InputField SamplingRate;
    TMP_InputField Octaves;
    TMP_InputField Lacunarity;
    TMP_InputField Gain;
    TMP_InputField Offset;
    TMP_InputField PositionZ;
    Toggle UnityToggle;
    TMP_Dropdown Dimension;

    bool Init;
    bool MovingRight = false;
    bool MovingUp = false;

    RawImage NoiseImage;

    float[] Samples;

    // Doc: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
    private void Start()
    {
        InitializeCanvas();

        perlin1D = new Perlin1D();
        perlin2D = new PerlinField2D();
        perlin3D = new PerlinField3D();

        MathFGeneratedPerlin = new Texture2D(1024, 1024);
        KMathGeneratedPerlin = new Texture2D(1024, 1024);

        Samples = new float[KMathGeneratedPerlin.height * KMathGeneratedPerlin.width];
        MathfGenerateTexture();

        GameState.Renderer.Initialize(Unlit);

        GeneratePerlin();

        Init = true;
    }

    private void Update()
    {
        if (MovingUp || MovingRight)
        {
            if ((Time.realtimeSinceStartup - lastDrawTime) >= 1)
            {
                if (MovingRight)
                {
                    float offset = float.Parse(Offset.text);
                    offset += 2.5f;
                    Offset.text = offset.ToString();
                }
                if (MovingUp)
                {
                    float position = float.Parse(PositionZ.text);
                    position += 2.5f;
                    PositionZ.text = position.ToString(); ;
                }

                GeneratePerlin();
            }

        }
    }

    public void InitializeCanvas()
    {
        NoiseImage = GameObject.Find("Noise Texture").GetComponent<RawImage>();

        Seed            = GameObject.Find("Seed").GetComponent<TMP_InputField>();
        SamplingRate    = GameObject.Find("Sampling Rate").GetComponent<TMP_InputField>();
        Octaves         = GameObject.Find("Octaves").GetComponent<TMP_InputField>();
        Lacunarity      = GameObject.Find("Lacunarity").GetComponent<TMP_InputField>();
        Gain            = GameObject.Find("Gain").GetComponent<TMP_InputField>();
        Offset          = GameObject.Find("Offset").GetComponent<TMP_InputField>();
        PositionZ       = GameObject.Find("Position Z").GetComponent<TMP_InputField>();

        UnityToggle = GameObject.Find("Unity Toggle").GetComponent<Toggle>();
        Dimension = GameObject.Find("Dropdown").GetComponent<TMP_Dropdown>();
        Dimension.value = 2;
        GameObject.Find("Generate Button").GetComponent<Button>().onClick.AddListener(delegate { GeneratePerlin(); });
        GameObject.Find("Move Up").GetComponent<Button>().onClick.AddListener(delegate { MovingUp = !MovingUp; });
        GameObject.Find("Move Right").GetComponent<Button>().onClick.AddListener(delegate { MovingRight = !MovingRight; });

        UnityToggle.isOn = false;

        Seed.text = "1400";
        SamplingRate.text = "48";
        Octaves.text = "3";
        Lacunarity.text = "2,0";
        Gain.text = "0,5";
        Offset.text = "0,0";
        PositionZ.text = "0,0";
    }

    void GeneratePerlin()
    {
        float lastDrawTime = Time.realtimeSinceStartup;

        if (Dimension.value == 0)
        {
            perlin1D.Seed = int.Parse(Seed.text);
            perlin1D.SamplingRate = int.Parse(SamplingRate.text);
            perlin1D.Lacunarity = float.Parse(Lacunarity.text);
            perlin1D.Offset = float.Parse(Offset.text);
            perlin1D.SetOctaves(int.Parse(Octaves.text));
            perlin1D.SetGain(float.Parse(Gain.text));
        }
        else if (Dimension.value == 1)
        {
            perlin2D.Seed = int.Parse(Seed.text);
            perlin2D.SamplingRate = int.Parse(SamplingRate.text);
            perlin2D.Lacunarity = float.Parse(Lacunarity.text);
            perlin2D.Offset = float.Parse(Offset.text);
            perlin2D.SetOctaves(int.Parse(Octaves.text));
            perlin2D.SetGain(float.Parse(Gain.text));
        }
        else if (Dimension.value == 2)
        {
            perlin3D.Seed = int.Parse(Seed.text);
            perlin3D.SamplingRate = int.Parse(SamplingRate.text);
            perlin3D.Lacunarity = float.Parse(Lacunarity.text);
            perlin2D.Offset = float.Parse(Offset.text);
            perlin3D.SetOctaves(int.Parse(Octaves.text));
            perlin3D.SetGain(float.Parse(Gain.text));
        }

        if (!UnityToggle.isOn)
        {
            KMathGenerateTexture();
            NoiseImage.texture = KMathGeneratedPerlin;
        }
        else
        {
            NoiseImage.texture = MathFGeneratedPerlin;
        }
    }

    void KMathGenerateTexture()
    {
        float deltaTime = Time.realtimeSinceStartup;

        if (Dimension.value == 0)
            KMathGenerateTexture1D();
        else if (Dimension.value == 1)
            KMathGenerateTexture2D();
        else if (Dimension.value == 2)
            KMathGenerateTexture3D();

        deltaTime = (Time.realtimeSinceStartup - deltaTime) * 1000; // get time and transform to ms.
        Debug.Log("Texture Generate " + deltaTime.ToString() + "ms");
    }

    void KMathGenerateTexture1D()
    {
        // Clear texture
        for (int y = 0; y < KMathGeneratedPerlin.height; y++)
        {
            for (int x = 0; x < KMathGeneratedPerlin.width; x++)
            {
                KMathGeneratedPerlin.SetPixel(x, y, Color.white);
            }
        }

        for (int x = 0; x < KMathGeneratedPerlin.width; x++)
        {
            Samples[x] = perlin1D.GetNoise(x);
        }

        for (int x = 0; x < KMathGeneratedPerlin.width; x++)
        {
            int y = KMathGeneratedPerlin.height/4;
            y += (int)(Samples[x] * KMathGeneratedPerlin.height / 2f);

            for (int i = 0; i < 10; i++)
            {
                KMathGeneratedPerlin.SetPixel(x, y - 4 + i, Color.grey);
            }
        }

        KMathGeneratedPerlin.Apply();
    }

    void KMathGenerateTexture2D()
    {
        float max = float.MinValue;
        float min = float.MaxValue;

        for (int y = 0, index = 0; y < KMathGeneratedPerlin.height; y++)
        {
            for (int x = 0; x < KMathGeneratedPerlin.width; x++)
            {
                Samples[index] = perlin2D.GetNoise(x, y);
                if (Samples[index] > max)
                {
                    max = Samples[index];
                }

                if (Samples[index] < min)
                {
                    min = Samples[index];
                }
                index++;
            }
        }

        for (int y = 0, index = 0; y < KMathGeneratedPerlin.height; y++)
        {
            for (int x = 0; x < KMathGeneratedPerlin.width; x++)
            {
                float value = (Samples[index++] + 1) / 2;
                Color color = new Color(value, value, value);
                KMathGeneratedPerlin.SetPixel(x, y, color);
            }
        }

        KMathGeneratedPerlin.Apply();
    }

    void KMathGenerateTexture3D()
    {
        float zPos = float.Parse(PositionZ.text);

        for (int y = 0, index = 0; y < KMathGeneratedPerlin.height; y++)
        {
            for (int x = 0; x < KMathGeneratedPerlin.width; x++)
            {
                Samples[index] = perlin3D.GetNoise(x, y, zPos);
                index++;
            }
        }

        for (int y = 0, index = 0; y < KMathGeneratedPerlin.height; y++)
        {
            for (int x = 0; x < KMathGeneratedPerlin.width; x++)
            {
                float value = (Samples[index++] + 1) / 2;
                Color color = new Color(value, value, value);
                KMathGeneratedPerlin.SetPixel(x, y, color);
            }
        }

        KMathGeneratedPerlin.Apply();
    }


    void MathfGenerateTexture()
    {
        for (int x = 0; x < MathFGeneratedPerlin.width; x++)
        {
            for (int y = 0; y < MathFGeneratedPerlin.height; y++)
            {
                Color color = MathfCalculateColor(x, y);
                MathFGeneratedPerlin.SetPixel(x, y, color);
            }
        }
        MathFGeneratedPerlin.Apply();
    }

    Color MathfCalculateColor(int x, int y)
    {
        const int scale = 20;
        float xCoord = (float)x / MathFGeneratedPerlin.width * scale;
        float yCoord = (float)y / MathFGeneratedPerlin.height * scale;

        float sample = Mathf .PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }
}
