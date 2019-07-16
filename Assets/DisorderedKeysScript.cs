using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DisorderedKeysScript : MonoBehaviour
{

    public KMAudio Audio;
    public KMBombInfo bomb;
    public KMColorblindMode ColorblindMode;

    public List<KMSelectable> keys;
    public Renderer meter;
    public Renderer[] keyID;
    public Material[] keyColours;

    private static int[][][] table =
        new int[4][][] { new int[6][] { new int[16] { 2, 0, 3, 5, 4, 1, 0, 2, 3, 5, 1, 4, 2, 0, 3, 5},
                                        new int[16] { 4, 5, 1, 0, 2, 3, 4, 5, 0, 1, 3, 2, 5, 4, 0, 1},
                                        new int[16] { 5, 3, 4, 2, 1, 0, 5, 3, 2, 4, 0, 1, 3, 5, 2, 4},
                                        new int[16] { 0, 1, 2, 4, 3, 5, 1, 0, 4, 2, 5, 3, 1, 0, 4, 2},
                                        new int[16] { 1, 2, 5, 3, 0, 4, 2, 1, 5, 3, 4, 0, 2, 1, 5, 3},
                                        new int[16] { 3, 4, 0, 1, 5, 2, 3, 4, 1, 0, 2, 5, 3, 4, 1, 0} },

                        new int[6][]  { new int[16] { 1, 4, 5, 0, 3, 2, 4, 1, 0, 5, 2, 3, 1, 4, 0, 5},
                                        new int[16] { 3, 0, 1, 5, 2, 4, 0, 3, 5, 1, 4, 2, 0, 3, 5, 1},
                                        new int[16] { 2, 1, 0, 4, 5, 3, 1, 2, 4, 0, 3, 5, 2, 1, 4, 0},
                                        new int[16] { 5, 2, 4, 3, 0, 1, 2, 5, 3, 4, 0, 1, 5, 2, 3, 4},
                                        new int[16] { 4, 5, 3, 2, 1, 0, 5, 4, 2, 3, 1, 0, 4, 5, 2, 3},
                                        new int[16] { 0, 3, 2, 1, 4, 5, 3, 0, 1, 2, 5, 4, 3, 0, 1, 2} },

                        new int[6][]  { new int[16] { 5, 1, 2, 0, 3, 4, 1, 5, 0, 2, 4, 3, 5, 1, 0, 2},
                                        new int[16] { 1, 3, 0, 4, 2, 5, 3, 1, 4, 0, 5, 2, 1, 3, 4, 0},
                                        new int[16] { 2, 0, 5, 3, 4, 1, 2, 0, 3, 5, 1, 4, 0, 2, 3, 5},
                                        new int[16] { 4, 5, 3, 1, 0, 2, 5, 4, 1, 3, 2, 0, 4, 5, 1, 3},
                                        new int[16] { 0, 2, 4, 5, 1, 3, 0, 2, 5, 4, 3, 1, 2, 0, 5, 4},
                                        new int[16] { 3, 4, 1, 2, 5, 0, 4, 3, 2, 1, 0, 5, 3, 4, 2, 1} },

                        new int[16][] { new int[16] { 3, 4, 1, 2, 6, 5, 4, 3, 2, 1, 5, 6, 3, 4, 2, 1},
                                        new int[16] { 4, 1, 6, 3, 5, 2, 1, 4, 3, 6, 2, 5, 4, 1, 3, 6},
                                        new int[16] { 2, 5, 3, 4, 1, 6, 2, 5, 4, 3, 6, 1, 5, 2, 4, 3},
                                        new int[16] { 6, 2, 5, 1, 4, 3, 6, 2, 1, 5, 3, 4, 2, 6, 1, 5},
                                        new int[16] { 1, 6, 2, 5, 3, 4, 1, 6, 5, 2, 4, 3, 6, 1, 5, 2},
                                        new int[16] { 5, 3, 4, 6, 2, 1, 3, 5, 6, 4, 1, 2, 5, 3, 6, 4},
                                        new int[16] { 2, 1, 6, 4, 5, 3, 2, 1, 4, 6, 3, 5, 1, 2, 4, 6},
                                        new int[16] { 4, 3, 1, 5, 6, 2, 3, 4, 1, 5, 2, 6, 3, 4, 1, 5},
                                        new int[16] { 1, 6, 4, 2, 3, 5, 1, 6, 2, 4, 5, 3, 6, 1, 2, 4},
                                        new int[16] { 5, 4, 2, 3, 1, 6, 4, 5, 3, 2, 6, 1, 4, 5, 3, 2},
                                        new int[16] { 3, 2, 5, 6, 4, 1, 2, 3, 5, 6, 1, 4, 2, 3, 5, 6},
                                        new int[16] { 6, 5, 3, 1, 2, 4, 5, 6, 1, 3, 4, 2, 5, 6, 1, 3},
                                        new int[16] { 4, 1, 6, 5, 3, 2, 4, 1, 6, 5, 2, 3, 1, 4, 6, 5},
                                        new int[16] { 1, 3, 4, 2, 5, 6, 1, 3, 2, 4, 5, 6, 3, 1, 2, 4},
                                        new int[16] { 5, 6, 2, 4, 1, 3, 6, 5, 4, 2, 3, 1, 6, 5, 4, 2},
                                        new int[16] { 6, 4, 1, 3, 2, 5, 4, 6, 3, 1, 2, 5, 4, 6, 3, 1} } };

    private static string[] colourList = new string[6] { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow" };
    private static string[] quirkList = new string[5] { "None", "Time", "First", "Last", "False"};
    private int[][] info = new int[6][] { new int[3], new int[3], new int[3], new int[3], new int[3], new int[3] };
    private int[][] seqinfo = new int[5][] { new int[3], new int[3], new int[3], new int[3], new int[3] };
    private int[] missing = new int[6];
    private int[] quirk = new int[6];
    private List<int> valueList = new List<int> { };
    private int pressCount;
    private int resetCount;
    private IEnumerator[] sequence = new IEnumerator[2];
    private bool[] alreadypressed = new bool[6] { true, true, true, true, true, true };
    private bool[] revealed = new bool[6];
    private bool pressable;
    private bool colorblind;

    //Logging
    static int moduleCounter = 1;
    int moduleID;
    private bool moduleSolved;

    private void Awake()
    {
        moduleID = moduleCounter++;
        sequence[0] = Shuff();
        meter.material = keyColours[7];
        foreach (KMSelectable key in keys)
        {
            key.transform.localPosition = new Vector3(0, 0, -1f);
            key.OnInteract += delegate () { KeyPress(key); return false; };
        }
    }

    void Start()
    {
        colorblind = ColorblindMode.ColorblindModeActive;
        Reset();
    }

    private void KeyPress(KMSelectable key)
    {
        int k = keys.IndexOf(key);
        if (alreadypressed[k] == false && moduleSolved == false && pressable == true)
        {
            key.AddInteractionPunch();
            if (quirk[k] != 4 && revealed[k] == false)
            {
                revealed[k] = true;
                GetComponent<KMAudio>().PlaySoundAtTransform("Reveal", transform);
                StartCoroutine(Reveal(k));
            }
            else if (quirk[k] != 4 && revealed[k] == true)
            {
                alreadypressed[k] = true;
                bool strike = false;
                int t = 0;
                if (quirk[k] == 1)
                {
                    t = Mathf.Abs((((int)bomb.GetTime() % 60 - ((int)bomb.GetTime() % 60) % 10) / 10) - ((int)bomb.GetTime() % 10));
                }
                for (int i = 0; i < 7; i++)
                {
                    if (i != 6)
                    {
                        if (i != k)
                        {
                            switch (quirk[k])
                            {
                                case 2:
                                    if (valueList[i] < valueList[k] && alreadypressed[i] == false && quirk[i] == 2)
                                    {
                                        Debug.LogFormat("[Disordered Keys #{0}] Error: Key {1} pushed out of order- Key {2} should have been pressed before this", moduleID, k + 1, i + 1);
                                        strike = true;
                                    }
                                    break;
                                case 0:
                                case 5:
                                    if ((quirk[i] == 2 && alreadypressed[i] == false) || (valueList[i] < valueList[k] && alreadypressed[i] == false && (quirk[i] == 0 || quirk[i] == 5)))
                                    {
                                        Debug.LogFormat("[Disordered Keys #{0}] Error: Key {1} pushed out of order- Key {2} should have been pressed before this", moduleID, k + 1, i + 1);
                                        strike = true;
                                    }
                                    break;
                                case 1:      
                                    if(quirk[i] == 2 && alreadypressed[i] == false)
                                    {
                                        Debug.LogFormat("[Disordered Keys #{0}] Error: Key {1} pushed out of order- Key {2} should have been pressed before this", moduleID, k + 1, i + 1);
                                        strike = true;
                                    }
                                    else if (valueList[k] % 6 != t)
                                    {
                                        Debug.LogFormat("[Disordered Keys #{0}] Error: Key {1} pushed at incorrect time", moduleID, k + 1);
                                        strike = true;
                                    }
                                    break;
                                case 3:
                                    if((alreadypressed[i] == false && quirk[i] != 3 && quirk[i] != 4) || (valueList[i] < valueList[k] && alreadypressed[i] == false && quirk[i] == 3))
                                    {
                                        Debug.LogFormat("[Disordered Keys #{0}] Error: Key {1} pushed out of order- Key {2} should have been pressed before this", moduleID, k + 1, i + 1);
                                        strike = true;
                                    }
                                    break;
                            }
                            if(strike == true)
                            {
                                GetComponent<KMBombModule>().HandleStrike();
                                GetComponent<KMAudio>().PlaySoundAtTransform("Error", transform);
                                resetCount++;
                                Reset();
                                break;
                            }
                        }
                    }
                    else
                    {
                        pressCount++;
                        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
                        key.transform.localPosition = new Vector3(0, 0, -1f);
                        if(quirk[k] == 5)
                        {
                            StopAllCoroutines();
                        }
                        if(pressCount == 6)
                        {
                            moduleSolved = true;
                            GetComponent<KMAudio>().PlaySoundAtTransform("InputCorrect", transform);
                            meter.material = keyColours[6];
                            Reset();
                        }
                    }
                }
            }
            else
            {
                for(int i = 0; i < 7; i++)
                {
                    if(i != 6)
                    {
                        if(i != k && alreadypressed[i] == false && quirk[i] != 4)
                        {
                            GetComponent<KMBombModule>().HandleStrike();
                            GetComponent<KMAudio>().PlaySoundAtTransform("Error", transform);
                            Debug.LogFormat("[Disordered Keys #{0}] Error: False key pressed", moduleID);
                            resetCount++;
                            Reset();
                            break;
                        }
                    }
                    else
                    {
                        pressCount++;
                        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
                        key.transform.localPosition = new Vector3(0, 0, -1f);
                        if (pressCount == 6)
                        {
                            moduleSolved = true;
                            GetComponent<KMAudio>().PlaySoundAtTransform("InputCorrect", transform);
                            meter.material = keyColours[6];
                            Reset();
                        }
                    }
                }
            }
        }
    }

    private void setKey(int keyIndex, int? color = null, int? labelColor = null, int? label = null)
    {
        string labelStr = string.Empty;
        if (missing[keyIndex] != 0)
        {
            keyID[keyIndex].material = keyColours[color ?? info[keyIndex][0]];
            if (colorblind == true)
            {
                labelStr = "\n" + "RGBCMY"[color ?? info[keyIndex][0]];
            }
        }
        else
        {
            keyID[keyIndex].material = keyColours[7];
            if (colorblind == true)
            {
                labelStr = "\n ?";
            }
        }
        if (missing[keyIndex] != 1)
        {
            switch (labelColor ?? info[keyIndex][1])
            {
                case 0:
                    keys[keyIndex].GetComponentInChildren<TextMesh>().color = new Color32(255, 25, 25, 255);
                    break;
                case 1:
                    keys[keyIndex].GetComponentInChildren<TextMesh>().color = new Color32(25, 255, 25, 255);
                    break;
                case 2:
                    keys[keyIndex].GetComponentInChildren<TextMesh>().color = new Color32(25, 25, 255, 255);
                    break;
                case 3:
                    keys[keyIndex].GetComponentInChildren<TextMesh>().color = new Color32(25, 255, 255, 255);
                    break;
                case 4:
                    keys[keyIndex].GetComponentInChildren<TextMesh>().color = new Color32(255, 75, 255, 255);
                    break;
                default:
                    keys[keyIndex].GetComponentInChildren<TextMesh>().color = new Color32(255, 255, 75, 255);
                    break;
            }
            if (colorblind == true)
            {
                labelStr = "\n" + "RGBCMY"[labelColor ?? info[keyIndex][1]] + "\n" + labelStr;
            }
        }
        else
        {
            keys[keyIndex].GetComponentInChildren<TextMesh>().color = new Color32(0, 0, 0, 255);
            if (colorblind == true)
            {
                labelStr = "\n ? \n" + labelStr;
            }
        }
        if(missing[keyIndex] != 2)
        {
            labelStr = ((label ?? info[keyIndex][2]) + 1).ToString() + labelStr;
        }
        else
        {
            labelStr = "#" + labelStr;
        }
        keys[keyIndex].GetComponentInChildren<TextMesh>().text = labelStr;
    }

    private IEnumerator Reveal(int k)
    {
        missing[k] = -1;
        int verifier = 0;
        for (int j = 0; j < 20; j++)
        {
            if (j < 19)
            {
                setKey(k, Random.Range(0, 6), Random.Range(0, 6), Random.Range(0, 6));
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                if (quirk[k] != 5)
                {
                    while (true)
                    {
                        info[k][0] = Random.Range(0, 6);
                        info[k][1] = Random.Range(0, 6);
                        info[k][2] = Random.Range(0, 6);
                        verifier = table[3][info[k][1] * 2 + k][info[k][0] * 2 + info[k][2]];
                        if (verifier == valueList[k])
                        {
                            break;
                        }
                    }
                    Debug.LogFormat("[Disordered Keys #{0}] Key {1} revealed: {2} with {3} {4}", moduleID, k + 1, colourList[info[k][0]], colourList[info[k][1]], info[k][2] + 1);
                    setKey(k);
                }
                else
                {
                    Debug.LogFormat("[Disordered Keys #{0}] Key {1} revealed:", moduleID, k + 1);
                    List<int> nums = new List<int> { 1, 2, 3, 4, 5, 6 };
                    List<int> seqvals = new List<int> { };
                    for (int i = 0; i < 6; i++)
                    {
                        int temp = Random.Range(0, nums.Count());
                        seqvals.Add(nums[temp]);
                        nums.RemoveAt(temp);
                    }
                    seqvals.Remove(valueList[k]);
                    for (int i = 0; i < 5; i++)
                    {
                        while (true)
                        {
                            seqinfo[i][0] = Random.Range(0, 6);
                            seqinfo[i][1] = Random.Range(0, 6);
                            seqinfo[i][2] = Random.Range(0, 6);
                            verifier = table[3][seqinfo[i][1] * 2 + k][seqinfo[i][0] * 2 + seqinfo[i][2]];
                            if (verifier == seqvals[i])
                            {
                                Debug.LogFormat("[Disordered Keys #{0}] - {1} with {2} {3}", moduleID, colourList[seqinfo[i][0]], colourList[seqinfo[i][1]], seqinfo[i][2] + 1);
                                break;
                            }
                        }
                    }
                    StartCoroutine(Seq(k));
                }
            }
        }
    }

    private void Reset()
    {
        if (moduleSolved == false)
        {
            pressable = false;
            pressCount = 0;
            StopAllCoroutines();
            for (int i = 0; i < 6; i++)
            {
                revealed[i] = false;
                alreadypressed[i] = true;
            }
            int seq = Random.Range(0, 6);
            quirk[seq] = 5;
            List<int> initialList = new List<int> { 1, 2, 3, 4, 5, 6 };
            valueList.Clear();
            string[] qui = new string[6];
            qui[seq] = "Sequence";
            for (int i = 0; i < 6; i++)
            {
                revealed[i] = false;
                int temp = Random.Range(0, initialList.Count());
                valueList.Add(initialList[temp]);
                initialList.RemoveAt(temp);
                if(i != seq)
                {
                    quirk[i] = Random.Range(0, 5);
                    qui[i] = quirkList[quirk[i]];
                }
            }
            string[] a = new string[6];
            string[] b = new string[6];
            string[] c = new string[6];
            int verifier = 0;
            for (int i = 0; i < 6; i++)
            {
                missing[i] = Random.Range(0, 3);
                while (true)
                {
                    info[i][0] = Random.Range(0, 6);
                    info[i][1] = Random.Range(0, 6);
                    info[i][2] = Random.Range(0, 6);
                    switch (missing[i])
                    {
                        case 0:
                            verifier = table[0][i][info[i][1] * 2 + info[i][2]];
                            break;
                        case 1:
                            verifier = table[1][i][info[i][0] * 2 + info[i][2]];
                            break;
                        case 2:
                            verifier = table[2][info[i][1]][info[i][0] * 2 + i];
                            break;
                    }
                    if (verifier == quirk[i])
                    {
                        break;
                    }
                }
                switch (missing[i])
                {
                    case 0:
                        a[i] = "#";
                        b[i] = colourList[info[i][1]];
                        c[i] = (info[i][2] + 1).ToString();
                        break;
                    case 1:
                        a[i] = colourList[info[i][0]];
                        b[i] = "#";
                        c[i] = (info[i][2] + 1).ToString();
                        break;
                    case 2:
                        a[i] = colourList[info[i][0]];
                        b[i] = colourList[info[i][1]];
                        c[i] = "#";
                        break;
                }
            }
            string[] valstring = new string[6];
            for(int i = 0; i < 6; i++)
            {
                if(quirk[i] == 4)
                {
                    valstring[i] = "#";
                }
                else
                {
                    valstring[i] = valueList[i].ToString();
                }
            }
            Debug.LogFormat("[Disordered Keys #{0}] After {1} reset(s), the keys had the colours: {2}", moduleID, resetCount, string.Join(", ", a));
            Debug.LogFormat("[Disordered Keys #{0}] After {1} reset(s), the labels had the colours: {2}", moduleID, resetCount, string.Join(", ", b));
            Debug.LogFormat("[Disordered Keys #{0}] After {1} reset(s), the keys were labelled: {2}", moduleID, resetCount, string.Join(", ", c));
            Debug.LogFormat("[Disordered Keys #{0}] After {1} reset(s), the keys had the quirks: {2}", moduleID, resetCount, string.Join(", ", qui));
            Debug.LogFormat("[Disordered Keys #{0}] After {1} reset(s), the keys had the values: {2}", moduleID, resetCount, string.Join(", ", valstring));
        }
        StartCoroutine(sequence[0]);
    }

    private IEnumerator Shuff()
    {
        for (int i = 0; i < 30; i++)
        {
            if (i % 5 == 4)
            {
                if (moduleSolved == true)
                {
                    alreadypressed[(i - 4) / 5] = false;
                    keyID[(i - 4) / 5].material = keyColours[8];
                    keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().color = new Color32(0, 0, 0, 255);
                    keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().text = "0";
                    if (i == 29)
                    {
                        GetComponent<KMBombModule>().HandlePass();
                        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
                    }
                }
                else
                {
                    keys[(i - 4) / 5].transform.localPosition = new Vector3(0, 0, 0);
                    GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
                    alreadypressed[(i - 4) / 5] = false;
                    setKey((i - 4) / 5);
                }
                if (i == 29)
                {
                    i = -1;
                    pressable = true;
                    StopCoroutine(sequence[0]);
                }
            }
            else
            {
                for (int j = 0; j < 6; j++)
                    if (alreadypressed[j] == true)
                        setKey(j, Random.Range(0, 6), Random.Range(0, 6), Random.Range(0, 6));
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator Seq(int k)
    {
        for(int i = 0; i < 5; i++)
        {
            setKey(k, seqinfo[i][0], seqinfo[i][1], seqinfo[i][2]);
            if (i == 4)
            {
                i = -1;
            }
            yield return new WaitForSeconds(1.2f);
        }
    } 
    public string TwitchHelpMessage = "Use '!{0} press 1 2 3 4' to press key 1, 2 3 & 4! BUT, for time keys use '!{0} press x at y'! For ex. '{0} press 1 at 22' will press the 1st key when the senonds digits are 22. Don't chain not time and time keys!";
    IEnumerator ProcessTwitchCommand(string command)
    {
        string commfinal=command.Replace("press ", "");
        if(commfinal.Contains("at")){
            string commfinal2=commfinal.Replace("at ", "");
            string[] digitstring = commfinal2.Split(' ');
            int tried;
            if(int.TryParse(digitstring[0], out tried) && int.TryParse(digitstring[1], out tried)){
                int buttonindex=int.Parse(digitstring[0]);
                int timer = int.Parse(digitstring[1]);
                yield return null;
                while (Mathf.FloorToInt(bomb.GetTime()) % 60 != timer) yield return "trycancel Button wasn't pressed due to request to cancel.";
                yield return null;
                yield return keys[buttonindex-1];
                yield return keys[buttonindex-1];
            }
            else{           //invalid digit
                yield return null;
                yield return "sendtochaterror Digit not valid.";
                yield break;
            }
        }
        else{
            string[] digitstring = commfinal.Split(' ');
            int tried;
            int index = 1;
            foreach(string digit in digitstring){
                if(int.TryParse(digit, out tried)){
                    tried=int.Parse(digit);
                    if(tried<=6){
                        if(tried>=1){
                            if(index<7){  
                            yield return null;
                            yield return keys[tried-1];
                            yield return keys[tried-1];
                            }
                            else{       //no more buttons to press
                                yield return null;
                                yield return "sendtochaterror Too many digits!";
                                yield break;
                            }
                        }
                        else{       //small
                            yield return null;
                            yield return "sendtochaterror Digit too small!";
                            yield break;
                        }
                    }
                    else{       //big
                        yield return null;
                        yield return "sendtochaterror Digit too big!";
                        yield break;
                    }
                }
                else{       //invalid digit
                yield return null;
                yield return "sendtochaterror Digit not valid.";
                yield break;
            }
            }
        }
    }
}
