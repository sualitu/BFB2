namespace Assets.Utilities {
    using System.Collections.Generic;
    using System.IO;

    using UnityEngine;

    public static class Texture2DExtensions
    {
        public static Texture2D Copy(this Texture2D texture)
        {
            var textureCopy = new Texture2D(texture.width, texture.height);
            textureCopy.SetPixels(texture.GetPixels());
            return textureCopy;
        }

        public static void SaveToDisk(this Texture2D texture, string path) {
            var bytes = texture.EncodeToPNG();
            File.WriteAllBytes(path, bytes);
        }
    }
}