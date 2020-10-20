using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    private void Awake()
    {
        instance = this;
    }
    static public SoundManager GetInstance()
    {
        if(instance == null)
        {
            GameObject obj = new GameObject();
            obj.name = "SoundManager";
            instance = obj.AddComponent<SoundManager>();
        }
        return instance;
    }
    // Update is called once per frame
    void Update()
    {
    }
    /*
     나는 여기서 사운드 매니저를 만들 것 임.
     현재, 각 씬 캔버스에 Audio Source에 있는 것을 여기에서 모든 것을 처리할 것이다.
     그래서 어떤 값을 변경해서 음악이 변경할 수 있도록 할 것임.
     그럼 일단 오디오 소스들을 public으로 선언을 해서 여기서 모든 값들을 받는 것이 1번이다.
     그 다음은 
     */
}
