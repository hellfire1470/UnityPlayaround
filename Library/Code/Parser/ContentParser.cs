using System.Collections;
using System.Collections.Generic;

public static class ContentParser {
	private static Dictionary<string, string> _dictReplaceWith = new Dictionary<string, string>();

	public static void InsertReplace(string key, string value){
		_dictReplaceWith.Add (key, value);
	}

	public static string ReplaceFromList(string input){
		foreach (KeyValuePair<string, string> pair in _dictReplaceWith) {
			input = input.Replace (pair.Key, pair.Value);
		}
		return input;
	}
}
