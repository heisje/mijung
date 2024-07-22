using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SelectManager : Singleton<SelectManager>
{
    public GameObject SelectButtonPrefab;
    public GameObject EmptyObjectPrefab;
    private TaskCompletionSource<int> SelectedUserButton; // 어떤 입력을 했는지 저장하고, Round상태를 결정지음

    public async Task<int> SelectButtonGroup(KeyValuePair<int, string>[] data, Transform targetTransform)
    {
        GameObject emptyObject = Instantiate(EmptyObjectPrefab, targetTransform);
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