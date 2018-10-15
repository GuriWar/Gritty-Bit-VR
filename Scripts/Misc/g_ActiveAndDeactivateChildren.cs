using UnityEngine;
using System.Collections;

public class g_ActiveAndDeactivateChildren : MonoBehaviour 
{
	[SerializeField]
	string[] names;
	
	public void ActivateChildren()
	{
		for (int i =0; i < names.Length; i++)
		{
			TransformDeepChildExtension.FindDeepChild (transform, names[i]).gameObject.SetActive(true);
		}
	}

    public void ActivateChildren(int index)
    {
        TransformDeepChildExtension.FindDeepChild(transform, names[index]).gameObject.SetActive(true);
    }

	public void DeactivateChildren()
	{
		for (int i =0; i < names.Length; i++)
		{
			TransformDeepChildExtension.FindDeepChild (transform, names[i]).gameObject.SetActive(false);
		}
	}

    public void DeactivateChildren(int index)
    {
        TransformDeepChildExtension.FindDeepChild(transform, names[index]).gameObject.SetActive(false);
    }
}
