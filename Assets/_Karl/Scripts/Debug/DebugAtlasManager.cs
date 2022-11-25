using System.IO;
using Sprites;
using UnityEngine;

public abstract class DebugAtlasManager : BaseMonoBehaviour
{
    [TextArea(3,6)]
    [SerializeField] private string _directory;

    public bool DirectoryExists() => Directory.Exists(_directory);

    protected abstract SpriteAtlas[] GetAtlases();


    public void Save()
    {
        if (!DirectoryExists())
        {
            Debug.LogWarning($"Directory does not exist '{_directory}'");
            return;
        }

        SpriteAtlas[] atlases = GetAtlases();
        if (atlases == null) return;

        int length = atlases.Length;
        for (int i = 0; i < length; i++)
        {
            SpriteAtlas atlas = atlases[i];
            if (atlas == null) continue;
            Texture2D tex = atlas.Texture;
            if(tex == null) continue;
            string fileName = $"{i} [{tex.name}]";
            string path = $"{_directory}\\{fileName}.png";
            File.WriteAllBytes(path, tex.EncodeToPNG());
            Debug.Log($"New PNG saved at '{path}'");
        }
    }
}
