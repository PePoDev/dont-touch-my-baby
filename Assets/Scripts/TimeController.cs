using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
	public float timeAllDay;
	public Action OnNextDay, OnNight;

	private float currentTime = 0f;
	private bool isDay = true;

	private void Update()
	{
		transform.Rotate(Vector3.down * (360 / timeAllDay) * Time.deltaTime);

		currentTime += Time.deltaTime;
		if (currentTime > timeAllDay / 2f)
		{
			currentTime -= timeAllDay / 2f;

			isDay = !isDay;

			if (isDay)
			{
				// play audio
				OnNextDay();
			}
			else
			{
				// play audio
				OnNight();
			}
		}
	}
}
