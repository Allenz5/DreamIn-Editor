using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditMap : MonoBehaviour
{
    #region Variables
    //UI
    public GameObject LevelsUI;
    public GameObject LevelUI;
    public GameObject MapEditorUI;
    public GameObject LevelSettingEditorUI;

    public GameObject MapLoopScroll;
    public GameObject ObjectLoopScroll;
    public GameObject CollideMap;
    public GameObject Background;
    public GameObject MessageEditor;
    public GameObject MapSelectBg;
    public GameObject ObjectSelectBg;

    public GameObject Maps;
    public TMP_Text mapFilter;
    public TMP_Text ObjectFilter;
    public GameObject TempImage;
    public GameObject Message;
    public GameObject LevelsEditor;
    public ButtonGroup Options;

    //Prefabs
    public GameObject ColliderImage;
    public GameObject ObjectPrefab;

    //Data
    public List<GameObject> Objects;
    public List<ObjectInfo> ObjectInfoList;
    public List<GameObject> Colliders;
    public bool[,] ColliderMap;
    public string BackgroundPath;

    //Constant
    private int COLLIDER_SIZE = 32;

    //Singleton
    public static EditMap Instance;

    #endregion

    #region Cycle
    void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        Objects = new List<GameObject>();
        ObjectInfoList = new List<ObjectInfo>();
        Colliders = new List<GameObject>();
        InitializeColliderMap();
    }

    private void Start()
    {
        //Objects = new List<GameObject>();
        //ObjectInfoList = new List<ObjectInfo>();
        //Colliders = new List<GameObject>();
        //InitializeColliderMap();
    }

    public void OnEnable()
    {
        Image BackgroundImage = Background.GetComponent<Image>();
        if (BackgroundImage.sprite == null)
        {
            ChangeBackground selected = Maps.GetComponentInChildren<ChangeBackground>();
            selected.BackgroundButton(selected.gameObject);
        }
    }
    #endregion

    #region Buttons
    /// <summary>
    /// Switch to map scroll view
    /// </summary>
    public void MapButton()
    {
        MapLoopScroll.SetActive(true);
        CollideMap.SetActive(false);
        ObjectLoopScroll.SetActive(false);
        MessageEditor.SetActive(false);
        MapSelectBg.SetActive(true);
        ObjectSelectBg.SetActive(false);
    }

    /// <summary>
    /// Switch to object scroll view
    /// </summary>
    public void ObjectButton()
    {
        MapLoopScroll.SetActive(false);
        CollideMap.SetActive(false);
        ObjectLoopScroll.SetActive(true);
        MessageEditor.SetActive(true);
        MapSelectBg.SetActive(false);
        ObjectSelectBg.SetActive(true);
    }

    /// <summary>
    /// Switch to collider view
    /// </summary>
    public void CollideButton()
    {
        CollideMap.SetActive(true);
    }

    /// <summary>
    /// Move to Characters editor
    /// </summary>
    public void BackButton()
    {
        LevelsEditor.GetComponent<EditLevels>().CancelAdding();
        EditMap map = EditMap.Instance;
        EditLevelSetting setting = EditLevelSetting.Instance;
        map.ClearMap();
        setting.ClearSettings();

        LevelUI.SetActive(false);
        LevelsUI.SetActive(true);
    }

    /// <summary>
    /// Move to game settings editor
    /// </summary>
    public void NextButton()
    {
        MapEditorUI.SetActive(false);
        LevelSettingEditorUI.SetActive(true);
    }

    #endregion

    #region Data
    public void InitializeColliderMap()
    {
        Vector2 map_size = Background.GetComponent<Image>().rectTransform.sizeDelta;
        ColliderMap = new bool[Mathf.CeilToInt(map_size.y / COLLIDER_SIZE), Mathf.CeilToInt(map_size.x / COLLIDER_SIZE)];
    }

    public void ResetMap()
    {
        Image BackgroundImage = Background.GetComponent<Image>();
        BackgroundImage.sprite = null;
        OnEnable();
        ClearMap();
        Options.Init();
    }

    public void ClearMap()
    {
        foreach (GameObject col in Colliders)
        {
            Destroy(col);
        }
        Colliders.Clear();
        InitializeColliderMap();
        foreach (GameObject obj in Objects)
        {
            Destroy(obj);
        }
        Objects.Clear();
        ObjectInfoList.Clear();
    }

    public void FillMap(string background, List<ObjectInfo> fillObjects, bool[,] fillColliders)
    {
        Sprite bg = Resources.Load<Sprite>(background);
        Image BackgroundImage = Background.GetComponent<Image>();
        BackgroundImage.sprite = bg;
        BackgroundImage.color = Color.white;
        BackgroundImage.rectTransform.sizeDelta = bg.textureRect.size;
        SaveBackgroundPath(BackgroundImage.sprite.name);

        ColliderMap = fillColliders;
        ObjectInfoList = fillObjects;
        FillColliders(fillColliders);
        for (int i = 0; i < fillObjects.Count; i++)
        {
            ObjectInfo cur = new ObjectInfo(fillObjects[i]);
            FillObject(cur);
        }
    }

    private void FillObject(ObjectInfo info){
        Sprite img = Resources.Load<Sprite>(info.GetImage());
        GameObject AddedObject = Instantiate(ObjectPrefab, Background.transform);
        AddedObject.transform.localPosition = info.GetOriginPos();
        AddedObject.GetComponent<Image>().sprite = img;
        AddedObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(img.texture.width, img.texture.height);
        AddedObject.GetComponent<ObjectScript>().message = info.GetMessage();
        Objects.Add(AddedObject);
    }

    private void FillColliders(bool[,] fillColliders){
        float mapWidth = GetMapWidth(), mapHeight = GetMapHeight();
        for (int i = 0; i < fillColliders.GetLength(0); i++){
            for (int j = 0; j < fillColliders.GetLength(1); j++){
                if(fillColliders[i,j]){
                    float posOfWidth = -mapWidth / 2 + j * COLLIDER_SIZE + COLLIDER_SIZE / 2;
                    float posOfHeight = mapHeight / 2 - i * COLLIDER_SIZE - COLLIDER_SIZE / 2;
                    GameObject AddedObject = Instantiate(ColliderImage, CollideMap.transform);
                    AddedObject.transform.localPosition = new Vector2(posOfWidth, posOfHeight);
                    Colliders.Add(AddedObject);
                }
            }
        }
    }

    public void SaveBackgroundPath(string name)
    {
        BackgroundPath = "Maps/" + mapFilter.text + "/" + name;
    }

    public void AddCollider()
    {
        Vector3 mousePos = TempImage.transform.localPosition;
        Vector3 mapPos = GetMapPosition();
        float mapWidth = GetMapWidth(), mapHeight = GetMapHeight();
        int indexOfWidth = Mathf.FloorToInt(Math.Abs(mousePos.x - (mapPos.x - mapWidth / 2)) / COLLIDER_SIZE);
        int indexOfHeight = Mathf.FloorToInt(Math.Abs(mousePos.y - (mapPos.y + mapHeight / 2)) / COLLIDER_SIZE);

        if (ColliderMap[indexOfHeight, indexOfWidth] == true)
        {
            return;
        }
        float posOfWidth = -mapWidth / 2 + indexOfWidth * COLLIDER_SIZE + COLLIDER_SIZE / 2;
        float posOfHeight = mapHeight / 2 - indexOfHeight * COLLIDER_SIZE - COLLIDER_SIZE / 2;

        GameObject AddedObject = Instantiate(ColliderImage, CollideMap.transform);
        AddedObject.transform.localPosition = new Vector2(posOfWidth, posOfHeight);
        Colliders.Add(AddedObject);
        ColliderMap[indexOfHeight, indexOfWidth] = true;
    }

    public void RemoveCollider(GameObject collider)
    {
        Vector3 mousePos = TempImage.transform.localPosition;
        Vector3 mapPos = GetMapPosition();
        float mapWidth = GetMapWidth(), mapHeight = GetMapHeight();
        int indexOfWidth = Mathf.FloorToInt(Math.Abs(mousePos.x - (mapPos.x - mapWidth / 2)) / COLLIDER_SIZE);
        int indexOfHeight = Mathf.FloorToInt(Math.Abs(mousePos.y - (mapPos.y + mapHeight / 2)) / COLLIDER_SIZE);

        Colliders.Remove(collider);
        ColliderMap[indexOfHeight, indexOfWidth] = false;
    }

    public void AddObject()
    {
        Image curImage = TempImage.GetComponent<Image>();
        GameObject AddedObject = Instantiate(ObjectPrefab, Background.transform);
        AddedObject.transform.position = TempImage.transform.position;
        AddedObject.GetComponent<Image>().sprite = curImage.sprite;
        AddedObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(curImage.sprite.texture.width, curImage.sprite.texture.height);
        TMP_InputField messageInput = Message.GetComponent<TMP_InputField>();
        AddedObject.GetComponent<ObjectScript>().message = messageInput.text;
        Objects.Add(AddedObject);

        ObjectInfo curObjectInfo = new ObjectInfo();
        string path = "Objects/" + ObjectFilter.text + "/" + curImage.sprite.name;
        curObjectInfo.SetImage(path);
        string m = String.Copy(Message.GetComponent<TMP_InputField>().text);
        curObjectInfo.SetMessage(m);
        curObjectInfo.SetPosition(AddedObject.transform.localPosition);
        ObjectInfoList.Add(curObjectInfo);
        messageInput.text = "";
    }

    public void RemoveObject(GameObject obj)
    {
        int idx = Objects.IndexOf(obj);
        Objects.RemoveAt(idx);
        ObjectInfoList.RemoveAt(idx);
    }

    private Vector3 GetMapPosition()
    {
        return Background.transform.localPosition;
    }

    private float GetMapWidth()
    {
        return Background.GetComponent<RectTransform>().rect.width;
    }

    private float GetMapHeight()
    {
        return Background.GetComponent<RectTransform>().rect.height;
    }

    #endregion
}
