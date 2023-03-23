using System;
using UnityEngine;

[Serializable]
public class PubInspect
{
	[SerializeField] string startDate;
	[SerializeField] string endDate;
	[SerializeField] string language;
	[SerializeField] string message;

	public string StartDate { get => startDate; }
	public string EndDate { get => endDate; }
	public string Language { get => language; }
	public string Message { get => message; }
}
