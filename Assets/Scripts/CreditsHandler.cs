using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsHandler : MonoBehaviour
{
    [SerializeField] private string _path = "Assets/Scripts/Credits.txt";
    [SerializeField] private Font _font;
    [SerializeField] private Color _headerColor = Color.yellow;
    [SerializeField] private Color _nameColor = Color.white;
    [SerializeField] private int _headerSize = 35;
    [SerializeField] private int _nameSize = 25;
    [SerializeField] private float _scrollSpeed = 10f;
    [SerializeField] private int _screenSpaceInDIvions = 8;

    private int _destroyedTexts = 0;

    private List<string> _headers = new List<string>();
    List<List<string>> _titles = new List<List<string>>();
    List<GameObject> _creditsTexts = new List<GameObject>();

    private void Awake()
    {
        StreamReader reader = new StreamReader(_path);
        string line = "";
        bool newStart = false;

        while ((line = reader.ReadLine()) != null)
        {
            string firstCharacter = line.Substring(0, 1);
            bool isIgnore = firstCharacter.Equals("#");
            bool isHeader = firstCharacter.Equals("!");
            if (isHeader)
            {
                newStart = true;
                _headers.Add(line.Substring(1));
            }
            else if (isIgnore)
            {
                //do nothing
            }
            else
            {
                if (newStart)
                {
                    _titles.Add(new List<string>());
                    newStart = false;
                }
                _titles[_titles.Count - 1].Add(line);
            }
        }

        reader.Close();

        if (_font == null)
            _font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
    }

    private void Start()
    {
        Vector3 lastPos = new Vector3(Screen.width * 0.5f, 0f, 0f);

        for (int i = 0; i < _headers.Count; i++)
        {
            GameObject newObj = NewText(_headers[i], true);
            Vector3 nextPos = new Vector3(Screen.width * 0.5f, lastPos.y - (Screen.height / _screenSpaceInDIvions), 0f);
            newObj.transform.position = nextPos;
            lastPos = nextPos;
            _creditsTexts.Add(newObj);

            for (int j = 0; j < _titles[i].Count; j++)
            {
                nextPos = new Vector3(Screen.width * 0.5f, lastPos.y - (Screen.height/ _screenSpaceInDIvions), 0f);
                GameObject oObj = NewText(_titles[i][j], false);
                oObj.transform.position = nextPos;
                _creditsTexts.Add(oObj);
                lastPos = nextPos;
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < _creditsTexts.Count; i++)
        {
            if (_creditsTexts[i] != null)
            {
                _creditsTexts[i].transform.position = new Vector3(_creditsTexts[i].transform.position.x, _creditsTexts[i].transform.position.y + _scrollSpeed, 0);
                if (_creditsTexts[i].transform.position.y > Screen.height * 1.2)
                {
                    Destroy(_creditsTexts[i]);
                    _creditsTexts[i] = null;
                    _destroyedTexts++;

                    if (_creditsTexts.Count == _destroyedTexts)
                        BackToMainMenu();
                }
            }
        }
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public GameObject NewText(string labelText, bool isHeader)
    {
        GameObject textObj = new GameObject(labelText);
        textObj.transform.SetParent(this.transform);
        Text myText;
        myText = textObj.AddComponent<Text>();
        myText.text = labelText;
        myText.font = _font;
        myText.horizontalOverflow = HorizontalWrapMode.Overflow;
        myText.alignment = TextAnchor.MiddleCenter;

        if (isHeader)
        {
            myText.fontStyle = FontStyle.Bold;
            myText.color = _headerColor;
            myText.fontSize = _headerSize;
        }
        else
        {
            myText.color = _nameColor;
            myText.fontSize = _nameSize;
        }

        textObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        textObj.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.25f, 0);

        return textObj;
    }
}
