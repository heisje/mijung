using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class CharacterLoader : Singleton<CharacterLoader>
{
    public GameObject swordsmanPrefab;
    public GameObject spiritPrefab;
    public GameObject gunnerPrefab;
    private Dictionary<ECharacter, GameObject> characterPrefabDictionary;

    protected override void Init()
    {
        characterPrefabDictionary = new Dictionary<ECharacter, GameObject>
        {
            { ECharacter.SwordMan, swordsmanPrefab },
            { ECharacter.Spirit, spiritPrefab },
            { ECharacter.Gunner, gunnerPrefab }
        };
    }

    public async Task<GameObject> LoadCharacterPrefab(ECharacter characterType, Player player)
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
