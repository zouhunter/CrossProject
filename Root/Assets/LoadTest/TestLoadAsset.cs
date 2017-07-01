using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

public class TestLoadAsset : MonoBehaviour {
    public string url = "";
    public string menu = "";
    public string bundleName;
    public string assetName;
	// Use this for initialization
	void Start () {
        AssetBundleLoader loader = AssetBundleLoader.GetInstance(url, menu);
        loader.LoadAssetFromUrlAsync<GameObject>(bundleName, assetName, OnLoad);
	}
	
	// Update is called once per frame
	void OnLoad (GameObject go) {
        Instantiate(go);
	}
}
