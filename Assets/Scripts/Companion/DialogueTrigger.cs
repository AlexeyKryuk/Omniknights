using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private InputChecker _inputChecker;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Aggregator _aggregator;
    [SerializeField] private Companion _companion;
    [Header("??????")]
    [SerializeField] private Camera _switchTo;
    [SerializeField] private Camera _switchFrom;
    [Header("????????? ????")]
    [SerializeField] private GameObject _companionTextWindow;
    [SerializeField] private GameObject _playerTextWindow;
    [Header("?????")]
    [SerializeField] private TMP_Text _companionText;
    [SerializeField] private TMP_Text _playerText;
    [Header("????? ??????????")]
    [SerializeField] private List<string> _companionPhrases;
    [SerializeField] private List<string> _playerPhrases;

    private int _currentPhrase;

    private void OnEnable()
    {
        _switchTo.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _inputChecker.Clicked -= NextPhrase;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            SwitchCamera();
            NextPhrase();
            _inputChecker.Clicked += NextPhrase;
            _joystick.IsActive = false;
            _joystick.ResetHandle();
        }
    }

    private void SwitchCamera()
    {
        if (_switchTo.gameObject.activeInHierarchy == false)
        {
            _switchTo.gameObject.SetActive(true);
            _switchFrom.gameObject.SetActive(false);
        }
        else
        {
            _switchFrom.gameObject.SetActive(true);
            _switchTo.gameObject.SetActive(false);
        }
    }

    private void NextPhrase()
    {
        if (_currentPhrase >= _playerPhrases.Count + _companionPhrases.Count)
        {
            EndDialogue();
            return;
        }

        if (_currentPhrase % 2 == 0)
        {
            if (_companionTextWindow.gameObject.activeInHierarchy == false)
                _companionTextWindow.gameObject.SetActive(true);

            _companionText.text = _companionPhrases[_currentPhrase / 2];
            _currentPhrase++;
        }
        else
        {
            if (_playerTextWindow.gameObject.activeInHierarchy == false)
                _playerTextWindow.gameObject.SetActive(true);

            _playerText.text = _playerPhrases[(_currentPhrase - 1) / 2];
            _currentPhrase++;
        }
    }

    private void EndDialogue()
    {
        SwitchCamera();

        _joystick.IsActive = true;
        _companionTextWindow.gameObject.SetActive(false);
        _playerTextWindow.gameObject.SetActive(false);

        _aggregator.TakeOver(_companion);
        gameObject.SetActive(false);
    }
}
