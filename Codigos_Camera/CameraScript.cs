using System.IO;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Texture2D photoTexture;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CapturePhoto();
        }
    }

    private void CapturePhoto()
    {
        // Crear la textura de la foto
        photoTexture = new Texture2D(1024, 1024, TextureFormat.RGB24, false);

        // Renderizar la escena en la textura
        Camera camera = GetComponent<Camera>();
        RenderTexture renderTexture = camera.targetTexture;
        RenderTexture.active = renderTexture;
        camera.Render();
        photoTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        photoTexture.Apply();
        RenderTexture.active = null;

        // Guardar la foto en un archivo
        byte[] bytes = photoTexture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/photo.png", bytes);

        // Opcional: mostrar la foto en un objeto de imagen en la escena
        GameObject photoObject = new GameObject("Photo");
        photoObject.transform.position = new Vector3(0, 0, 10);
        Sprite sprite = Sprite.Create(photoTexture, new Rect(0, 0, photoTexture.width, photoTexture.height), new Vector2(0.5f, 0.5f));
        SpriteRenderer spriteRenderer = photoObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
}