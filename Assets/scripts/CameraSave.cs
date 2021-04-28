// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using System.IO;
// using UnityEngine;
 
// public class CameraSave : MonoBehaviour {
 

 
//     public Texture2D CamCapture()
//     {
//         Camera Cam = GetComponent<Camera>();
 
//         RenderTexture currentRT = RenderTexture.active;
//         RenderTexture.active = Cam.targetTexture;
 
//         Cam.Render();
 
//         Texture2D Image = new Texture2D(Cam.targetTexture.width, Cam.targetTexture.height);
//         Image.ReadPixels(new Rect(0, 0, Cam.targetTexture.width, Cam.targetTexture.height), 0, 0);
//         Image.Apply();
//         RenderTexture.active = currentRT;
 
//         return Image;
//         // var Bytes = Image.EncodeToPNG();
//         // Destroy(Image);
 
//         // File.WriteAllBytes(Application.dataPath + "/Backgrounds/" + FileCounter + ".png", Bytes);
//         // FileCounter++;
//     }
   
// }
