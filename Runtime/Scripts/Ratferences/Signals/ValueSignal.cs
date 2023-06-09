using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ratferences {
	public class ValueSignal<T> : ScriptableObject {

		public delegate void SignalRaised(T value);

		public SignalRaised OnSignalRaised;

		public void Raise(T value) {
			OnSignalRaised?.Invoke(value);
		}
	}
}