using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public static SelectManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject SelectButtonPrefab;
    public GameObject EmptyObjectPrefab;
    private TaskCompletionSource<int> SelectedUserButton; // 어떤 입력을 했는지 저장하고, Round상태를 결정지음

    public async Task<int> SelectButtonGroup(KeyValuePair<int, string>[] data)
    {
        GameObject emptyObject = Instantiate(EmptyObjectPrefab, transform);
        // GameObject selectButtonGroupObject = new GameObject("SelectButtonGroup");
        // selectButtonGroupObject.transform.SetParent(transform, false);
        // selectButtonGroupObject.transform.localPosition = Vector3.zero;
        // selectButtonGroupObject.transform.localScale = Vector3.one;
        SelectButtonGroup selectButtonsGroup = emptyObject.AddComponent<SelectButtonGroup>();

        selectButtonsGroup.Initialize(data, SelectButtonPrefab, this);

        return await WaitForUserInput();
    }

    public void OnClickButton(int i)
    {
        Debug.Log($"{i} 선택됨");
        // 유저가 클릭했을 때 호출되는 메서드
        if (SelectedUserButton != null && !SelectedUserButton.Task.IsCompleted)
        {
            SelectedUserButton.SetResult(i);
        }
    }

    // 비동기입력을 대기한다.
    private Task<int> WaitForUserInput()
    {
        SelectedUserButton = new TaskCompletionSource<int>();
        return SelectedUserButton.Task;
    }
}