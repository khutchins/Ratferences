using Ratferences;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveDelayer : MonoBehaviour {
    [Tooltip("How long after the last reference change to wait until saving the game. Useful if options are changed a lot in a small amount of time, like a user messing with a slider.")]
    public float SaveDelay = 1f;
    [Tooltip("Maximum time to wait to save the game. Useful if you're constantly changing some scriptable objects and want to minimize the worst-case save delay scenario. NOTE: In this case, you should probably be disabling triggersSave on the binding.")]
    public float MaxSaveDelay = 15f;

    private float _delayableSaveTime;
    private float _undelayableSaveTime;
    private Coroutine _saveCoroutine = null;
    // Theoretically this variable is redundant because I *should* be able
    // to check if (_saveCoroutine != null), but a null coroutine was still
    // going into the block. Probably Unity doing its weird fake null thing.
    private bool _isCoroutineActive = false;

    public void ScheduleSave(SaveType saveType) {
        switch (saveType) {
            case SaveType.Immediate:
                SaveNow();
                break;
            case SaveType.AfterDelay:
                ScheduleSave();
                break;
            case SaveType.DoesntTrigger:
            default:
                break;
        }
    }

    public void ScheduleSave() {
        _delayableSaveTime = Time.unscaledTime + SaveDelay;
        _undelayableSaveTime = Mathf.Min(Time.unscaledTime + MaxSaveDelay, _undelayableSaveTime);
        if (!_isCoroutineActive) {
            _isCoroutineActive = true;
            _saveCoroutine = StartCoroutine(SaveOptionsAfterDelay());
        }
    }

    IEnumerator SaveOptionsAfterDelay() {
        while (Time.unscaledTime < _delayableSaveTime && Time.unscaledTime < _undelayableSaveTime) {
            yield return null;
        }
        SaveNow();
    }

    protected void CancelSave() {
        if (_isCoroutineActive) {
            // Theoretically these should be in alignment, but just in case.
            if (_saveCoroutine != null) StopCoroutine(_saveCoroutine);
            _saveCoroutine = null;
            _isCoroutineActive = false;
        }
        _undelayableSaveTime = float.MaxValue;
    }

    public void SaveNow() {
        CancelSave();
        PerformSave();
    }

    protected abstract void PerformSave();
}
