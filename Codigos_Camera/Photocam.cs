using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Photocam : MonoBehaviour
{
    Camera camer;
    int resWidth = 640;
    int resHeigth = 480;
    Texture2D photoTexture;
    public GameObject prefab;
    public Animator anim;
    public GameObject photo;
    AudioSource audio;
    bool foto=true;

    private void Awake()
    {
        //snapCam = GetComponent<Camera>();
        camer = GetComponent<Camera>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && foto)
        {
            CallSnapshot();
        }
    }

    public void CallSnapshot()
    {
        // Crear la textura de la foto
        photoTexture = new Texture2D(resWidth, resHeigth, TextureFormat.RGB24, false);

        //RenderTexture renderTexture = camera.targetTexture;
        camer.Render();
        RenderTexture.active =camer.targetTexture;
        
        //RenderTexture.active = null;

        // Guardar la foto en un archivo
        //byte[] bytes = photoTexture.EncodeToPNG();
        //string fileName = SnapsotName();
        //File.WriteAllBytes(fileName, bytes);

        //GameObject photoObject = new GameObject("Photo");
        //photoObject.transform.position = new Vector3(0, 6, 10);
        Sprite sprite = Sprite.Create(photoTexture, new Rect(0, 0, photoTexture.width, photoTexture.height), new Vector3(0.5f, 0.5f,0.5f));
        // SpriteRenderer spriteRenderer = photoObject.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        GameObject instantiatedObject = Instantiate(prefab, Vector3.zero, Quaternion.Euler(Vector3.zero));//Quaternion.LookRotation(camer.transform.forward)
        instantiatedObject.transform.SetParent(photo.transform,false);
        anim.SetTrigger("Foto");
        foto=false;
        audio.Play();
        StartCoroutine(DesactivarPrefab(instantiatedObject));
        StartCoroutine(TomarFoto());
    }

    public void offenablePhoto(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnPostRender()
    {
        photoTexture.ReadPixels(new Rect(0, 0, resWidth, resHeigth), 0, 0);
        photoTexture.Apply();
    }

    string SnapsotName()
    {
        return string.Format("{0}/Snapshots/snap_{1}x{2}_{3}.jpg",
            Application.dataPath,
            resWidth,
            resHeigth,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    IEnumerator DesactivarPrefab(GameObject obj)
    {
        // Esperar 4 segundos
        yield return new WaitForSeconds(4f);

        // Desactivar el objeto prefab
        offenablePhoto(obj);
    }

    IEnumerator TomarFoto()
    {
        // Esperar 4 segundos
        yield return new WaitForSeconds(4f);
        foto=true;
    }
}
