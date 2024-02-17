using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectObject : MonoBehaviour
{
    public Image CharacterImage => _characterImage;
    public Button CharacterButton => _characterButton;
    public Text CharacterNameText => _characterNameText;
    public Image CharacterName => _characterName;
    
    [SerializeField] private Image _characterImage;
    [SerializeField] private Button _characterButton;
    [SerializeField] private Text _characterNameText;
    [SerializeField] private Image _characterName;
}
