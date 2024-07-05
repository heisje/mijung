using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        // 반환할 리스트 초기화
        var list = new List<Dictionary<string, object>>();

        // Resources 폴더에서 파일을 불러옴
        TextAsset data = Resources.Load(file) as TextAsset;

        // 파일의 내용을 줄바꿈을 기준으로 분할
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        // 줄 수가 1 이하일 경우 빈 리스트 반환
        if (lines.Length <= 1) return list;

        // 첫 번째 줄(헤더)을 분할
        var header = Regex.Split(lines[0], SPLIT_RE);

        // 두 번째 줄부터 반복
        for (var i = 1; i < lines.Length; i++)
        {
            // 각 셀을 분할
            var values = Regex.Split(lines[i], SPLIT_RE);

            // 빈 줄 건너뛰기
            if (values.Length == 0 || values[0] == "") continue;

            // 새로운 Dictionary 초기화
            var entry = new Dictionary<string, object>();

            // 헤더와 값을 매칭하여 Dictionary에 추가
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;

                // 문자열을 정수로 변환 시도
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                // 문자열을 실수로 변환 시도
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }

                // 헤더와 값을 Dictionary에 추가
                entry[header[j]] = finalvalue;
            }

            // Dictionary를 리스트에 추가
            list.Add(entry);
        }
        return list;
    }
}
