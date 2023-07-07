using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RangedFloat), true)]
public class RangedFloatDrawer : PropertyDrawer 
{

	private const float RANGE_BOUNDS_LABEL_WIDTH = 40f;
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		label = EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, label);

		SerializedProperty minProp = property.FindPropertyRelative(RangedFloat.MIN_VALUE_NAME);
		SerializedProperty maxProp = property.FindPropertyRelative(RangedFloat.MAX_VALUE_NAME);

		float minValue = minProp.floatValue;
		float maxValue = maxProp.floatValue;

		float rangeMin = 0;
		float rangeMax = 1;

		var ranges = (MinMaxRangeAttribute[]) fieldInfo.GetCustomAttributes(typeof (MinMaxRangeAttribute), true);
		if (ranges.Length > 0)
		{
			rangeMin = ranges[0].Min;
			rangeMax = ranges[0].Max;
		}

		var rangeBoundsLabel1Rect = new Rect(position);
		rangeBoundsLabel1Rect.width = RANGE_BOUNDS_LABEL_WIDTH;
		GUI.Label(rangeBoundsLabel1Rect, new GUIContent(minValue.ToString("F2")));
		position.xMin += RANGE_BOUNDS_LABEL_WIDTH;

		var rangeBoundsLabel2Rect = new Rect(position);
		rangeBoundsLabel2Rect.xMin = rangeBoundsLabel2Rect.xMax - RANGE_BOUNDS_LABEL_WIDTH;
		GUI.Label(rangeBoundsLabel2Rect, new GUIContent(maxValue.ToString("F2")));
		position.xMax -= RANGE_BOUNDS_LABEL_WIDTH;

		EditorGUI.BeginChangeCheck();
		{
			EditorGUI.MinMaxSlider(position, ref minValue, ref maxValue, rangeMin, rangeMax);
			
			if (EditorGUI.EndChangeCheck())
			{
				minProp.floatValue = minValue;
				maxProp.floatValue = maxValue;
			}
		}

		EditorGUI.EndProperty();
	}
}
