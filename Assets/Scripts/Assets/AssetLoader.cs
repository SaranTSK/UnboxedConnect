using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Unboxed
{
    public static class AssetLoader
    {
        public static async Task CreateAssetAddToList<T>(AssetReference reference, List<T> completeAsset)
            where T : Object
        {
            completeAsset.Add(await reference.LoadAssetAsync<GameObject>().Task as T);
        }

        public static async Task CreateAssetAddToList<T>(List<AssetReference> references, List<T> completeAsset)
            where T : Object
        {
            foreach(AssetReference reference in references)
            {
                completeAsset.Add(await reference.LoadAssetAsync<GameObject>().Task as T);
            }
        }

        public static async Task InstantiateAssetReference<T>(List<AssetReference> references, List<T> completeAsset)
            where T : Object
        {
            foreach (var reference in references)
            {
                completeAsset.Add(await reference.InstantiateAsync().Task as T);
            }
        }

        public static async Task LoadSpriteAddToList<T>(AssetReferenceSprite reference, List<T> completeAsset)
            where T : Object
        {
            completeAsset.Add(await reference.LoadAssetAsync().Task as T);
        }

        public static async Task LoadSpriteAddToList<T>(List<AssetReferenceSprite> references, List<T> completeAsset)
            where T : Object
        {
            foreach (AssetReferenceSprite reference in references)
            {
                completeAsset.Add(await reference.LoadAssetAsync().Task as T);
            }
        }
    }
}

