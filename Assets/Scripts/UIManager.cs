using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Horizontal Group")] [SerializeField]
    private GameObject _horizontalGroup;

    [Header("Select Button Props")] [SerializeField]
    private GameObject _chSelectBtnPrefab;

    [SerializeField] private Vector2 _selectSizeDelta;
    [SerializeField] private Vector2 _unselectSizeDelta;
    [SerializeField] private Color _unSelectColor;
    [SerializeField] private float _nameStartValue;
    [SerializeField] private float _nameEndValue;
    
    [Header("Characters")]
    public List<Character> Characters;
    
    [System.Serializable]
    public class Character
    {
        public Sprite CharacterSprite => _characterSprite;
        public string CharacterName => _characterName;
        
        [SerializeField] private Sprite _characterSprite;
        [SerializeField] private string _characterName;
    }

    private List<CharacterSelectObject> _characterSelectObjects = new();
    private int _selectedChIndex = 0;
    private int _previousChIndex = 0;
    
    private void Start()
    {
       CreateCharacterButtons(); 
    }

    private void CreateCharacterButtons()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            int index = i;
            
            GameObject newChSelectObject = Instantiate(_chSelectBtnPrefab, _horizontalGroup.transform);

            CharacterSelectObject characterSelectObject = newChSelectObject.GetComponent<CharacterSelectObject>();

            _characterSelectObjects.Add(characterSelectObject);
            
            Image characterImage = characterSelectObject.CharacterImage;
            RectTransform rectTransform = characterImage.rectTransform;
            Button characterButton = characterSelectObject.CharacterButton;
            Text characterNameText = characterSelectObject.CharacterNameText;
            Image characterName = characterSelectObject.CharacterName;
            
            characterImage.sprite = Characters[i].CharacterSprite;
            characterNameText.text = Characters[i].CharacterName;
            
            if (index != 0)
            {
                rectTransform.sizeDelta = _unselectSizeDelta;
                characterImage.color = _unSelectColor;
            }
            else
            {
                characterButton.enabled = false;

                characterName.transform.DOLocalMoveY(_nameEndValue, 1f);
            }
            
            characterButton.onClick.AddListener(()=> CharacterSelectButtonOnClick(index));
        }
    }

    private void CharacterSelectButtonOnClick(int selectedIndex)
    {
        _selectedChIndex = selectedIndex;

        CharacterSelectObject selectedCharacterSelectObject = _characterSelectObjects[_selectedChIndex];
        CharacterSelectObject unSelectedCharacterSelectObject = _characterSelectObjects[_previousChIndex];

        Image characterImage = selectedCharacterSelectObject.CharacterImage;
        RectTransform rectTransform = characterImage.rectTransform;
        
        Image previousCharacterImage = unSelectedCharacterSelectObject.CharacterImage;
        RectTransform previousRectTransform = previousCharacterImage.rectTransform;

        rectTransform.DOSizeDelta(_selectSizeDelta, 1f);
        previousRectTransform.DOSizeDelta(_unselectSizeDelta, 1f);

        characterImage.DOColor(Color.white, 1f);
        previousCharacterImage.DOColor(_unSelectColor, 1f);

        selectedCharacterSelectObject.CharacterButton.enabled = false;
        unSelectedCharacterSelectObject.CharacterButton.enabled = true;

        selectedCharacterSelectObject.CharacterName.transform.DOLocalMoveY(_nameEndValue, 1f);
        unSelectedCharacterSelectObject.CharacterName.transform.DOLocalMoveY(_nameStartValue, 1f);

        _previousChIndex = _selectedChIndex;
    }
}
