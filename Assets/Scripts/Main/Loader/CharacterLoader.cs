using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class CharacterLoader : Singleton<CharacterLoader>
{
    public GameObject swordsmanPrefab;
    public GameObject spiritPrefab;
    public GameObject gunnerPrefab;
    private Dictionary<CharacterType, GameObject> characterPrefabDictionary;

    protected override void Instantiation()
    {
        characterPrefabDictionary = new Dictionary<CharacterType, GameObject>
        {
            { CharacterType.Swordsman, swordsmanPrefab },
            { CharacterType.Spirit, spiritPrefab },
            { CharacterType.Gunner, gunnerPrefab }
        };
    }

    public async Task<GameObject> LoadCharacterPrefab(CharacterType characterType, Player player)
    {
        if (characterPrefabDictionary.TryGetValue(characterType, out GameObject prefab))
        {
            return Instantiate(prefab, player.transform);
        }
        else
        {
            throw new Exception($"Prefab not found for character type: {characterType}");
        }
    }

}
