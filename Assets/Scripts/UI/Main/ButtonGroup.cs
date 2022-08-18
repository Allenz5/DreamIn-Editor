using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGroup : MonoBehaviour
{
    public PointerEvents[] btnList;

    public Image curButton;
    public Color selectedColor;
    public Color highlightedColor;
    public Color normalColor;

    public Button DefaultBtn;
    // Start is called before the first frame update
    void Awake()
    {
        btnList = GetComponentsInChildren<PointerEvents>();
        foreach(PointerEvents btn in btnList){
            btn.onPointerEnter.AddListener(Highlight);
            btn.onPointerExit.AddListener(Exit);
            Exit(btn.gameObject);
            btn.onPointerClick.AddListener(Select);
        }
        Init();
    }

    public void Init(){
        Select(DefaultBtn.gameObject);
        DefaultBtn.onClick.Invoke();
    }

    public void Select(GameObject btn){
        if(curButton != null && curButton.gameObject == btn) return;
        if(curButton != null){
            curButton.color = normalColor;
            PointerEvents curBtnPointerEvents = curButton.GetComponent<PointerEvents>();
            curBtnPointerEvents.onPointerExit.AddListener(Exit);
            curBtnPointerEvents.onPointerEnter.AddListener(Highlight);
        }
        Image btnImage = btn.GetComponent<Image>();
        btnImage.color = selectedColor;
        PointerEvents btnPointerEvents = btn.GetComponent<PointerEvents>();
        btnPointerEvents.onPointerExit.RemoveListener(Exit);
        btnPointerEvents.onPointerEnter.RemoveListener(Highlight);
        curButton = btnImage;
    }

    public void Highlight(GameObject btn){
        Image btnImage = btn.GetComponent<Image>();
        btnImage.color = highlightedColor;
    }

    public void Exit(GameObject btn){
        Image btnImage = btn.GetComponent<Image>();
        btnImage.color = normalColor;
    }

    public void SelectNone(){
        if(curButton){
            Exit(curButton.gameObject);
            curButton = null;
        }
    }




}
