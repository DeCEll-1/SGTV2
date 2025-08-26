using RGL.API.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SGTV2.Impl.Helpers
{
    public class ResourceHelper
    {// made chatgpt make comments cuz man, this is annoying


        // Returns a Texture object associated with a given type string.
        public static Texture GetTypeTexture(string type)
        {
            return Resources.Textures[GetPathFromType(type)];
        }

        // Resolves a type string to a file path for the texture associated with that type.
        private static string GetPathFromType(string type)
        {
            string id = "";             // Will store the resolved ID of the type
            string texturePath = "";    // Will store the final path to the texture

            // Check if the type exists in StarGenDatas and retrieve its ID if found
            if (Settings.GameResources.StarGenDatas.Any(x => x.id == type))
                id = Settings.GameResources.StarGenDatas.First(x => x.id == type).id;

            // Check if the type exists in PlanetGenDatas and retrieve its ID if found
            if (Settings.GameResources.PlanetGenDatas.Any(x => x.id == type))
                id = Settings.GameResources.PlanetGenDatas.First(x => x.id == type).id;

            // Priority 1: Use the "Texture" field if it exists and is not empty
            if (!string.IsNullOrEmpty(Settings.GameResources.PlanetJson[id].Texture))
            {
                // so, /s crash in windows so im turning /s to \s and im not really sure if this will work in linux or w/e sooooo
                string path = Settings.GameResources.PlanetJson[id].Texture.Replace(@"/", @"\");
                texturePath = Path.Combine(Paths.GameCore.FullName, path);
            }
            // Priority 2: If "Texture" is not available, try the "Icon" field
            else if (!string.IsNullOrEmpty(Settings.GameResources.PlanetJson[id].Icon))
            {
                string path = Settings.GameResources.PlanetJson[id].Icon.Replace(@"/", @"\");
                texturePath = Path.Combine(Paths.GameCore.FullName, path);
            }
            // Priority 3: If neither "Texture" nor "Icon" is available, try "StarscapeIcon"
            else if (!string.IsNullOrEmpty(Settings.GameResources.PlanetJson[id].StarscapeIcon))
            {
                string path = Settings.GameResources.PlanetJson[id].StarscapeIcon.Replace(@"/", @"\");
                texturePath = Path.Combine(Paths.GameCore.FullName, path);
            }

            // If a valid file exists at the constructed path, add it to the texture resources and return the path
            if (File.Exists(texturePath))
            {
                ResourceController.AddTexture(texturePath);
                return texturePath;
            }

            // Fallback: Return a default texture path when no valid file was found
            return AppResources.Textures.NoTextureFoundTexture_png;
        }


        // Disposes and removes the texture associated with a given type string.
        public static void DisposeTypeTexture(string type)
        {
            // Resolve the full texture path using the same logic as in GetTypeTexture
            string name = GetPathFromType(type);

            // Try to get the texture from the global texture dictionary
            if (Resources.Textures.TryGetValue(name, out Texture texture))
            {
                // Properly dispose of the texture to free GPU/memory resources
                texture.Dispose();

                // Remove the texture entry from the dictionary to avoid stale references
                Resources.Textures.Remove(name);
            }
        }




    }
}
