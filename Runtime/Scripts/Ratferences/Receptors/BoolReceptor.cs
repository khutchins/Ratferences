using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ratferences {
	public class BoolReceptor : ValueReceptor<bool, BoolReference> {
		public void UpdateValue(int newValue) {
			Reference?.SetValue(newValue == 0 ? false : true);
		}
	}
}